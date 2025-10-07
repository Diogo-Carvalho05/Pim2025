using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;
using BemAgendado.Model;
using System.Windows.Forms;


namespace BemAgendado.Controler
{
    public class BuscarAgenda : IDisposable
    {
        private readonly DbBemAgendado _dbConnection;

        public BuscarAgenda(DbBemAgendado dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Agendamento> BuscarTodosAgendamentos()
        {
            var agendamentos = new List<Agendamento>();
            try
            {
                _dbConnection.Open();
                string query = @"
                    SELECT  a.id AS id_agendamento, p.nome AS nome_paciente, p.email, a.data_agendamento, a.horario_agendamento, a.status
                    FROM agenda a
                    JOIN paciente p ON a.pacientes_id = p.id
                    ORDER BY a.data_agendamento, a.horario_agendamento;
                ";

                using (var command = new NpgsqlCommand(query, _dbConnection.GetConnection()))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        agendamentos.Add(new Agendamento
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id_agendamento")),
                            nome_paciente = reader.GetString(reader.GetOrdinal("nome_paciente")),
                            data_agendamento = reader.GetDateTime(reader.GetOrdinal("data_agendamento")),
                            horario_agendamento = reader.GetTimeSpan(reader.GetOrdinal("horario_agendamento")),
                            email = reader.GetString(reader.GetOrdinal("email")),
                            status = reader.GetString(reader.GetOrdinal("status"))
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar agendamentos: {ex.Message}");
            }
            finally
            {
                _dbConnection.Close();
            }
            return agendamentos;
        }
        public Agendamento ObterAgendamentoPorId(int agendamentoId)
        {
            try
            {
                _dbConnection.Open();
                string query = @"
            SELECT a.id AS id_agendamento,
                   p.nome AS nome_paciente,
                   p.email,
                   a.data_agendamento,
                   a.horario_agendamento,
                   a.status
            FROM agenda a
            JOIN paciente p ON a.pacientes_id = p.id
            WHERE a.id = @id;
        ";

                using (var command = new NpgsqlCommand(query, _dbConnection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@id", agendamentoId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Agendamento
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id_agendamento")),
                                nome_paciente = reader.GetString(reader.GetOrdinal("nome_paciente")),
                                email = reader.GetString(reader.GetOrdinal("email")),
                                data_agendamento = reader.GetDateTime(reader.GetOrdinal("data_agendamento")),
                                horario_agendamento = reader.GetTimeSpan(reader.GetOrdinal("horario_agendamento")),
                                status = reader.GetString(reader.GetOrdinal("status")),
                                
                            };
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar agendamento por ID: {ex.Message}");
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public bool AlterarData(int agendamentoId, DateTime novaData)
        {
            try
            {
                _dbConnection.Open();
                string query = @"
            UPDATE agenda
            SET data_agendamento = @novaData
            WHERE id = @id;
        ";

                using (var command = new NpgsqlCommand(query, _dbConnection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@novaData", novaData);
                    command.Parameters.AddWithValue("@id", agendamentoId);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao alterar a data do agendamento: {ex.Message}");
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public bool AlterarDataHora(int agendamentoId, DateTime novaData, TimeSpan novoHorario)
        {
            try
            {
                _dbConnection.Open();
                string query = @"
            UPDATE agenda
            SET data_agendamento = @data, horario_agendamento = @horario
            WHERE id = @id;
        ";

                using (var cmd = new NpgsqlCommand(query, _dbConnection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@id", agendamentoId);
                    cmd.Parameters.AddWithValue("@data", novaData.Date);
                    cmd.Parameters.Add("@horario", NpgsqlDbType.Time).Value = novoHorario;

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar data e hora: " + ex.Message);
                return false;
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public bool ConfirmarAgendamento(int agendamentoId)
        {
            try
            {
                _dbConnection.Open();
                string query = @"
            UPDATE agenda
            SET status = 'confirmado'
            WHERE id = @id;
        ";

                using (var command = new NpgsqlCommand(query, _dbConnection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@id", agendamentoId);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao confirmar agendamento: {ex.Message}");
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public bool CancelarAgendamento(int agendamentoId)
        {
            try
            {
                _dbConnection.Open();
                string query = @"
            UPDATE agenda
            SET status = 'cancelado'
            WHERE id = @id;
        ";

                using (var command = new NpgsqlCommand(query, _dbConnection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@id", agendamentoId);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cancelar agendamento: {ex.Message}");
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public bool InserirAgendamento(int pacienteId, DateTime dataAgendamento, TimeSpan horarioAgendamento)
        {
            try
            {
                _dbConnection.Open();
                string query = @"
            INSERT INTO agenda (pacientes_id, data_agendamento, horario_agendamento, status)
            VALUES (@pacienteId, @dataAgendamento, @horarioAgendamento, 'pendente');
        ";

                using (var command = new NpgsqlCommand(query, _dbConnection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@pacienteId", pacienteId);
                    command.Parameters.AddWithValue("@dataAgendamento", dataAgendamento);
                    command.Parameters.Add("@horarioAgendamento", NpgsqlTypes.NpgsqlDbType.Time).Value = horarioAgendamento;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir agendamento: {ex.Message}");
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public List<Paciente> BuscarTodosPacientes()
        {
            var pacientes = new List<Paciente>();
            try
            {
                _dbConnection.Open();
                string query = @"
            SELECT id, nome, email
            FROM paciente
            ORDER BY nome;
        ";

                using (var command = new NpgsqlCommand(query, _dbConnection.GetConnection()))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pacientes.Add(new Paciente
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Nome = reader.GetString(reader.GetOrdinal("nome")),
                            email = reader.GetString(reader.GetOrdinal("email")),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar pacientes: {ex.Message}");
            }
            finally
            {
                _dbConnection.Close();
            }
            return pacientes;
        }

        public void Dispose()
        {
            _dbConnection?.Dispose();
        }
    }
}

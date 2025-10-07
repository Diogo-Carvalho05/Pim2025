using Npgsql;
using NpgsqlTypes;
using System;
using BemAgendado.Model;

namespace BemAgendado.Controler
{
    public class GerenciarUsuario : IDisposable
    {
        private readonly DbBemAgendado _dbConnection;

        public GerenciarUsuario(DbBemAgendado dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public bool CriarUsuario(Usuario novoUsuario)
        {
            try
            {
                _dbConnection.Open();

               
                string checkQuery = "SELECT COUNT(1) FROM usuarios WHERE nomeusuario = @NomeUsuario";
                using (var checkCmd = new NpgsqlCommand(checkQuery, _dbConnection.GetConnection()))
                {
                    checkCmd.Parameters.Add(new NpgsqlParameter("@NomeUsuario", NpgsqlDbType.Varchar)
                    {
                        Value = novoUsuario.NomeUsuario ?? (object)DBNull.Value
                    });

                    if ((long)checkCmd.ExecuteScalar() > 0)
                    {
                        throw new Exception("Já existe um usuário com este nome.");
                    }
                }

                string query = @"
            INSERT INTO usuarios (nomeusuario, senha, tipodeusuario) 
            VALUES (@NomeUsuario, @Senha, @TipoDeUsuario)";

                using (var command = new NpgsqlCommand(query, _dbConnection.GetConnection()))
                {
                    
                    command.Parameters.Add(new NpgsqlParameter("@NomeUsuario", NpgsqlDbType.Varchar)
                    {
                        Value = novoUsuario.NomeUsuario ?? (object)DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("@Senha", NpgsqlDbType.Varchar)
                    {
                        Value = novoUsuario.Senha ?? (object)DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("@TipoDeUsuario", NpgsqlDbType.Varchar)
                    {
                        Value = novoUsuario.TipoDeUsuario ?? (object)DBNull.Value
                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar usuário: {ex.Message}", ex);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public bool EditarUsuario(Usuario usuario)
        {
            try
            {
                _dbConnection.Open();
                string query = @"
            UPDATE usuarios 
            SET nomeusuario = @NomeUsuario, 
                senha = @Senha, 
                tipodeusuario = @TipoDeUsuario 
            WHERE id = @Id";

                using (var command = new NpgsqlCommand(query, _dbConnection.GetConnection()))
                {
                    command.Parameters.Add(new NpgsqlParameter("@NomeUsuario", NpgsqlDbType.Varchar)
                    {
                        Value = usuario.NomeUsuario ?? (object)DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("@Senha", NpgsqlDbType.Varchar)
                    {
                        Value = usuario.Senha ?? (object)DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("@TipoDeUsuario", NpgsqlDbType.Varchar)
                    {
                        Value = usuario.TipoDeUsuario ?? (object)DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("@Id", NpgsqlDbType.Integer)
                    {
                        Value = usuario.Id
                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao editar usuário: {ex.Message}", ex);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public bool ExcluirUsuario(int id)
        {
            try
            {
                _dbConnection.Open();
                string query = "DELETE FROM usuarios WHERE id = @Id";
                using (var command = new NpgsqlCommand(query, _dbConnection.GetConnection()))
                {
                    command.Parameters.Add(new NpgsqlParameter("@Id", NpgsqlDbType.Integer)
                    {
                        Value = id
                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir usuário: {ex.Message}", ex);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public bool TrocarSenha(int usuarioId, string novaSenha)
        {
            try
            {
                _dbConnection.Open();
                string query = @"
            UPDATE usuarios 
            SET senha = @NovaSenha
            WHERE id = @UsuarioId";

                using (var command = new NpgsqlCommand(query, _dbConnection.GetConnection()))
                {
                    command.Parameters.Add(new NpgsqlParameter("@NovaSenha", NpgsqlDbType.Varchar)
                    {
                        Value = novaSenha ?? (object)DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("@UsuarioId", NpgsqlDbType.Integer)
                    {
                        Value = usuarioId
                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao trocar senha: {ex.Message}", ex);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public bool VerificarSenhaAtual(int usuarioId, string senhaAtual)
        {
            try
            {
                _dbConnection.Open();
                string query = "SELECT senha FROM usuarios WHERE id = @UsuarioId";

                using (var command = new NpgsqlCommand(query, _dbConnection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@UsuarioId", usuarioId);

                    var senhaBanco = command.ExecuteScalar()?.ToString();
                    return senhaBanco == senhaAtual;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao verificar senha: {ex.Message}", ex);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public void Dispose()
        {
            _dbConnection?.Dispose();
        }
    }
}
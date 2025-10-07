using System;
using System.Collections.Generic;
using System.Data;
using BemAgendado.Model;
using Npgsql;

namespace BemAgendado.Controler
{
   
       

    public class BuscarUsuario : IDisposable
    {
        private readonly DbBemAgendado _dbConnection;

        public BuscarUsuario(DbBemAgendado dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public List<Usuario> BuscarTodosUsuarios()
        {
            var usuarios = new List<Usuario>();
            try
            {
                _dbConnection.Open();
                string query = "SELECT id, nomeusuario, senha, tipodeusuario FROM usuarios ORDER BY nomeusuario";
                using (var command = new NpgsqlCommand(query, _dbConnection.GetConnection()))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuarios.Add(new Usuario
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            NomeUsuario = reader.GetString(reader.GetOrdinal("nomeusuario")),
                            Senha = reader.GetString(reader.GetOrdinal("senha")),
                            TipoDeUsuario = reader.GetString(reader.GetOrdinal("tipodeusuario"))
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar todos os usuários: {ex.Message}");
            }
            finally
            {
                _dbConnection.Close();
            }
            return usuarios;
        }
    

        public void Dispose()
        {
            _dbConnection?.Dispose();
        }
    }
}
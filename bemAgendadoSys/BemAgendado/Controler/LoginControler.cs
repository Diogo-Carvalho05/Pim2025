using Npgsql;
using System;
using BemAgendado.Model;

namespace BemAgendado.Controler
{
    

    public class VerificarLogin : IDisposable
    {
        private readonly DbBemAgendado _dbConnection;

        public VerificarLogin(DbBemAgendado dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Método que verifica login e retorna o tipo de usuário
        public Usuario VerificarLoginRetornarUsuario(string nomeUsuario, string senha)
        {

            if (VerificarSenhasPadrao(nomeUsuario, senha, out var usuarioPadrao))
            {
                return usuarioPadrao;
            }
            try
            {
                _dbConnection.Open();
                string query = "SELECT id, nomeusuario, senha, tipodeusuario FROM usuarios WHERE nomeusuario = @NomeUsuario AND senha = @Senha";

                using (var command = new NpgsqlCommand(query, _dbConnection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@NomeUsuario", nomeUsuario);
                    command.Parameters.AddWithValue("@Senha", senha);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                Id = reader.GetInt32(0),
                                NomeUsuario = reader.GetString(1),
                                Senha = reader.GetString(2),
                                TipoDeUsuario = reader.GetString(3)
                            };
                        }
                    }
                }
                return null;
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        private bool VerificarSenhasPadrao(string nomeUsuario, string senha, out Usuario usuario)
        {
            usuario = null;

            // Senha padrão para admin
            if (nomeUsuario.Equals("admin", StringComparison.OrdinalIgnoreCase) &&
                senha == "admin123")
            {
                usuario = new Usuario
                {
                    Id = -1, // ID especial para usuários padrão
                    NomeUsuario = "admin",
                    Senha = "admin123",
                    TipoDeUsuario = "Adm"
                };
                return true;
            }

            // Senha padrão para colaborador
            if (nomeUsuario.Equals("colaborador", StringComparison.OrdinalIgnoreCase) &&
                senha == "colab123")
            {
                usuario = new Usuario
                {
                    Id = -2, // ID especial para usuários padrão
                    NomeUsuario = "colaborador",
                    Senha = "colab123",
                    TipoDeUsuario = "Colaborador"
                };
                return true;
            }

            return false;
        }


        // Método que apenas verifica se o login é válido
        public bool LoginValido(string nomeUsuario, string senha)
        {
            Usuario usuario = VerificarLoginRetornarUsuario(nomeUsuario, senha);
            return usuario != null;
        }
        public void Dispose()
        {
            _dbConnection?.Dispose();
        }
    }
}
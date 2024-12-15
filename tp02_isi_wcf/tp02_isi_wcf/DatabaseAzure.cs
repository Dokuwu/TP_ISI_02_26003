using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static tp02_isi_wcf.ILocalPrevisao;

namespace tp02_isi_wcf
{
    public class DatabaseAzure
    {
        private readonly string connectionString;

        public DatabaseAzure()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void TestConnection()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Conexão bem-sucedida!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao conectar: {ex.Message}");
                }
            }
        }

        public LocalPrevisao ObterLocalPrevisao(string nome, string pais)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM LocalPrevisao WHERE nomeLocal = @nome and pais = @pais";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nome", nome);
                        command.Parameters.AddWithValue("@pais", pais);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new  LocalPrevisao
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    NomeLocal = reader.GetString(reader.GetOrdinal("nomeLocal")),
                                    Latitude = reader.IsDBNull(reader.GetOrdinal("latitude")) ? 0 : (double)reader.GetFloat(reader.GetOrdinal("latitude")),
                                    Longitude = reader.IsDBNull(reader.GetOrdinal("longitude")) ? 0 : (double)reader.GetFloat(reader.GetOrdinal("longitude")),
                                    Pais = reader.GetString(reader.GetOrdinal("pais")),
                                    User_id = reader.GetInt32(reader.GetOrdinal("user_id"))
                                };
                            }
                            else
                            {
                                throw new Exception("Nenhum local encontrado com o ID fornecido.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao obter LocalPrevisao: {ex.Message}");
                }
            }
        }

        public void CriarLocalPrevisao(LocalPrevisao localPrevisao)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO LocalPrevisao (nomeLocal, latitude, longitude, pais, user_id) VALUES (@nomeLocal, @latitude, @longitude, @pais, @user_id)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nomeLocal", localPrevisao.NomeLocal);
                        command.Parameters.AddWithValue("@latitude", localPrevisao.Latitude);
                        command.Parameters.AddWithValue("@longitude", localPrevisao.Longitude);
                        command.Parameters.AddWithValue("@pais", localPrevisao.Pais);
                        command.Parameters.AddWithValue("@user_id", localPrevisao.User_id);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao criar LocalPrevisao: {ex.Message}");
                }
            }
        }

        public void AtualizarLocalPrevisao( LocalPrevisao localPrevisao)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE LocalPrevisao SET nomeLocal = @nomeLocal, latitude = @latitude, longitude = @longitude, pais = @pais, user_id = @user_id WHERE id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", localPrevisao.Id);
                        command.Parameters.AddWithValue("@nomeLocal", localPrevisao.NomeLocal);
                        command.Parameters.AddWithValue("@latitude", localPrevisao.Latitude);
                        command.Parameters.AddWithValue("@longitude", localPrevisao.Longitude);
                        command.Parameters.AddWithValue("@pais", localPrevisao.Pais);
                        command.Parameters.AddWithValue("@user_id", localPrevisao.User_id);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao atualizar LocalPrevisao: {ex.Message}");
                }
            }
        }

        public void DeletarLocalPrevisao(string nome, string pais)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM LocalPrevisao WHERE id = @nome and pais = @pais";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nome", nome);
                        command.Parameters.AddWithValue("@pais", pais);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao deletar LocalPrevisao: {ex.Message}");
                }
            }
        }

        public List< LocalPrevisao> ListarTodosLocaisPrevisao()
        {
            List< LocalPrevisao> locais = new List< LocalPrevisao>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM LocalPrevisao";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                locais.Add(new LocalPrevisao
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    NomeLocal = reader.GetString(reader.GetOrdinal("nomeLocal")),
                                    Latitude = reader.IsDBNull(reader.GetOrdinal("latitude")) ? 0 : (double)reader.GetFloat(reader.GetOrdinal("latitude")),
                                    Longitude = reader.IsDBNull(reader.GetOrdinal("longitude")) ? 0 : (double)reader.GetFloat(reader.GetOrdinal("longitude")),
                                    Pais = reader.GetString(reader.GetOrdinal("pais")),
                                    User_id = reader.GetInt32(reader.GetOrdinal("user_id"))
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao listar LocalPrevisao: {ex.Message}");
                }
            }

            return locais;
        }
    }
}

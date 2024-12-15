using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI.WebControls;

namespace tp02_isi_soap
{
    public class DatabaseAzure
    {
        private readonly string connectionString;

        public DatabaseAzure()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        // Método para testar a conexão com o banco de dados
        public string TestConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return "Conexão bem-sucedida!";
                }
            }
            catch (Exception ex)
            {
                return $"Erro ao conectar: {ex.Message}";
            }
        }

        // Método para criar um histórico de alerta
        public string CreateHistoricoAlerta(int localid, DateTime data, string desc, string tipo, string categoria, int prioridade, int nivel, string fonte)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO HistoricoAlerta (localid, data, [desc], tipo, categoria, prioridade, nivel, fonte) " +
                                   "VALUES (@localid, @data, @desc, @tipo, @categoria, @prioridade, @nivel, @fonte)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@localid", localid);
                        command.Parameters.AddWithValue("@data", data);
                        command.Parameters.AddWithValue("@desc", desc);
                        command.Parameters.AddWithValue("@tipo", tipo);
                        command.Parameters.AddWithValue("@categoria", categoria);
                        command.Parameters.AddWithValue("@prioridade", prioridade);
                        command.Parameters.AddWithValue("@nivel", nivel);
                        command.Parameters.AddWithValue("@fonte", fonte);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                return "Registo inserido com sucesso!";
            }
            catch (Exception ex)
            {
                return $"Erro ao inserir registo: {ex.Message}";
            }
        }


        // Método para ler todos os registros
        public DataTable ReadHistoricoAlertas()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM HistoricoAlerta";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                // Retorna uma DataTable vazia em vez de null
                DataTable emptyDataTable = new DataTable();
                return emptyDataTable;
            }
        }

        // Método para atualizar um histórico de alerta existente
        public string UpdateHistoricoAlerta(int localid, DateTime data, string desc, string tipo, string categoria, int? prioridade, int? nivel, string fonte)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Inicia a construção da query SQL
                    string query = "UPDATE HistoricoAlerta SET ";

                    // Lista para armazenar os parâmetros a serem atualizados
                    List<SqlParameter> parameters = new List<SqlParameter>();

                    // Condicionais para verificar se os campos foram passados
                    if (!string.IsNullOrEmpty(desc))
                    {
                        query += "[desc] = @desc, ";
                        parameters.Add(new SqlParameter("@desc", desc));
                    }

                    if (!string.IsNullOrEmpty(tipo))
                    {
                        query += "tipo = @tipo, ";
                        parameters.Add(new SqlParameter("@tipo", tipo));
                    }

                    if (!string.IsNullOrEmpty(categoria))
                    {
                        query += "categoria = @categoria, ";
                        parameters.Add(new SqlParameter("@categoria", categoria));
                    }

                    if (prioridade.HasValue)
                    {
                        query += "prioridade = @prioridade, ";
                        parameters.Add(new SqlParameter("@prioridade", prioridade.Value));
                    }

                    if (nivel.HasValue)
                    {
                        query += "nivel = @nivel, ";
                        parameters.Add(new SqlParameter("@nivel", nivel.Value));
                    }

                    if (!string.IsNullOrEmpty(fonte))
                    {
                        query += "fonte = @fonte, ";
                        parameters.Add(new SqlParameter("@fonte", fonte));
                    }

                    // Remover a última vírgula e espaço da query
                    query = query.TrimEnd(',', ' ') + " WHERE localid = @localid AND data = @data";

                    // Adiciona os parâmetros obrigatórios
                    parameters.Add(new SqlParameter("@localid", localid));
                    parameters.Add(new SqlParameter("@data", data));

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Adiciona todos os parâmetros à consulta
                        command.Parameters.AddRange(parameters.ToArray());

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0 ? "Registo atualizado com sucesso!" : "Nenhum registo foi atualizado.";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Erro ao atualizar registo: {ex.Message}";
            }
        }


        // Método para deletar um histórico de alerta
        public string DeleteHistoricoAlerta(int localid, DateTime data)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM HistoricoAlerta WHERE localid = @localid AND data = @data";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@localid", localid);
                        command.Parameters.AddWithValue("@data", data);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0 ? "Registo eliminado com sucesso!" : "Nenhum registo encontrado para eliminar.";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Erro ao eliminar registo: {ex.Message}";
            }
        }

        // Método para obter um histórico de alerta específico
        public HistoricoAlerta GetHistoricoAlerta(int localid, DateTime data)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM HistoricoAlerta WHERE localid = @localid AND data = @data";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@localid", localid);
                        command.Parameters.AddWithValue("@data", data);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new HistoricoAlerta
                                {
                                    Localid = Convert.ToInt32(reader["localid"]),
                                    Data = Convert.ToDateTime(reader["data"]),
                                    Desc = reader["desc"].ToString(),
                                    Tipo = reader["tipo"].ToString(),
                                    Categoria = reader["categoria"].ToString(),
                                    Prioridade = Convert.ToInt32(reader["prioridade"]),
                                    Nivel = reader["nivel"].ToString(),
                                    Fonte = reader["fonte"].ToString()
                                };
                            }
                            else
                            {
                                return new HistoricoAlerta
                                {
                                    Localid = -1,
                                    Data = new DateTime(),
                                    Desc = "",
                                    Tipo = "",
                                    Categoria = "",
                                    Prioridade = -1,
                                    Nivel = "",
                                    Fonte = ""
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter registo: {ex.Message}");
                return new HistoricoAlerta
                {
                    Localid = -1,
                    Data = new DateTime(),
                    Desc = "",
                    Tipo = "",
                    Categoria = "",
                    Prioridade = -1,
                    Nivel = "",
                    Fonte = ""
                };
            }
        }



        // Utilizador


        public string CreateUtilizador(int id, string nome, string senha)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Utilizador (id, nome, senha) VALUES (@id, @nome, @senha)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@nome", nome);
                        command.Parameters.AddWithValue("@senha", senha);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                return "Utilizador inserido com sucesso!";
            }
            catch (Exception ex)
            {
                return $"Erro ao inserir utilizador: {ex.Message}";
            }
        }

        // Método para ler todos os utilizadores
        public DataTable ReadUtilizadores()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Utilizador";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                DataTable emptyDataTable = new DataTable();
                return emptyDataTable;
            }
        }

        // Método para atualizar um utilizador
        public string UpdateUtilizador(int id, string nome, string senha)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Utilizador SET nome = @nome, senha = @senha WHERE id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@nome", nome);
                        command.Parameters.AddWithValue("@senha", senha);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0 ? "Utilizador atualizado com sucesso!" : "Nenhum utilizador encontrado para atualizar.";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Erro ao atualizar utilizador: {ex.Message}";
            }
        }

        // Método para deletar um utilizador
        public string DeleteUtilizador(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Utilizador WHERE id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0 ? "Utilizador removido com sucesso!" : "Nenhum utilizador encontrado para remover.";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Erro ao remover utilizador: {ex.Message}";
            }
        }

        // Método para obter um utilizador específico
        public Utilizador GetUtilizador(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Utilizador WHERE id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Utilizador
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Nome = reader["nome"].ToString(),
                                    Senha = reader["senha"].ToString()
                                };
                            }
                            else
                            {
                                return new Utilizador
                                {
                                    Id = -1,
                                    Nome = "",
                                    Senha = ""
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter utilizador: {ex.Message}");
                return new Utilizador
                {
                    Id = -1,
                    Nome = "",
                    Senha = ""
                };
            }
        }
    }

}


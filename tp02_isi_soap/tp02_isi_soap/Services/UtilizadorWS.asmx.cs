using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace tp02_isi_soap.Services
{
    /// <summary>
    /// Summary description for UtilizadorWS
    /// </summary>
    [WebService(Namespace = "UtilizadorWS", Description = "Serviço de Gestão de Utilizadores")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class UtilizadorWS : WebService
    {
        private readonly DatabaseAzure db;

        public UtilizadorWS()
        {
            db = new DatabaseAzure();
        }

        // Criar Utilizador
        [WebMethod]
        public string CreateUtilizador(int id, string nome, string senha)
        {
            try
            {
                return db.CreateUtilizador(id, nome, senha);
            }
            catch (Exception ex)
            {
                return $"Erro ao inserir utilizador: {ex.Message}";
            }
        }

        // Ler todos os utilizadores
        [WebMethod]
        public DataTable ReadUtilizadores()
        {
            try
            {
                var dataTable = db.ReadUtilizadores();
                dataTable.TableName = "Utilizadores";
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao ler os utilizadores: {ex.Message}");
            }
        }

        // Atualizar um utilizador
        [WebMethod]
        public string UpdateUtilizador(int id, string nome, string senha)
        {
            try
            {
                return db.UpdateUtilizador(id, nome, senha);
            }
            catch (Exception ex)
            {
                return $"Erro ao atualizar utilizador: {ex.Message}";
            }
        }

        // Deletar um utilizador
        [WebMethod]
        public string DeleteUtilizador(int id)
        {
            try
            {
                return db.DeleteUtilizador(id);
            }
            catch (Exception ex)
            {
                return $"Erro ao remover utilizador: {ex.Message}";
            }
        }

        // Obter um utilizador específico
        [WebMethod]
        public Utilizador GetUtilizador(int id)
        {
            try
            {
                return db.GetUtilizador(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter o utilizador: {ex.Message}");
            }
        }
    }
}

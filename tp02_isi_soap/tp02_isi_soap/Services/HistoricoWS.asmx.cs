using System;
using System.Data;
using System.Web;
using System.Web.Services;

namespace tp02_isi_soap.Services
{
    /// <summary>
    /// Summary description for HistoricoWS
    /// </summary>
    [WebService(Namespace = "HistoricoWS", Description = "Historico de alertas temporais")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class HistoricoWS : WebService
    {
        private readonly DatabaseAzure db;

        public HistoricoWS()
        {
            db = new DatabaseAzure();
        }

        [WebMethod]
        public string CreateHistoricoAlerta(int localid, DateTime data, string desc, string tipo, string categoria, int prioridade, int nivel, string fonte)
        {
            try
            {
                return db.CreateHistoricoAlerta(localid, data, desc, tipo, categoria, prioridade, nivel, fonte); // Método de inserção do alerta
            }
            catch (Exception ex)
            {
                return $"Erro ao inserir registo: {ex.Message}";
            }
        }


        [WebMethod]
        public DataTable ReadHistoricoAlertas()
        {
            try
            {
                var dataTable = db.ReadHistoricoAlertas();
                // Definir o nome da tabela para garantir que a serialização ocorra corretamente
                dataTable.TableName = "HistoricoAlertas";
                return dataTable; // Retorna o DataTable com o nome definido
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao ler os registos: {ex.Message}");
            }
        }

        [WebMethod]
        public string UpdateHistoricoAlerta(int localid, DateTime data, string desc, string tipo, string categoria, int? prioridade, int? nivel, string fonte)
        {
            try
            {
                return db.UpdateHistoricoAlerta(localid, data, desc, tipo, categoria, prioridade, nivel, fonte); // Método de atualização do alerta
            }
            catch (Exception ex)
            {
                return $"Erro ao atualizar registo: {ex.Message}";
            }
        }



        [WebMethod]
        public string DeleteHistoricoAlerta(int localid, DateTime data)
        {
            try
            {
                return db.DeleteHistoricoAlerta(localid, data); // Método de remoção do alerta
            }
            catch (Exception ex)
            {
                return $"Erro ao remover registo: {ex.Message}";
            }
        }

        [WebMethod]
        public HistoricoAlerta GetHistoricoAlerta(int localid, DateTime data)
        {
            try
            {
                return db.GetHistoricoAlerta(localid, data); // Retorna o alerta ou null
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter o registo: {ex.Message}");
            }
        }
    }
}

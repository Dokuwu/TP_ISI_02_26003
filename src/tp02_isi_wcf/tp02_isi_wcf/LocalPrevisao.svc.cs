using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using static tp02_isi_wcf.ILocalPrevisao;

namespace tp02_isi_wcf
{
    public class Service2 : ILocalPrevisao
    {
        private readonly DatabaseAzure db;

        public Service2()
        {
            db = new DatabaseAzure();
        }

        // Testar conexão com o banco de dados
        public string TestarConexao()
        {
            try
            {
                db.TestConnection();
                return "Conexão com a base de dados bem-sucedida!";
            }
            catch (Exception ex)
            {
                return $"Erro: {ex.Message}";
            }
        }

        // Obter um local de previsão por ID
        public LocalPrevisao ObterLocalPrevisao(string nome, string pais)
        {
            try
            {
                return db.ObterLocalPrevisao(nome,pais);
            }
            catch (Exception ex)
            {
                throw new FaultException($"Erro ao obter os dados: {ex.Message}");
            }
        }

        // Criar um novo local de previsão
        public string CriarLocalPrevisao(LocalPrevisao localPrevisao)
        {
            try
            {
                db.CriarLocalPrevisao(localPrevisao);
                return "Local de previsão criado com sucesso!";
            }
            catch (Exception ex)
            {
                return $"Erro ao criar LocalPrevisao: {ex.Message}";
            }
        }

        // Atualizar um local de previsão existente
        public string AtualizarLocalPrevisao(LocalPrevisao localPrevisao)
        {
            try
            {
                db.AtualizarLocalPrevisao(localPrevisao);
                return "Local de previsão atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                return $"Erro ao atualizar LocalPrevisao: {ex.Message}";
            }
        }

        // Deletar um local de previsão pelo ID
        public string DeletarLocalPrevisao(string nome, string pais)
        {
            try
            {
                db.DeletarLocalPrevisao(nome,pais);
                return "Local de previsão deletado com sucesso!";
            }
            catch (Exception ex)
            {
                return $"Erro ao deletar LocalPrevisao: {ex.Message}";
            }
        }

        // Listar todos os locais de previsão
        public List<LocalPrevisao> ListarTodosLocaisPrevisao()
        {
            try
            {
                return db.ListarTodosLocaisPrevisao();
            }
            catch (Exception ex)
            {
                throw new FaultException($"Erro ao listar LocalPrevisao: {ex.Message}");
            }
        }
    }
}

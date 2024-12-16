using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace tp02_isi_wcf
{
    [ServiceContract]
    public interface ILocalPrevisao
    {
        [OperationContract]
        string TestarConexao();

        [OperationContract]
        LocalPrevisao ObterLocalPrevisao(string nome, string pais);

        [OperationContract]
        string CriarLocalPrevisao(LocalPrevisao localPrevisao);

        [OperationContract]
        string AtualizarLocalPrevisao(LocalPrevisao localPrevisao);

        [OperationContract]
        string DeletarLocalPrevisao(string nome,string pais);

        [OperationContract]
        List<LocalPrevisao> ListarTodosLocaisPrevisao();

        [DataContract]
        public class LocalPrevisao
        {
            private int id;
            private string nomeLocal;
            private double latitude;
            private double longitude;
            private string pais;
            private int user_id;

            public LocalPrevisao()
            {
            }

            public LocalPrevisao(int idSearch, string nomeLocal, double latitude, double longitude, string pais, int user_id)
            {
                this.id = idSearch;
                this.nomeLocal = nomeLocal;
                this.latitude = latitude;
                this.longitude = longitude;
                this.pais = pais;
                this.user_id = user_id;
            }

            [DataMember]
            public int Id
            {
                get { return id; }
                set { id = value; }
            }

            [DataMember]
            public string NomeLocal
            {
                get { return nomeLocal; }
                set { nomeLocal = value; }
            }

            [DataMember]
            public double Latitude
            {
                get { return latitude; }
                set { latitude = value; }
            }

            [DataMember]
            public double Longitude
            {
                get { return longitude; }
                set { longitude = value; }
            }

            [DataMember]
            public string Pais
            {
                get { return pais; }
                set { pais = value; }
            }

            [DataMember]
            public int User_id
            {
                get { return user_id; }
                set { user_id = value; }
            }
        }
    }
}

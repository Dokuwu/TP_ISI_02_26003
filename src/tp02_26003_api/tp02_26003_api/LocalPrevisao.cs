using System.Runtime.Serialization;

namespace tp02_26003_api
{
    /// <summary>
    /// Classe representante de um local
    /// </summary>
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

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string NomeLocal
        {
            get { return nomeLocal; }
            set { nomeLocal = value; }
        }

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        public string Pais
        {
            get { return pais; }
            set { pais = value; }
        }

        public int User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }
    }
}


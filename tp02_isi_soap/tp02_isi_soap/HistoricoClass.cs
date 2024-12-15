using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tp02_isi_soap
{
    using System.Runtime.Serialization;

    public class HistoricoAlerta
    {
        private int localid;
        private DateTime data;
        private string desc;
        private string tipo;
        private string categoria;
        private int prioridade;
        private string nivel;
        private string fonte;

        public HistoricoAlerta()
        {
        }

        public HistoricoAlerta(int localid, DateTime data, string desc, string tipo, string categoria, int prioridade, string nivel, string fonte)
        {
            this.localid = localid;
            this.data = data;
            this.desc = desc;
            this.tipo = tipo;
            this.categoria = categoria;
            this.prioridade = prioridade;
            this.nivel = nivel;
            this.fonte = fonte;
        }

        public int Localid
        {
            get { return localid; }
            set { localid = value; }
        }

        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

         
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

         
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        public int Prioridade
        {
            get { return prioridade; }
            set { prioridade = value; }
        }

        public string Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }

        public string Fonte
        {
            get { return fonte; }
            set { fonte = value; }
        }
    }
}
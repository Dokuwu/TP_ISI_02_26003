namespace tp02_isi_soap
{

    public class Utilizador
    {
        private int id;
        private string nome;
        private string senha;

        // Construtor padrão
        public Utilizador()
        {
        }

        // Construtor com parâmetros
        public Utilizador(int id, string nome, string senha)
        {
            this.id = id;
            this.nome = nome;
            this.senha = senha;
        }

        // Propriedade ID
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        // Propriedade Nome
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        // Propriedade Senha
        public string Senha
        {
            get { return senha; }
            set { senha = value; }
        }
    }

}

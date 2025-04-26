namespace IMCApi
{
    public class Receita
    {

        public int ReceitaId { get; set; }

        public string Tipo { get; set; } = string.Empty;

        public string NomeReceita { get; set;} = string.Empty;

        public string Ingrendientes {  get; set; } = string.Empty;

    }
}
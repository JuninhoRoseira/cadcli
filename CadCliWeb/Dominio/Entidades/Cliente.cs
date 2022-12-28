namespace Dominio.Entidades
{
    public class Cliente
    {
        public long Id { get; set; }
        public string Documento { get; set; } = "";
        public string Nome { get; set; } = "";
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; } = "";
        public string Endereco { get; set; } = "";
        public string Cidade { get; set; } = "";
        public string Estado { get; set; } = "";
    }
}

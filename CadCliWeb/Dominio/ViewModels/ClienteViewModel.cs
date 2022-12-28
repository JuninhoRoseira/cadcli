namespace Dominio.ViewModels
{
    public class ClienteViewModel
    {
        public long Id { get; set; }
        public string Documento { get; set; } = "";
        public string Nome { get; set; } = "";
        public string DataNascimento { get; set; } = "";
        public string Sexo { get; set; } = "";
        public string Endereco { get; set; } = "";
        public string Cidade { get; set; } = "";
        public string Estado { get; set; } = "";
    }
}

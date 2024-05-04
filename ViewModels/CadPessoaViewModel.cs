namespace KTCSharpApi.ViewModels
{
    public sealed class CadPessoaViewModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public int Idade { get; set; }
        public float Altura { get; set; }
        public float Peso { get; set; }
        public List<CProfissao> Profissoes { get; set; } = new();

        public class CProfissao
        {
            public string? EmpresaNome { get; set; }
            public string? CargoNome { get; set; }
            public string? EmpresaCnpj { get; set; }
        }
    }
}

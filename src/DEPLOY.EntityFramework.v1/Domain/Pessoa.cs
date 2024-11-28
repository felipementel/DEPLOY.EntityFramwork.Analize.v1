namespace DEPLOY.EntityFramework.v1.Domain;
// public class PessoaContrato()
// {
//     public Guid PessoaId { get; set; }
//     public Guid ContratoId { get; set; }

//     public Pessoa Pessoa { get; set; }

//     public Contrato Contrato { get; set; }
// }

public class Pessoa : BaseEntity<Guid>
{
    public Pessoa(Guid id,
        DateTime createdAt,
        DateTime updatedAt,
        string nome,
        string email,
        string telefone,
        string documento,
        string endereco,
        DateOnly dataNascimento)
        : base(id, createdAt, updatedAt)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Documento = documento;
        Endereco = endereco;
        DataNascimento = dataNascimento;
    }

    public string Nome { get; init; }

    public string Email { get; init; }

    public string Telefone { get; init; }

    public string Documento { get; init; }

    public string Endereco { get; init; }

    public DateOnly DataNascimento { get; init; }

    public List<Contrato> Contratos { get; set; }
}
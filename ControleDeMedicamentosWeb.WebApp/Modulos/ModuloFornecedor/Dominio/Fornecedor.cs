using ControleDeMedicamentosWeb.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Dominio;

public class Fornecedor : EntidadeBase<Fornecedor>
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;

    public Fornecedor()
    {
    }

    public Fornecedor(string nome, string telefone, string cnpj)
    {
        Nome = nome;
        Telefone = telefone;
        Cnpj = cnpj;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        ValidarNome(erros);
        ValidarTelefone(erros);
        ValidarCnpj(erros);

        return erros;
    }

    private void ValidarNome(List<string> erros)
    {
        if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");
    }

    private void ValidarTelefone(List<string> erros)
    {
        string telefoneEncurtado = RemoverFormatacao(Telefone);

        if (telefoneEncurtado.StartsWith("0"))
            telefoneEncurtado = telefoneEncurtado.Substring(1);

        bool telefoneValido = true;

        if (telefoneEncurtado.Length < 10 || telefoneEncurtado.Length > 11)
        {
            erros.Add("O campo \"Telefone\" deve conter entre 10 e 11 dígitos.");
            telefoneValido = false;
        }

        if (!ContemSomenteDigitos(telefoneEncurtado))
        {
            erros.Add("O campo \"Telefone\" deve conter apenas dígitos.");
            telefoneValido = false;
        }

        if (telefoneValido)
        {
            if (telefoneEncurtado.Length == 10)
            {
                Telefone = Convert.ToUInt64(telefoneEncurtado)
                    .ToString(@"\(00\) 0000\-0000");
            }
            else
            {
                Telefone = Convert.ToUInt64(telefoneEncurtado)
                    .ToString(@"\(00\) 00000\-0000");
            }
        }
    }

    private void ValidarCnpj(List<string> erros)
    {
        if (string.IsNullOrWhiteSpace(Cnpj))
        {
            erros.Add("O campo \"CNPJ\" deve ser preenchido.");
            return;
        }

        string cnpjEncurtado = RemoverFormatacao(Cnpj);

        bool cnpjValido = true;

        if (cnpjEncurtado.Length != 14)
        {
            erros.Add("O campo \"CNPJ\" deve conter 14 dígitos.");
            cnpjValido = false;
        }

        if (!ContemSomenteDigitos(cnpjEncurtado))
        {
            erros.Add("O campo \"CNPJ\" deve conter somente dígitos.");
            cnpjValido = false;
        }

        if (cnpjValido)
        {
            Cnpj = Convert.ToUInt64(cnpjEncurtado)
                .ToString(@"00\.000\.000\/0000\-00");
        }
    }

    public override void Atualizar(Fornecedor entidadeAtualizada)
    {
        Fornecedor fornecedorAtualizado = entidadeAtualizada;

        Nome = fornecedorAtualizado.Nome;
        Telefone = fornecedorAtualizado.Telefone;
        Cnpj = fornecedorAtualizado.Cnpj;
    }

    private bool ContemSomenteDigitos(string valor)
    {
        for (int i = 0; i < valor.Length; i++)
        {
            if (!char.IsDigit(valor[i]))
                return false;
        }

        return true;
    }

    public static string RemoverFormatacao(string valor)
    {
        return valor
            .Replace(" ", "")
            .Replace("-", "")
            .Replace(".", "")
            .Replace("/", "")
            .Replace("(", "")
            .Replace(")", "");
    }
}
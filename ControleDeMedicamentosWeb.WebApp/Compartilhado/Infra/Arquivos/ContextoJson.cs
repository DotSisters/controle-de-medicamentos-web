using System.Text.Json;
using System.Text.Json.Serialization;

namespace ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra.Arquivos;

public sealed class ContextoJson
{
  // public List<Fornecedor> Fornecedores { get; set; } = new List<Fornecedor>();
  // public List<Paciente> Pacientes { get; set; } = new List<Categoria>();
  // public List<Medicamento> Medicamentos { get; set; } = new List<Medicamento>();
  // public List<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
  // public List<Estoque> Estoque { get; set; } = new List<Estoque>();

  private readonly string caminhoArquivo;

  public ContextoJson()
  {
    string caminhoAppData = Environment
        .GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    string caminhoDiretorio = Path.Combine(caminhoAppData, "ControleDeMedicamentosWeb");

    Directory.CreateDirectory(caminhoDiretorio);

    caminhoArquivo = Path.Combine(caminhoDiretorio, "dados.json");
  }

  public void Salvar()
  {
    JsonSerializerOptions opcoesJson = new JsonSerializerOptions();
    opcoesJson.WriteIndented = true;
    opcoesJson.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;
    opcoesJson.Converters.Add(new JsonStringEnumConverter());

    string jsonString = JsonSerializer.Serialize(this, opcoesJson);

    File.WriteAllText(caminhoArquivo, jsonString);
  }

  public void Carregar()
  {
    if (!File.Exists(caminhoArquivo))
      return;

    string jsonString = File.ReadAllText(caminhoArquivo);

    JsonSerializerOptions opcoesJson = new JsonSerializerOptions();
    opcoesJson.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;
    opcoesJson.Converters.Add(new JsonStringEnumConverter());

    ContextoJson? contextoSalvo = JsonSerializer
        .Deserialize<ContextoJson>(jsonString, opcoesJson);

    if (contextoSalvo == null)
      return;

    // Pacientes = contextoSalvo.Pacientes;
    // Fornecedores = contextoSalvo.Fornecedores;
    // Medicamentos = contextoSalvo.Medicamentos;
    // Funcionarios = contextoSalvo.Funcionarios;
    // Estoque = contextoSalvo.Estoque;
  }
}
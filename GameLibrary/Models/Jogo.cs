using System.Text.Json.Serialization;

namespace GameLibrary.Models;
public class Jogo
{
    public int JogoId { get; set; }

    public string? NomeJogo { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public GeneroJogo Genero { get; set; }

}

public enum GeneroJogo
    {
    Acao,
    Aventura,
    Estrategia,
    RPG,
    Esportes,
    Terror,
    Outro,
    Corrida,
    Simulacao,
    Plataforma,
    QuebraCabeca,
    Luta,
    Musica,
    Casual,
    Educacional,
    Cartas,
    EsporteEletronico,
    Sobrevivencia,
    FPS
    }


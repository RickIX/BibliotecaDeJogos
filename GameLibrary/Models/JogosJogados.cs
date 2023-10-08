namespace GameLibrary.Models;

public class JogosJogados
{
    public int Id { get; set; }
    public int JogadorId { get; set; }
    public Jogador? Jogador { get; set; }
    public int JogoId { get; set; }
    public Jogo? Jogo { get; set; }
    
}

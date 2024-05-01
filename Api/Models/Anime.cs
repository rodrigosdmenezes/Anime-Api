public class Anime{
    
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Resumo { get; set; }
    public string Diretor { get; set; }

    public Anime(){ }
    public Anime(int id, string nome, string resumo, string diretor)
    {
        Id = id;
        Nome = nome;
        Resumo = resumo;
        Diretor = diretor;
    }

}
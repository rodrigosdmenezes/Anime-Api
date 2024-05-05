public class Anime{
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Resumo { get; set; }
    public string Diretor { get; set; }

    public Anime(){ }
    public Anime(int id, string name, string resumo, string diretor)
    {
        Id = id;
        Name = name;
        Resumo = resumo;
        Diretor = diretor;
    }

}
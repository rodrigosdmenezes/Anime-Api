using Moq;

namespace TestsAnimes
{
    public class AnimeServiceTestes
    {
        [Fact]
        public void TestarGetAnimePorId()
        {

            int idDoAnime = 1;
            var animeEsperado = new Anime(1, "Nome do anime", "Resumo do anime", "Diretor do anime");

            var mockAnimeService = new Mock<IAnimeService>();
            mockAnimeService.Setup(service => service.BuscarPorId(idDoAnime)).Returns(animeEsperado);

            var animeService = mockAnimeService.Object;

            var animeEncontrado = animeService.BuscarPorId(idDoAnime);

            Assert.Equal(animeEsperado, animeEncontrado);
        }

        [Fact]
        public void TestarPostAnime()
        {
            var novoAnime = new Anime(1, "Novo anime", "Resumo do Novo anime", "Diretor do Novo anime");

            var mockAnimeService = new Mock<IAnimeService>();
            mockAnimeService.Setup(service => service.Adicionar(It.IsAny<Anime>())).Returns(true);

            var animeService = mockAnimeService.Object;

            bool animeAdicionado = animeService.Adicionar(novoAnime);

            Assert.True(animeAdicionado);
        }

        [Fact]
        public void TestarPutAnime()
        {
            int idDoAnime = 1;
            var animeExistente = new Anime(1,"Nome do anime existente", "Resumo do anime existente", "Diretor do anime existente");
            var animeAtualizado = new Anime(1, "Novo Nome do anime", "Novo Resumo do anime", "Novo Diretor do anime");

            var mockAnimeService = new Mock<IAnimeService>();
            mockAnimeService.Setup(service => service.Atualizar(idDoAnime, animeAtualizado)).Returns(true);

            var animeService = mockAnimeService.Object;

            bool animeAtualizadoComSucesso = animeService.Atualizar(idDoAnime, animeAtualizado);

            Assert.True(animeAtualizadoComSucesso);
        }

        [Fact]
        public void TestarDeleteAnime()
        {
            int idDoAnime = 1;

            var mockAnimeService = new Mock<IAnimeService>();
            mockAnimeService.Setup(service => service.Excluir(idDoAnime)).Returns(true);

            var animeService = mockAnimeService.Object;

            bool animeExcluidoComSucesso = animeService.Excluir(idDoAnime);

            Assert.True(animeExcluidoComSucesso);
        }
        public interface IAnimeService
        {
            Anime BuscarPorId(int id);

            Anime BuscarPorDiretor(string diretor);
            bool Adicionar(Anime anime);
            bool Atualizar(int id, Anime anime);
            bool Excluir(int id);
        }
    }

}
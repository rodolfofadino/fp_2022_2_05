using fiap2022.domain.Models;

namespace fiap2022.application.Interfaces
{
    public interface INoticiaService
    {
        List<Noticia> Load(int totalDeNoticias, string categoria = "padrao");
    }
}
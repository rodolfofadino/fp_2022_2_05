using fiap2022.application.Interfaces;
using fiap2022.domain.Models;
using fiap2022.application.Services;
using Microsoft.AspNetCore.Mvc;

namespace fiap2022.ViewComponents
{
    public class NoticiasViewComponent : ViewComponent
    {

        private INoticiaService _noticiaService;

        public NoticiasViewComponent(INoticiaService noticiaService)
        {
            _noticiaService = noticiaService;

        }

        public async Task<IViewComponentResult> InvokeAsync(int total, bool noticiasUrgentes)
        {
            var view = "noticias";

            if (noticiasUrgentes)
            {
                view = "noticiasUrgentes";
            }
            var noticias = _noticiaService.Load(total);


            return View(view, noticias);

        }

    }
}

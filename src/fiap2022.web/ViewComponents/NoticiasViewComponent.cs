﻿using fiap2022.core.Models;
using fiap2022.core.Services;
using Microsoft.AspNetCore.Mvc;

namespace fiap2022.ViewComponents
{
    public class NoticiasViewComponent : ViewComponent
    {

        private NoticiaService _noticiaService;

        public NoticiasViewComponent(NoticiaService noticiaService)
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

        private IEnumerable<Noticia> GetItems(int total)
        {

            //var listNoticias = new List<Noticia>();
            for(int i = 1; i <= total; i++)
            {
               // listNoticias.Add(new Noticia() { Id = i, Titulo = $"Titulo Da Noticia {i}" });
                yield return new Noticia() { Id = i, Titulo = $"Titulo Da Noticia {i}" };

            }

            //return listNoticias;
        }
    }
}

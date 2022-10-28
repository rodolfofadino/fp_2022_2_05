using fiap2022.Models;
using Microsoft.AspNetCore.Mvc;

namespace fiap2022.Controllers
{
    public class TimeController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Time model )
        {
            //TODO: Salvar no banco
            return View(model);
        }
    }
}

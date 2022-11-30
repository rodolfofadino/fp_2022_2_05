using fiap2022.api.ActionFilters;
using fiap2022.core.Contexts;
using fiap2022.core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fiap2022.api.Controllers
{
    //[Route("api/times")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("Default")]
    //[CustomAuthorize]
    [Authorize]
    public class TimesController : Controller
    {
        private DataContext _dataContext;

        public TimesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        //[HttpGet]
        //public List<Time> Get()
        //{
        //    return _dataContext.Times.ToList();
        //}


        //[HttpGet]
        //[ProducesResponseType(200, Type = typeof(List<Time>))]
        //[ProducesResponseType(400)]
        ////[ProducesResponseType(200, Type = typeof(List<Time>))]
        //public IActionResult Get()
        //{
        //    return Ok( _dataContext.Times.ToList());
        //}


        [HttpGet]
        public ActionResult<List<Time>> Get()
        {
            //return Ok(_dataContext.Times.ToList());
            //ou
            return _dataContext.Times.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Time>> Get(int id)
        {
            var time = await _dataContext.Times.FirstOrDefaultAsync(x => x.Id == id);

            if (time == null)
                return NotFound();

            return time;
        }

        [HttpPost]
        public async Task<ActionResult<Time>> Post(Time time)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Times.Add(time);
                await _dataContext.SaveChangesAsync();

                return Created($"/api/times/{time.Id}", time);

                //insert no banco
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Time> Put(int id, Time time)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Update(time);
                _dataContext.SaveChanges();
                return time;
            }

            return BadRequest(ModelState);
        }


        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var time = _dataContext.Times.FirstOrDefault(a => a.Id == id);
            if (time == null)
                return NotFound();

            _dataContext.Times.Remove(time);
            _dataContext.SaveChanges();

            return NoContent();

        }

    }
}

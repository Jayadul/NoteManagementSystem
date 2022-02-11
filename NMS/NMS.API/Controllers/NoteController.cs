using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NMS.Model;
using NMS.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private NoteService _noteService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly string DbPath;

        public NoteController(NoteService noteService, IWebHostEnvironment hostEnvironment)
        {
            _noteService = noteService;
            _hostEnvironment = hostEnvironment;
            DbPath = Path.Combine(_hostEnvironment.ContentRootPath, "JsonDb\\NoteDB.json");
        }

        [HttpPost]
        public IActionResult Create(Note model)
        {
            _noteService.Create(model, DbPath);
            return Ok();
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _noteService.DataDeserialize(DbPath);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult GetByUserId(Guid id)
        {
            var data = _noteService.GetByUser(id,DbPath);
            return Ok(data);
        }


        [HttpPut("{id}")]
        public ActionResult Update(Note model)
        {
            var data = _noteService.Update(model, DbPath);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var data = _noteService.Delete(id, DbPath);
            return Ok();
        }
    }
}

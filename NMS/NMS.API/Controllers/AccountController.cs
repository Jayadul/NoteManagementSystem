using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NMS.API.Service;
using NMS.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserService _userService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly string DbPath;

        public AccountController(UserService userService, IWebHostEnvironment hostEnvironment)
        {
            _userService = userService;
            _hostEnvironment = hostEnvironment;
            DbPath = Path.Combine(_hostEnvironment.ContentRootPath, "JsonDb\\UserDB.json");
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            _userService.Create(model, DbPath);
            return Ok();
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _userService.DataDeserialize(DbPath);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult GetByUserId(Guid id)
        {
            var data = _userService.GetByUser(id, DbPath);
            return Ok(data);
        }


        [HttpPut("{id}")]
        public ActionResult Update(User model)
        {
            var data = _userService.Update(model, DbPath);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var data = _userService.Delete(id, DbPath);
            return Ok();
        }
    }
}

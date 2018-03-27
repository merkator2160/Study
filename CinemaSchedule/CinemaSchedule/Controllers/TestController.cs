using CinemaSchedule.BusinessLogic.Models.Exceptions;
using System;
using System.Web.Http;

namespace CinemaSchedule.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public IHttpActionResult ExceptionTest()
        {
            throw new BusinessException("sdfhsdfd");
            throw new Exception("sdfhsdfd");
            return Ok(new Object());
        }
    }
}
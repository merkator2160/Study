using AutoMapper;
using CinemaSchedule.Attributes;
using CinemaSchedule.BusinessLogic.Interfaces;
using CinemaSchedule.BusinessLogic.Models.Exceptions;
using CinemaSchedule.Models.ApiModels;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace CinemaSchedule.Controllers
{
    [ExceptionHandling]
    public class CinemaController : ApiController
    {
        private readonly ICinemaService _cinemaScheduleService;
        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;


        public CinemaController(ICinemaService cinemaScheduleService, IFilmService filmService, IMapper mapper)
        {
            _cinemaScheduleService = cinemaScheduleService;
            _filmService = filmService;
            _mapper = mapper;
        }


        // ACTIONS ////////////////////////////////////////////////////////////////////////////////
        /// <summary>Get all cinemas</summary>
        /// <remarks>This method returns the all found cinemas</remarks>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CinemaAm[]), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorWrapperAm), Description = "Internal Server Error")]
        public IHttpActionResult GetAll()
        {
            var cinemasDto = _cinemaScheduleService.GetAll();
            var cinemasAm = _mapper.Map<CinemaAm[]>(cinemasDto.ToArray());
            return Ok(cinemasAm);
        }

        /// <summary>Get cinema by specified id</summary>
        /// <param name="id">Unique cinema id</param>
        /// <remarks>This method returns the found cinema by providing id</remarks>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CinemaAm), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorWrapperAm), Description = "Bad request")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorWrapperAm), Description = "Internal Server Error")]
        public IHttpActionResult GetById(Int32 id)
        {
            try
            {
                var cinemasDto = _cinemaScheduleService.Get(id);
                var cinemaAm = _mapper.Map<CinemaAm>(cinemasDto);
                return Ok(cinemaAm);
            }
            catch (RequestedObjectNotFoundException ex)
            {
                return Content(HttpStatusCode.BadRequest, new ErrorWrapperAm(ex.Message));
            }

        }
    }
}

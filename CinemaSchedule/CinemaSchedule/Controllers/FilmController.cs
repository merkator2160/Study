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
    public class FilmController : ApiController
    {
        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;


        public FilmController(IFilmService filmService, IMapper mapper)
        {
            _filmService = filmService;
            _mapper = mapper;
        }


        // ACTIONS ////////////////////////////////////////////////////////////////////////////////
        /// <summary>Get all films</summary>
        /// <remarks>This method returns the all avalible films</remarks>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(FilmAm[]), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorWrapperAm), Description = "Internal Server Error")]
        public IHttpActionResult GetAll()
        {
            var filmDtos = _filmService.GetAll();
            var filmAm = _mapper.Map<FilmAm[]>(filmDtos.ToArray());
            return Ok(filmAm);
        }

        /// <summary>Get film by specified id</summary>
        /// <param name="id">Unique film id</param>
        /// <remarks>This method returns the found film by providing id</remarks>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(FilmAm), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorWrapperAm), Description = "Bad request")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorWrapperAm), Description = "Internal Server Error")]
        public IHttpActionResult GetById(Int32 id)
        {
            try
            {
                var filmDto = _filmService.Get(id);
                var filmAm = _mapper.Map<FilmAm>(filmDto);
                return Ok(filmAm);
            }
            catch (RequestedObjectNotFoundException ex)
            {
                return Content(HttpStatusCode.BadRequest, new ErrorWrapperAm(ex.Message));
            }
        }

        /// <summary>Get films by specified cinema id</summary>
        /// <param name="id">Unique cinema id</param>
        /// <remarks>This method returns the all found films by providing cinema id</remarks>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(FilmAm), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorWrapperAm), Description = "Bad request")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorWrapperAm), Description = "Internal Server Error")]
        public IHttpActionResult GetByCinema(Int32 id)
        {
            try
            {
                var filmDtos = _filmService.GetByCinema(id);
                var filmAms = _mapper.Map<FilmAm[]>(filmDtos);
                return Ok(filmAms);
            }
            catch (RequestedObjectNotFoundException ex)
            {
                return Content(HttpStatusCode.BadRequest, new ErrorWrapperAm(ex.Message));
            }
        }
    }
}
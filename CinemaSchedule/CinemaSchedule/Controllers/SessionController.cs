using AutoMapper;
using CinemaSchedule.Attributes;
using CinemaSchedule.BusinessLogic.Interfaces;
using CinemaSchedule.BusinessLogic.Models.Exceptions;
using CinemaSchedule.Models.ApiModels;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace CinemaSchedule.Controllers
{
    [ExceptionHandling]
    public class SessionController : ApiController
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;


        public SessionController(ISessionService sessionService, IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }


        // ACTIONS //////////////////////////////////////////////////////////////////////////////
        /// <summary>Get session by specified id</summary>
        /// <param name="id">Unique session id</param>
        /// <remarks>This method returns the found session by providing id</remarks>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(SessionAm), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorWrapperAm), Description = "Bad request")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorWrapperAm), Description = "Internal Server Error")]
        public IHttpActionResult GetById(Int32 id)
        {
            try
            {
                var sessionDto = _sessionService.Get(id);
                var sessionAm = _mapper.Map<CinemaAm>(sessionDto);
                return Ok(sessionAm);
            }
            catch (RequestedObjectNotFoundException ex)
            {
                return Content(HttpStatusCode.BadRequest, new ErrorWrapperAm(ex.Message));
            }
        }




        /// <summary>Add new session</summary>
        /// <remarks>Add a new session</remarks>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(SessionAm[]), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorWrapperAm), Description = "Bad request")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorWrapperAm), Description = "Internal Server Error")]
        public IHttpActionResult UpdateCinema([FromBody]PostSessionAm cinema)
        {
            try
            {
                //if (!ModelState.IsValid)
                //    return Content(HttpStatusCode.BadRequest, FillErrorWrapper(ModelState));

                //var cinemaDto = _mapper.Map<CinemaDto>(cinema);
                //var validationResult = _cinemaScheduleService.Validate(cinemaDto);
                //if (validationResult.Any())
                //    return Content(HttpStatusCode.BadRequest, FillErrorWrapper(validationResult));

                return Content(HttpStatusCode.OK, new ErrorWrapperAm(false));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new ErrorWrapperAm(ex.Message));
            }
        }

        /// <summary>Create cinema</summary>
        /// <remarks>Create new cinema</remarks>
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(CinemaAm), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(CinemaAm[]), Description = "Bad request")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorWrapperAm), Description = "Internal Server Error")]
        public IHttpActionResult CreateCinema([FromBody]PutSessionAm cinema)
        {
            try
            {
                //if (!ModelState.IsValid)
                //    return Content(HttpStatusCode.BadRequest, FillErrorWrapper(ModelState));

                //var cinemaDto = _mapper.Map<CinemaDto>(cinema);
                //var validationResult = _cinemaScheduleService.Validate(cinemaDto);
                //if (validationResult.Any())
                //    return Content(HttpStatusCode.BadRequest, FillErrorWrapper(validationResult));

                //var createdCinema = _cinemaScheduleService.AddCinema(cinemaDto);
                //var cinemaAm = _mapper.Map<CinemaAm>(createdCinema);
                //return Content(HttpStatusCode.Created, cinemaAm);

                return Content(HttpStatusCode.Created, new Object());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new ErrorWrapperAm(ex.Message));
            }
        }




        /// <summary>Delete session by specified id</summary>
        /// <param name="id">Unique session id</param>
        /// <remarks>This method delete the session by providing id</remarks>
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorWrapperAm), Description = "Bad request")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorWrapperAm), Description = "Internal Server Error")]
        public IHttpActionResult DeleteById(Int32 id)
        {
            try
            {
                _sessionService.Delete(id);
                return Ok();
            }
            catch (NullReferenceException)
            {
                return Content(HttpStatusCode.BadRequest, new ErrorWrapperAm()
                {
                    CommonMessage = $"Session with id = {id} not found"
                });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new ErrorWrapperAm(ex.Message));
            }
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private ErrorWrapperAm FillErrorWrapper(ModelStateDictionary modelState)
        {
            return new ErrorWrapperAm()
            {
                CommonMessage = "There are a number of model validation errors occured, please see the FormErrors collection for more details!",
                FormErrors = modelState.Select(x => new ErrorAm()
                {
                    FieldName = x.Key,
                    Message = x.Value.Value.AttemptedValue
                }).ToArray()
            };
        }
        private ErrorWrapperAm FillErrorWrapper(IReadOnlyDictionary<String, String> validationResult)
        {
            return new ErrorWrapperAm()
            {
                CommonMessage = "There are a number of business logic validation errors occured, please see the FieldErrors collection for more details!",
                FieldErrors = validationResult.Select(x => new ErrorAm()
                {
                    FieldName = x.Key,
                    Message = x.Value
                }).ToArray()
            };
        }
    }
}

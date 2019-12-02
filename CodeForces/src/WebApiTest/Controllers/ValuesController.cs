using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApiTest.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ValuesController : ApiController
    {
        private static readonly List<String> _values;

        static ValuesController()
        {
            _values = new List<String>() { "value1", "value2" };
        }


        // ACTIONS ////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Retrieves all values
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Values take it</response>
        /// <response code="500">Oops! Can't get values right now</response>
        [HttpGet]
        public IEnumerable<String> GetAll()
        {
            return _values;
        }

        /// <summary>
        /// Retrieves a specific value by unique id
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Value founded</response>
        /// <response code="400">Value has missing/invalid values</response>
        /// <response code="500">Oops! Can't get your value right now</response>
        [HttpGet]
        public String Get(Int32 id)
        {
            return _values[id];
        }

        /// <summary>
        /// Add new value
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Value created</response>
        /// <response code="500">Oops! Can't create your value right now</response>
        [HttpPost]
        public void Post([FromBody]String value)
        {
            if (value == null)
                throw new InvalidOperationException($"{nameof(value)} is null!");

            if (_values.Contains(value))
                throw new InvalidOperationException($"{nameof(value)}: {value} is already exists!");

            _values.Add(value);
        }

        /// <summary>
        /// Change value with desired id
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Value changed</response>
        /// <response code="400">Value has missing/invalid values</response>
        /// <response code="500">Oops! Can't update your value right now</response>
        [HttpPut]
        public void Put(Int32 id, [FromBody]String value)
        {
            _values[id] = value;
        }

        /// <summary>
        /// Delete value with desired id
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Value deleted</response>
        /// <response code="400">Value has missing/invalid values</response>
        /// <response code="500">Oops! Can't delete your value right now</response>
        [HttpDelete]
        public void Delete(Int32 id)
        {
            _values.RemoveAt(id);
        }
    }
}

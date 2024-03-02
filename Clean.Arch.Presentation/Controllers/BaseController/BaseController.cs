using Clean.Arch.Helpers.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Clean.Arch.Helpers.Enums;
using System.Collections;

namespace Clean.Arch.Presentation.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult ApiResponse<T>(T obj, string message = "")
        {
            var response = new GlobalHttpResponse((int)HttpStatusCodes.Created, 
                    $"{HttpContext.Request.Method} value with successfuly", obj);

            if ((typeof(T).IsClass || typeof(T) == typeof(List<>)) && obj is null)
            {
                response.StatusCode = (int)HttpStatusCodes.BadRequest;
                response.Message = message;
                return BadRequest(response);
            }
            else if (typeof(T) != typeof(bool))
            {
                response.StatusCode = (int)HttpStatusCodes.OK;
                response.Message = message;
                return Ok(response);
            }

            bool.TryParse(Convert.ToString(obj), out bool result);

            if (typeof(T) == typeof(bool) && !result)
            {
                response.StatusCode = (int)HttpStatusCodes.BadRequest;
                response.Message = message;
                return BadRequest(response);
            }

            return CreatedAtAction(null, null, response);
        }

        protected IActionResult GetResponse<T>(T obj)
        {
            string method = HttpContext.Request.Method;
            var response = new GlobalHttpResponse((int)HttpStatusCodes.OK, $"{method} value with successfuly", obj);

            if (obj is null)
            {
                response.StatusCode = (int)HttpStatusCodes.NotFound;
                response.Message = $"No values found on {method}";
                return NotFound(response);
            }

            if (IsList(typeof(T)))
            {
                var lista = obj as IList;
                if (lista?.Count == 0)
                {
                    response.StatusCode = (int)HttpStatusCodes.NotFound;
                    response.Message = $"No values found on {method}";
                    return NotFound(response);
                }
            }

            return Ok(response);
        }

        private bool IsList(Type obj)
            => obj.IsGenericType && obj.GetGenericTypeDefinition() == typeof(List<>);
    }
}

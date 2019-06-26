using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TriangleClassifier;

namespace TriangleClassifier.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriangleTypeController : Controller
    {

        private readonly ITriangleClassifier _triangleClassifier;

        public TriangleTypeController(ITriangleClassifier triangleClassifier)
        {
            _triangleClassifier = triangleClassifier;
        }

        // GET api/triangletype?a=3&b=4&c=5
        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get([FromQuery] string a, [FromQuery] string b, [FromQuery] string c)
        {
            // TODO there's probably a better way to do validation!
            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b) || string.IsNullOrEmpty(c))
            {
                return new NotFoundResult();
            }
            try
            {
                int ia = int.Parse(a);
                int ib = int.Parse(b);
                int ic = int.Parse(c);
                return Json(_triangleClassifier.TriangleType(ia, ib, ic).ToString()); 
            }
            catch (ArgumentException) 
            {
                var result = new ContentResult();
                result.Content = "\"Error\"";
                result.StatusCode = 200;
                return result;
            }
            catch (Exception ex)
            {
                if (ex is FormatException ||
                    ex is OverflowException ||
                    ex is ArgumentNullException) 
                {
                    var result = new ContentResult();
                    result.Content = @"{""message"":""The request is invalid.""}";
                    result.StatusCode = 400;
                    return result;
                }
                throw;
            }
        }
    }
}

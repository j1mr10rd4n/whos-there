using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fibonacci;

namespace Fibonacci.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FibonacciController : Controller
    {

        private readonly IFibonacciCalculator _fibonacciCalculator;

        public FibonacciController(IFibonacciCalculator fibonacciCalculator)
        {
            _fibonacciCalculator = fibonacciCalculator;
        }

        // GET api/fibonacci?n=42
        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get([FromQuery] string n)
        {
            // TODO there's probably a better way to do validation!
            if (string.IsNullOrEmpty(n))
            {
                return new NotFoundResult();
            }
            try
            {
                long ln = long.Parse(n);
                return Json(_fibonacciCalculator.NthFibonacciNum(ln)); 
            }
            catch (ArgumentException) 
            {
                var result = new ContentResult();
                result.StatusCode = 400;
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

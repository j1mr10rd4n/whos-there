using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordReverser;

namespace WordReverser.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReverseWordsController : Controller
    {

        private readonly IWordReverser _wordReverser;

        public ReverseWordsController(IWordReverser wordReverser)
        {
            _wordReverser = wordReverser;
        }

        // GET api/reversewords?sentence
        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get(string sentence)
        {
            Console.WriteLine("sentence: >>" + sentence + "<<");
            if (String.IsNullOrEmpty(sentence)) {
                sentence = "";
            }
            try
            {
                return Json(_wordReverser.ReverseWords(sentence)); 
            }
            catch (ArgumentException) 
            {
                var result = new ContentResult();
                result.Content = "The resource you are looking for has been removed, had its name changed, or is temporarily unavailable.";
                result.StatusCode = 404;
                return result;
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using DataScraper.DataLib;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataScraper.Api.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private QuotesRepo _quotesRepo;
        public QuotesController(QuotesRepo repo)
        {
            _quotesRepo = repo;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        // GET: api/<QuotesController>
        [HttpGet]
        public ActionResult<IEnumerable<Quote>> Get(int page = 1, int pageSize = 10)
        {
            var quotes = _quotesRepo.GetAll();
            if (quotes.Any())
            {
                var pagedQuotes = quotes
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return Ok(pagedQuotes);
            }
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        // GET api/<QuotesController>/author/{author}
        [HttpGet("author/{author}")]
        public ActionResult<IEnumerable<Quote>> GetByAuthor(string author)
        {
            var quotes = _quotesRepo.FilterByAuthor(author);
            if (quotes.Count == 0)
            {
                return NoContent();
            }
            return Ok(quotes);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        // GET api/<QuotesController>/text/{text}
        [HttpGet("{text}")]
        public ActionResult<IEnumerable<Quote>> GetByText(string text)
        {
            var quotes = _quotesRepo.FilterByText(text);
            if (quotes.Count == 0)
            {
                return NoContent();
            }
            return Ok(quotes);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //run python scraper directly
        [HttpPost("scrape")]
        public IActionResult ScrapeQuotes()
        {
            if (_quotesRepo.RunScraper(out var output, out var error))
            {
                return Ok("Scraper completed!\n" + output);
            }
            return BadRequest("Scraper failed:\n" + error);
        }


        //// GET api/<QuotesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<QuotesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<QuotesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<QuotesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

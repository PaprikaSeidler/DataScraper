using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace DataScraper.DataLib
{
    public class QuotesRepo
    {
        private List<Quote> _quotes = new List<Quote>();

        public QuotesRepo()
        {
            var json = File.ReadAllText("../../quotes.json");
            _quotes = JsonConvert.DeserializeObject<List<Quote>>(json) ?? new List<Quote>();
        }

        public List<Quote> GetAll(int page = 1, int pageSize = 10)
        {
            return _quotes.Select(q => new Quote(q)).ToList();
        }

        public List<Quote> FilterByAuthor(string author)
        {
            return _quotes
                .Where(q => q.Author.Contains(author, StringComparison.OrdinalIgnoreCase))
                .Select(q => new Quote(q))
                .ToList();
        }

        public List<Quote> FilterByText(string text)
        {
            return _quotes
                .Where(q => q.Text.Contains(text, StringComparison.OrdinalIgnoreCase))
                .Select(q => new Quote(q))
                .ToList();
        }
    }
}

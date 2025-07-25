using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        // run python scraper
        public bool RunScraper(out string output, out string error)
        {
            var scriptPath = "../../scraper.py";

            var startInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = scriptPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(startInfo); // Starts the Python script using the Python interpreter
            if (process == null)
            {
                output = "";
                error = "Failed to start the Python process.";
                return false;
            }

            output = process.StandardOutput.ReadToEnd(); // Reads the output from the Python script.
            error = process.StandardError.ReadToEnd(); // Reads the error output from the Python script

            process.WaitForExit();
            return process.ExitCode == 0; 
        }

    }
}

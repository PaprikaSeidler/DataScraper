namespace DataScraper.DataLib
{
    public class Quote
    {
        public string? Text { get; set; }
        public string? Author { get; set; }

        public Quote() { }

        public Quote(Quote copy)
        {
            Text = copy.Text;
            Author = copy.Author;
        }

        public override string ToString()
        {
            return $"'{Text}' - {Author}";
        }
    }
}

const baseUrl = 'http://localhost:5170/api/quotes';

Vue.createApp({
  data() {
    return {
      quotes: [],
      quote: null,
      authorInput: '',
      quoteInput: '',
    };
  },
 mounted() {
        this.getAll();
    },

    methods: {
        async getQuotes(url) {
            try {
                const response = await axios.get(url);
                this.quotes = response.data;
            }
            catch (error) {
                alert(error.message);
            }
        },

        async getAll() {
            await this.getQuotes(baseUrl);
        },

        async sortByAuthor() {
            this.quotes.sort((a, b) => a.author > b.author ? 1 : -1);
            // Sorts quotes by author in ascending order
            // ? 1 : -1 means if a.author is greater than b.author, return 1 (a comes after b), otherwise return -1 (a comes before b)
        },

        async filterByAuthor() {
            if (this.authorInput.trim() === '') {
                await this.getAll();
            } else {
                const url = `${baseUrl}/author/${this.authorInput}`;
                await this.getQuotes(url);
            }
        },

        async searchQuote() {
            if (this.quoteInput.trim() === '') {
                await this.getAll();
            } else {
                const url = `${baseUrl}/quote/${this.quoteInput}`;
                await this.getQuotes(url);
            }
        },

        async runScraper() {
            try {
                await axios.post(`${baseUrl}/scrape`); 
                await this.getAll();
                alert('Scraping completed successfully!');
            } catch (error) {
                alert('Error during scraping: ' + error.message);
            }
        }
  }
}).mount('#app');
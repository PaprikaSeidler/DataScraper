const baseUrl = 'http://localhost:5170/api/quotes';

Vue.createApp({
  data() {
    return {
      quotes: [],
      quote: null,
      authorInput: '',
      quoteInput: '',
      isLoading: false,
      currentPage: 1,
      disableNextPage: false,
      disablePrevPage: true,
      totalPages: 10,
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
            this.currentPage = 1;
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
                const url = `${baseUrl}/${this.quoteInput}`;
                await this.getQuotes(url);
            }
        },

        async resetFilters() {
            this.authorInput = '';
            this.quoteInput = '';
            await this.getAll();
        },

        async runScraper() {
            this.isLoading = true;
            try {
                await axios.post(`${baseUrl}/scrape`); 
                await this.getAll();
                alert('Scraping completed successfully!');
            } catch (error) {
                alert('Error during scraping: ' + error.message);
            } finally {
                this.isLoading = false;
            }
        },

        async nextPage() {
            const nextPage = this.currentPage + 1;
            const url = `${baseUrl}?page=${nextPage}&pageSize=10`;
            await this.getQuotes(url);
            this.currentPage = nextPage; 
            this.disablePrevPage = false;
            if (this.quotes.length === 0) {
                this.disableNextPage = true;    
            }
        },
        async previousPage() {
            const previousPage = this.currentPage - 1;
            if (previousPage < 1) return; // Prevent going to a non-existent page
            const url = `${baseUrl}?page=${previousPage}&pageSize=10`;
            await this.getQuotes(url);
            this.currentPage = previousPage;
            if (this.currentPage === 1) {
                this.disablePrevPage = true;
            }
        }
  }
}).mount('#app');
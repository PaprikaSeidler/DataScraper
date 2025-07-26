# 🧠 Data Scraper

This started as a small Python project to learn about web scraping. It has since grown into a full-stack application with a .NET 8 API and a Vue-based frontend for practice.
The app scrapes quotes from [quotes.toscrape.com](http://quotes.toscrape.com), stores them in a JSON file, and displays them with filtering and pagination.

---

## ✨ Features

- Scrapes quotes (text + author) from multiple pages
- Waits 1 second between requests to be polite
- Saves all quotes to a `quotes.json` file
- .NET 8 Web API to serve and control scraping
- Frontend built with Vue 3 + Bootstrap
- Filter by author or quote text
- Paginated quote list
- Fully integrated: run scraper via button in the browser

---

## 🧱 Tech Stack

### Backend:
- ASP.NET Core 8 Web API
- Python 3.x (runs scraper from backend)
- Newtonsoft.Json

### Scraper:
- `requests` – for fetching pages
- `beautifulsoup4` – for parsing HTML
- `json` (built-in) – for saving quotes

### Frontend:
- Vue 3 (CDN version)
- Bootstrap 4
- Axios – for API calls

---

## 🛡️ About scraping and legality
This project only scrapes quotes.toscrape.com, which is a website specifically designed for practicing web scraping.
I have made sure to respect the site's usage policies and robots.txt, and the scraper waits 1 second between requests to avoid overloading the server.

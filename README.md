# 🧠 Quote Scraper – small Python learning project
This is a tiny Python project I built to learn how web scraping works.  
It collects quotes from quotes.toscrape.com and saves them in a JSON file.

## ✨ What it does
- Goes through all pages on the site
- Grabs each quote's text and author
- Saves the result in a clean `quotes.json` file
- Waits 1 second between requests

## 🔧 Built with
- Python 3.x
- `requests`
- `beautifulsoup4`
- JSON module (built-in)

## 🛡️ About scraping and legality
This project only scrapes quotes.toscrape.com, which is a website specifically designed for practicing web scraping.
I have made sure to respect the site's usage policies and robots.txt, and the scraper waits 1 second between requests to avoid overloading the server.

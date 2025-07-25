import json  # JSON handling library
import time  # For adding delays between requests
import requests # Web scraping library
from bs4 import BeautifulSoup # HTML parsing library
import os  # For file handling


# This script scrapes quotes from the website 'quotes.toscrape.com'
quotesList = []
pageNo = 1  # Initialize page number
while True:
    url = f'https://quotes.toscrape.com/page/{pageNo}/'
    response = requests.get(url)
    soup = BeautifulSoup(response.text, 'html.parser')
    
    quotes = soup.find_all('div', class_='quote')

    for quote in quotes:
        text = quote.find('span', class_='text').get_text()  # Get the quote text
        author = quote.find('small', class_='author').get_text()  # Get the author name
        quotesList.append({'text': text, 'author': author}) # Append to the JSON list
    
    nextBtn = soup.find('li', class_='next') # Check for the next button
    if nextBtn:
        time.sleep(1)  # Sleep for 1 second to avoid overwhelming the server
        pageNo += 1
    else:
        break


# Convert the list to JSON format
quotesJson = json.dumps(quotesList, indent=4)


# Save the JSON data to a file
script_dir = os.path.dirname(os.path.abspath(__file__))  # Get the directory of the script
json_file_path = os.path.join(script_dir, 'quotes.json')  # Create the full path for the JSON file
with open(json_file_path, 'w', encoding='utf-8') as file:
    file.write(quotesJson)  # Write the JSON data to the file
    
print(f"Quotes have been saved to '{json_file_path}'.")  # Confirmation message
print(f'Total quotes scraped: {len(quotesList)}')  # Print the total
print(quotesJson)  # Print the JSON data to the console
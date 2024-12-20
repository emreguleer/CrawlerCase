Project Title:

News Crawler & Elasticsearch News Management

Project Description

This project is developed to fetch recent news from sözcü.com.tr website and manage these news using Elasticsearch. A Docker Compose setup has been used to create and manage an Elasticsearch server. News crawled from the website is stored in Elasticsearch, and old news is removed to maintain up-to-date data. Afterward, Razor Pages is used to fetch and display news with indexed numbers and sorted accordingly. A search feature is also added for easy access.

Features

1.Elasticsearch Usage:

Elasticsearch server is set up using Docker Compose.
Recent news is stored and managed using Elasticsearch.

2.Crawler:

News is fetched from sözcü.com.tr.
Old news is removed to keep only the latest data.

3.Razor Pages for News Management:

A search function is added for searching by title.
News is displayed with an indexed number and sorted accordingly.

4.Docker Compose:

Docker Compose file created to run Elasticsearch server.
Server is set up and integrated with Elasticsearch.

5.Search Functionality:

Users can search for news by title.

Project Screenshots:


Sorted News Display on the Website

![Opera Snapshot_2024-12-20_024850_localhost](https://github.com/user-attachments/assets/0c80db5e-3ec9-4acb-b339-11885a9b5ade)

Searched Data Display

![WhatsApp Görsel 2024-12-20 saat 10 08 37_2f69cc1d](https://github.com/user-attachments/assets/c86156ad-795b-48f1-ab97-baac4b3d478a)

Elasticsearch Docker Compose

![image](https://github.com/user-attachments/assets/74f0b1f3-9384-4868-ab96-12a6c02dfb1e)

Docker Container Setup

![image](https://github.com/user-attachments/assets/1baf9065-d835-4235-9443-a87936f6aa67)

﻿using Business.Abstract;
using Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CrawlerService : ICrawlerService
    {
        private readonly string _url = "https://www.sozcu.com.tr/";
        public async Task<List<Article>> CrawlArticlesAsync()
        {
            List<Article> articles = new List<Article>();
            try
            {
                // HTML içeriğini çek
                HttpClient client = new HttpClient();
                string htmlContent = await client.GetStringAsync(_url);
                // HTML içeriğini parse et
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(htmlContent);
                // Haber başlıklarını seç
                var nodes = document.DocumentNode.SelectNodes("/html/body/div/section/div/div/div/a");
                if (nodes != null)
                {
                    foreach (var node in nodes)
                    {
                        string title = node.InnerText.Trim();
                        var decodedTitle = System.Net.WebUtility.HtmlDecode(title);
                        decodedTitle = Regex.Replace(decodedTitle, @"\s+", " ");
                        if (!string.IsNullOrEmpty(decodedTitle))
                        {
                            articles.Add(new Article { Title = decodedTitle });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Crawl işlemi sırasında hata oluştu: {ex.Message}");
            }
            return articles;
        }
    }
}
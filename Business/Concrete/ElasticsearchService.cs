using Business.Abstract;
using Business.Concrete;
using Entities;
using Nest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
public class ElasticsearchService : IElasticsearchService
{
    private readonly ElasticClient _client;
    private readonly ICrawlerService _crawlerService;
    public ElasticsearchService(ICrawlerService crawlerService)
    {
        var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                       .DefaultIndex("news");
        _client = new ElasticClient(settings);
        _crawlerService = crawlerService;
    }
    // Elasticsearch'e bir liste haber ekler
    public async Task<bool> AddNewsAsync()
    {
        List<Article> articles = new List<Article>();
        articles = await _crawlerService.CrawlArticlesAsync();
        var response = await _client.IndexManyAsync(articles);
        return response.IsValid;
    }
    // Elasticsearch'ten haber listesini alır
    public async Task<List<Article>> GetNewsAsync()
    {
        DeleteIndexAsync();
        var searchResponse = await _client.SearchAsync<Article>(s => s
            .Query(q => q.MatchAll())
            .Size(150)
        );
        return searchResponse.IsValid ? new List<Article>(searchResponse.Documents) : new List<Article>();
    }
    public async Task<bool> DeleteIndexAsync()
    {
        var response = await _client.Indices.DeleteAsync("news");
        return response.IsValid;
    }
    public async Task<List<Article>> SearchNewsAsync(string query)
    {
        var searchResponse = await _client.SearchAsync<Article>(s => s
            .Query(q => q
                .Match(m => m
                    .Field(f => f.Title) // Başlık üzerinden arama yapar
                    .Query(query)
                )
            )
        );

        return searchResponse.IsValid ? new List<Article>(searchResponse.Documents) : new List<Article>();
    }
}
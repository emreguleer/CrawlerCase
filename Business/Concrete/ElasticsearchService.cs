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
        //Elasticsearch port ve index adı belirleme
        var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                       .DefaultIndex("news");
        _client = new ElasticClient(settings);
        _crawlerService = crawlerService;
    }
    // Elasticsearch'e haber listesi ekler
    public async Task<bool> AddNewsAsync(List<Article> articles)
    {
        var response = await _client.IndexManyAsync(articles);
        return response.IsValid;
    }
    // Elasticsearch'ten haber listesini alır
    public async Task<List<Article>> GetNewsAsync()
    {
        
        var searchResponse = await _client.SearchAsync<Article>(s => s
            .Query(q => q.MatchAll())
            .Size(150)
        );
        //Tarnery ile uygunluk kontrolü
        return searchResponse.IsValid ? new List<Article>(searchResponse.Documents) : new List<Article>();
    }
    //Index silme işlemi
    public async Task<bool> DeleteIndexAsync()
    {
        var response = await _client.Indices.DeleteAsync("news");
        return response.IsValid;
    }
    //Aranan haberi indexte bulup getirir
    public async Task<List<Article>> SearchNewsAsync(string query)
    {
        var searchResponse = await _client.SearchAsync<Article>(s => s
            .Query(q => q
                .Match(m => m
                    .Field(f => f.Title) 
                    .Query(query)
                )
            )
        );

        return searchResponse.IsValid ? new List<Article>(searchResponse.Documents) : new List<Article>();
    }
}
using Business.Abstract;
using Business.Concrete;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
public class IndexModel : PageModel
{
    private readonly ICrawlerService _crawlService;
    private readonly IElasticsearchService _elasticsearchService;
    private readonly ICrawlerService _crawlerService;
    public string Message { get; set; }
    public List<Article> Articles { get; set; }
    [BindProperty]
    public string Query { get; set; }
    public IndexModel(ICrawlerService crawlService, IElasticsearchService elasticSearchService, ICrawlerService crawlerService)
    {
        _crawlService = crawlService;
        _elasticsearchService = elasticSearchService;
        _crawlerService = crawlerService;
    }
    public async Task OnGetAsync()
    {
        // Elasticsearch'ten verileri al
        List<Article> currentArticles = await _elasticsearchService.GetNewsAsync();

        // Siteden yeni verileri çek
        var newArticles = await _crawlerService.CrawlArticlesAsync();

        // E?er yeni veriler mevcutsa ve eski verilerle e?le?miyorsa, Elasticsearch güncellenir. Bu sayede veriler güncel kal?r.
        if (newArticles != null && newArticles.Any() && !newArticles.SequenceEqual(currentArticles))
        {
            // Elasticsearch'i sil ve yeni verileri ekle
            await _elasticsearchService.DeleteIndexAsync();
            await _elasticsearchService.AddNewsAsync(newArticles);
            Articles = newArticles;
        }
        else
        {
            // Mevcut verileri kullan
            Articles = currentArticles;
        }
    }

    public async Task<IActionResult> OnPostSearchAsync()
    {
        // Kullan?c?n?n sorgusuna göre Elasticsearch'ten verileri ara
        if (!string.IsNullOrEmpty(Query))
        {
            Articles = await _elasticsearchService.SearchNewsAsync(Query);
        }
        else
        {
            // E?er sorgu bo?sa tüm verileri getir
            Articles = await _elasticsearchService.GetNewsAsync();
        }

        return Page();
    }


}

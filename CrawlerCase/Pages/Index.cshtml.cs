using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
public class IndexModel : PageModel
{
    private readonly ICrawlerService _crawlService;
    private readonly IElasticsearchService _elasticsearchService;
    public string Message { get; set; }
    public List<Article> Articles { get; set; }
    [BindProperty]
    public string Query { get; set; }
    public IndexModel(ICrawlerService crawlService, IElasticsearchService elasticSearchService)
    {
        _crawlService = crawlService;
        _elasticsearchService = elasticSearchService;
    }
    public async Task OnGetAsync()
    {

            var isAdded = await _elasticsearchService.AddNewsAsync();
           
                if (isAdded == false)
                {
                    // Elasticsearch'ten verileri al
                    Articles = await _elasticsearchService.GetNewsAsync();
                }
                else
                    Articles = await _elasticsearchService.GetNewsAsync();

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

using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IElasticsearchService
    {
        Task<bool> AddNewsAsync(List<Article> articles);
        Task<List<Article>> GetNewsAsync();
        Task<bool> DeleteIndexAsync();
        Task<List<Article>> SearchNewsAsync(string query);
    }
}

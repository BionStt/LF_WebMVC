using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public interface ICatalogRepository
    {
        List<CatalogItem> GetAllCatalogItems();

        Task<CatalogItem> GetCatalogItemByIdAsync(int id);
        Task<List<CatalogItem>> GetAllCatalogItemsAsync();

    }
}
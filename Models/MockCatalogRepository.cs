using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class MockCatalogRepository : ICatalogRepository
    {
        public List<CatalogItem> GetAllCatalogItems()
        {
            var cat = new List<CatalogItem>
            {
                new CatalogItem{Id=1, Name="Spectre"},
                new CatalogItem{Id=2, Name="Quantum of Solice"},
                new CatalogItem{Id=3, Name="Casino Royale"}
            };
            return cat;            
        }

        public Task<List<CatalogItem>> GetAllCatalogItemsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<CatalogItem> GetCatalogItemByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }



        //Could also write as...
        /*
        public List<CatalogItem> GetCatalogItemsToo =>
            new List<CatalogItem>
            {
                new CatalogItem{Id=1, Name="Spectre"},
                new CatalogItem{Id=2, Name="Quantum of Solice"},
                new CatalogItem{Id=3, Name="Casino Royale"}
            };
        */
    }

}
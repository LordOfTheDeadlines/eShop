using Catalog.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories.Interfaces
{
    public interface ICatalogRepository
    {
        Task<CategoryAssortment> GetCategoryAssortment(int id);
        Task<IEnumerable<CategoryAssortment>> GetCatalog();
    }
}

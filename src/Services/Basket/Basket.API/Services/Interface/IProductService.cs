using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Services.Interface
{
    public interface IProductService
    {
        Task<Product> GetProduct(int productId);
    }
}

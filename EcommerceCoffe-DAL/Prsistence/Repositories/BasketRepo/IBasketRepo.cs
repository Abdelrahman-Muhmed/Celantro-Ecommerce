using EcommerceCoffe_DAL.Models.BasketModel;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCoffe_DAL.Prsistence.Repositories
{
    public  interface IBasketRepo
    {


        Task<CustomerBasket> GetAsync(string id);

        Task<CustomerBasket> CreateOrUpdateAsync(CustomerBasket basket);

        Task<bool> DeleteAsync(string id);
    }
}

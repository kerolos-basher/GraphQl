using Graph_Ql.Data;
using Graph_Ql.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graph_Ql.Repository
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
           _applicationDbContext = applicationDbContext;
        }
        public IEnumerable<Products> GetAll()
        {
            return _applicationDbContext.products;
        }
    }
}

using Bulky.DataAccess.Respository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialApplication.Data;

namespace Bulky.DataAccess.Respository
{

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
            
        public void Update(Product obj)
        {
            _db.Update(obj);
        }
    }
}

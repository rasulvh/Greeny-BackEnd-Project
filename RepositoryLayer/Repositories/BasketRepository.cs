using DomainLayer.Models;
using RepositoryLayer.Data;
using RepositoryLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class BasketRepository : Repository<Basket>, IBasketRepository
    {
        public BasketRepository(AppDbContext context) : base(context) { }
    }
}

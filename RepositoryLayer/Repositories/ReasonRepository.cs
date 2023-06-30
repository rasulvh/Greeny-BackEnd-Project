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
    public class ReasonRepository : Repository<Reason>, IReasonRepository
    {
        public ReasonRepository(AppDbContext context) : base(context)
        {
        }
    }
}

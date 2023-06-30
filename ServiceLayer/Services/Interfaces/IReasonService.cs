using DomainLayer.Models;
using ServiceLayer.ViewModels.Admin.Contact;
using ServiceLayer.ViewModels.Admin.Reason;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IReasonService
    {
        Task<IEnumerable<Reason>> GetAllAsync();
        Task<Reason> GetByIdAsync(int id);
        Task CreateAsync(ReasonCreateVM request);
        Task DeleteAsync(int id);
        Task EditAsync(int id, ReasonEditVM request);
    }
}

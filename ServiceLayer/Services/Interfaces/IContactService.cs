using DomainLayer.Models;
using ServiceLayer.ViewModels.Admin.Blog;
using ServiceLayer.ViewModels.Admin.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int id);
        Task CreateAsync(ContactCreateVM request);
        Task DeleteAsync(int id);
        Task EditAsync(int id, ContactEditVM request);
    }
}

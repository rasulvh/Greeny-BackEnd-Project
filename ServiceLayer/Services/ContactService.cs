using DomainLayer.Models;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.Admin.Contact;

namespace ServiceLayer.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task CreateAsync(ContactCreateVM request)
        {
            Contact contact = new()
            {
                Text = request.Text,
                Title = request.Title,
            };

            await _contactRepository.CreateAsync(contact);
        }

        public async Task DeleteAsync(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            await _contactRepository.DeleteAsync(contact);
        }

        public async Task EditAsync(int id, ContactEditVM request)
        {
            var contact = await _contactRepository.GetByIdAsync(id);

            contact.Text = request.Text;
            contact.Title = request.Title;

            await _contactRepository.UpdateAsync(contact);
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            return await _contactRepository.GetByIdAsync(id);
        }
    }
}

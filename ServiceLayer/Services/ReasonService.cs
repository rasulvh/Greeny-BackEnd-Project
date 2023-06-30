using DomainLayer.Models;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.Admin.Contact;
using ServiceLayer.ViewModels.Admin.Reason;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ReasonService : IReasonService
    {
        private readonly IReasonRepository _reasonRepository;

        public ReasonService(IReasonRepository reasonRepository)
        {
            _reasonRepository = reasonRepository;
        }

        public async Task CreateAsync(ReasonCreateVM request)
        {
            Reason reason = new()
            {
                Text = request.Text,
                Title = request.Title,
            };

            await _reasonRepository.CreateAsync(reason);
        }

        public async Task DeleteAsync(int id)
        {
            var reason = await _reasonRepository.GetByIdAsync(id);
            await _reasonRepository.DeleteAsync(reason);
        }

        public async Task EditAsync(int id, ReasonEditVM request)
        {
            var reason = await _reasonRepository.GetByIdAsync(id);

            reason.Text = request.Text;
            reason.Title = request.Title;

            await _reasonRepository.UpdateAsync(reason);
        }

        public async Task<IEnumerable<Reason>> GetAllAsync()
        {
            return await _reasonRepository.GetAllAsync();
        }

        public async Task<Reason> GetByIdAsync(int id)
        {
            return await _reasonRepository.GetByIdAsync(id);
        }
    }
}

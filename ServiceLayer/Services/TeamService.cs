using DomainLayer.Models;
using Microsoft.AspNetCore.Hosting;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Helpers;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.Admin.Blog;
using ServiceLayer.ViewModels.Admin.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IWebHostEnvironment _env;

        public TeamService(ITeamRepository teamRepository,
                           IWebHostEnvironment env)
        {
            _teamRepository = teamRepository;
            _env = env;
        }

        public async Task CreateAsync(TeamCreateVM request)
        {
            string image = string.Empty;

            string fileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
            await request.Image.SaveFileAsync(fileName, _env.WebRootPath, "images/team");

            image = fileName;

            Team team = new()
            {
                Image = image,
                About = request.About,
                Name = request.Name,
                Position = request.Position
            };

            await _teamRepository.CreateAsync(team);
        }

        public async Task DeleteAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);

            await _teamRepository.DeleteAsync(team);

            string path = Path.Combine(_env.WebRootPath, "images/team", team.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int id, TeamEditVM request)
        {
            var team = await _teamRepository.GetByIdAsync(id);

            if (request.NewImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + request.NewImage.FileName;
                team.Image = fileName;
                await request.NewImage.SaveFileAsync(fileName, _env.WebRootPath, "images/team");
            }

            team.Name = request.Name;
            team.About = request.About;
            team.Position = request.Position;

            await _teamRepository.UpdateAsync(team);
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _teamRepository.GetAllAsync();
        }

        public async Task<Team> GetByIdAsync(int id)
        {
            return await _teamRepository.GetByIdAsync(id);
        }
    }
}

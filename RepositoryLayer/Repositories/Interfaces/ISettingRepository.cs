using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories.Interfaces
{
    public interface ISettingRepository : IRepository<Setting>
    {
        Dictionary<string, string> GetAllDatas();
        List<Setting> GetAllWithIdsDatas();
    }
}

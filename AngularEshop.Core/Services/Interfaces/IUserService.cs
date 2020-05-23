using AngularEshop.DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<List<User>> GetAllUsers();
    }
}

using System.Linq.Expressions;
using VillaAPI.Models;

namespace VillaAPI.Repository.IRepository
{
    public interface IVillaRepository : IRepository<VillaModel>
    {

  
        Task<VillaModel> UpdateVillaAsync(VillaModel entity);

    }
}

using VillaAPI.Models;

namespace VillaAPI.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumberModel>
    {
        Task<VillaNumberModel> UpdateAsync(VillaNumberModel villaNumber);
    }
}

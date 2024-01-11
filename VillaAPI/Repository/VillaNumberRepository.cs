using VillaAPI.Data;
using VillaAPI.Models;
using VillaAPI.Repository.IRepository;

namespace VillaAPI.Repository
{
    public class VillaNumberRepository : Repository<VillaNumberModel>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _context;

        public VillaNumberRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<VillaNumberModel> UpdateAsync(VillaNumberModel villaNumber)
        {
            villaNumber.UpdatedDate = DateTime.Now;
            _context.villaNumbers.Update(villaNumber);
            await _context.SaveChangesAsync();
            return villaNumber;
        }
    }
}

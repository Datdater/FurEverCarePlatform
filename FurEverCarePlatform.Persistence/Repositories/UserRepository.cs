namespace FurEverCarePlatform.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PetDatabaseContext _context;

        public UserRepository(PetDatabaseContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetByIdAsync(Guid id)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<AppUser> GetByIdWithRelatedDataAsync(Guid id)
        {
            var user = await _context
                .Set<AppUser>()
                .Include(u => u.Orders)
                .Include(u => u.Bookings)
                .Include(u => u.Pets)
                .Include(u => u.Address)
                .Include(u => u.Notifications)
                .Include(u => u.Feedback)
                .Include(u => u.Reports)
                .Include(u => u.Stores)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                Console.WriteLine($"User with ID {id} not found in database.");
            }
            else if (user.IsDeleted)
            {
                Console.WriteLine($"User with ID {id} found but marked as deleted.");
            }

            return user;
        }

        public async Task UpdateAsync(AppUser user)
        {
            _context.AppUsers.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace MinimalApiWithToken.Users
{
    public class UserDB : DbContext
    {
        public UserDB(DbContextOptions<UserDB> options) : base(options) { }

        public DbSet<User> Users => Set<User>();

    }
}

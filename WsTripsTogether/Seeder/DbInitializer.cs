using WsTripsTogether.Model;
using WsTripsTogether.Utils;

namespace WsTripsTogether.Seeder;

public class DbInitializer(Context context)
{
    public async Task InitDb()
    {
        // Add admin user if not present
        if (!context.Users.Any(x => x.IsAdmin))
        {
            await context.Users.AddAsync(new User()
            {
                FirstName = "Lorenzo",
                LastName = "Alessi",
                Username = "lorenzo.alessi",
                Password = "Robot2002!".ConvertToSha512(),
                Email = "alessilorenzo02@gmail.com",
                IsAdmin = true
            });
            await context.SaveChangesAsync();
        }
    }
}
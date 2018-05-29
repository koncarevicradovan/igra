using System.Collections.Generic;

namespace Igra.DAL
{
    public class IgraSeed : System.Data.Entity.DropCreateDatabaseIfModelChanges<IgraContext>
    {
        protected override void Seed(IgraContext context)
        {
            var users = new List<GamingUser>
        {
            new GamingUser{Id= 1,Username= "Julija", Password = "2204", FirstName = "Julija", LastName = "Stefanovic"}
        };

            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Paris2024.Data;

public class DbSeederOfferType
{
    public static async Task SeedOfferTypeData(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
        serviceProvider.GetRequiredService<
            DbContextOptions<ApplicationDbContext>>()))
        {

            if (context.OfferTypes.Any())
            {
                return;
            }

            context.OfferTypes.AddRange(
                new OfferType
                { OfferType_Name = "Solo", OfferType_AllowedPerson = 1 },
                new OfferType
                { OfferType_Name = "Duo", OfferType_AllowedPerson = 2 },
                new OfferType
                { OfferType_Name = "Familiale", OfferType_AllowedPerson = 4 }
            );

            context.SaveChanges();
        }
    }
}

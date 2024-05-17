using Microsoft.EntityFrameworkCore;

namespace Paris2024.Data;

public class DbSeederOffer
{
    public static async Task SeedOfferData(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
        serviceProvider.GetRequiredService<
            DbContextOptions<ApplicationDbContext>>()))
        {

            if (context.Offers.Any())
            {
                return;
            }
            context.Offers.AddRange(
                new Offer
                {
                    Offer_Code = "FBL02 H",
                    Offer_Sport = "Football",
                    Offer_Description = "Argentine vs Maroc éliminatoires",
                    Offer_ImagePath = "0ddfb2fc-169c-4258-b5e7-cb49dfe991be.png",
                    Offer_UnitPrice = 40,
                    OfferTypeId = 1
                },
                new Offer
                {
                    Offer_Code = "FBL03 H",
                    Offer_Sport = "Football",
                    Offer_Description = "Égypte vs République domicaine éliminatoires",
                    Offer_ImagePath = "0ddfb2fc-169c-4258-b5e7-cb49dfe991be.png",
                    Offer_UnitPrice = 40,
                    OfferTypeId = 1
                },
                new Offer
                {
                    Offer_Code = "FBL04 H",
                    Offer_Sport = "Football",
                    Offer_Description = "CAF vs Nouvelle-Zélande éliminatoires",
                    Offer_ImagePath = "0ddfb2fc-169c-4258-b5e7-cb49dfe991be.png",
                    Offer_UnitPrice = 40,
                    OfferTypeId = 1
                },
                new Offer
                {
                    Offer_Code = "FBL05 H",
                    Offer_Sport = "Football",
                    Offer_Description = "AFC1 vs Paraguay éliminatoires",
                    Offer_ImagePath = "0ddfb2fc-169c-4258-b5e7-cb49dfe991be.png",
                    Offer_UnitPrice = 40,
                    OfferTypeId = 1
                },
                new Offer
                {
                    Offer_Code = "FBL08 H",
                    Offer_Sport = "Football",
                    Offer_Description = "France vs États - Unis éliminatoires",
                    Offer_ImagePath = "0ddfb2fc-169c-4258-b5e7-cb49dfe991be.png",
                    Offer_UnitPrice = 40,
                    OfferTypeId = 1
                },

                new Offer
                {
                    Offer_Code = "FBL02 H",
                    Offer_Sport = "Football",
                    Offer_Description = "Argentine vs Maroc éliminatoires",
                    Offer_ImagePath = "0ddfb2fc-169c-4258-b5e7-cb49dfe991be.png",
                    Offer_UnitPrice = 80,
                    OfferTypeId = 2
                },
                new Offer
                {
                    Offer_Code = "FBL03 H",
                    Offer_Sport = "Football",
                    Offer_Description = "Égypte vs République domicaine éliminatoires",
                    Offer_ImagePath = "0ddfb2fc-169c-4258-b5e7-cb49dfe991be.png",
                    Offer_UnitPrice = 80,
                    OfferTypeId = 2
                },
                new Offer
                {
                    Offer_Code = "FBL04 H",
                    Offer_Sport = "Football",
                    Offer_Description = "CAF vs Nouvelle-Zélande éliminatoires",
                    Offer_ImagePath = "0ddfb2fc-169c-4258-b5e7-cb49dfe991be.png",
                    Offer_UnitPrice = 80,
                    OfferTypeId = 2
                },
                new Offer
                {
                    Offer_Code = "FBL05 H",
                    Offer_Sport = "Football",
                    Offer_Description = "AFC1 vs Paraguay éliminatoires",
                    Offer_ImagePath = "0ddfb2fc-169c-4258-b5e7-cb49dfe991be.png",
                    Offer_UnitPrice = 80,
                    OfferTypeId = 2
                },
                new Offer
                {
                    Offer_Code = "FBL08 H",
                    Offer_Sport = "Football",
                    Offer_Description = "France vs États - Unis éliminatoires",
                    Offer_ImagePath = "0ddfb2fc-169c-4258-b5e7-cb49dfe991be.png",
                    Offer_UnitPrice = 80,
                    OfferTypeId = 2
                },

                new Offer
                {
                    Offer_Code = "FBL02 H",
                    Offer_Sport = "Football",
                    Offer_Description = "Argentine vs Maroc éliminatoires",
                    Offer_ImagePath = "0ddfb2fc-169c-4258-b5e7-cb49dfe991be.png",
                    Offer_UnitPrice = 150,
                    OfferTypeId = 2
                },
                new Offer
                {
                    Offer_Code = "FBL03 H",
                    Offer_Sport = "Football",
                    Offer_Description = "Égypte vs République domicaine éliminatoires",
                    Offer_ImagePath = "0ddfb2fc-169c-4258-b5e7-cb49dfe991be.png",
                    Offer_UnitPrice = 150,
                    OfferTypeId = 2
                },
                new Offer
                {
                    Offer_Code = "FBL04 H",
                    Offer_Sport = "Football",
                    Offer_Description = "CAF vs Nouvelle-Zélande éliminatoires",
                    Offer_ImagePath = "0ddfb2fc-169c-4258-b5e7-cb49dfe991be.png",
                    Offer_UnitPrice = 150,
                    OfferTypeId = 2
                },
                new Offer
                {
                    Offer_Code = "FBL05 H",
                    Offer_Sport = "Football",
                    Offer_Description = "AFC1 vs Paraguay éliminatoires",
                    Offer_ImagePath = "0ddfb2fc-169c-4258-b5e7-cb49dfe991be.png",
                    Offer_UnitPrice = 150,
                    OfferTypeId = 2
                },
                new Offer

                {
                    Offer_Code = "FBL08 H",
                    Offer_Sport = "Football",
                    Offer_Description = "France vs États - Unis éliminatoires",
                    Offer_ImagePath = "0ddfb2fc-169c-4258-b5e7-cb49dfe991be.png",
                    Offer_UnitPrice = 150,
                    OfferTypeId = 2
                },


                 new Offer
                 {
                     Offer_Code = "BKB02 H",
                     Offer_Sport = "BasketBall",
                     Offer_Description = "Phase de groupe (1 match) : Vainqueur TQO GRC - Canada",
                     Offer_ImagePath = "27f4ee53-2018-4218-96ba-1a83c1ec5f5c.png",
                     Offer_UnitPrice = 40,
                     OfferTypeId = 1
                 },
                new Offer
                {
                    Offer_Code = "BKB03 H",
                    Offer_Sport = "BasketBall",
                    Offer_Description = "Phase de groupe (1 match) : Vainqueur TQO GRC - Canada",
                    Offer_ImagePath = "27f4ee53-2018-4218-96ba-1a83c1ec5f5c.png",
                    Offer_UnitPrice = 40,
                    OfferTypeId = 1
                },
                new Offer
                {
                    Offer_Code = "BKB04 H",
                    Offer_Sport = "BasketBall",
                    Offer_Description = "Phase de groupe (1 match) : Soudan du Sud - Vainqueur TQO PRI",
                    Offer_ImagePath = "27f4ee53-2018-4218-96ba-1a83c1ec5f5c.png",
                    Offer_UnitPrice = 40,
                    OfferTypeId = 1
                },
                new Offer
                {
                    Offer_Code = "BKB05 H",
                    Offer_Sport = "BasketBall",
                    Offer_Description = " Phase de groupe (1 match) : Serbie - Porto Rico",
                    Offer_ImagePath = "27f4ee53-2018-4218-96ba-1a83c1ec5f5c.png",
                    Offer_UnitPrice = 40,
                    OfferTypeId = 1
                },
                new Offer
                {
                    Offer_Code = "BKB08 H",
                    Offer_Sport = "BasketBall",
                    Offer_Description = "Phase de groupe (2 matchs) : Porto Rico - Espagne, Chine",
                    Offer_ImagePath = "27f4ee53-2018-4218-96ba-1a83c1ec5f5c.png",
                    Offer_UnitPrice = 40,
                    OfferTypeId = 1
                },

                new Offer
                {
                    Offer_Code = "BKB02 H",
                    Offer_Sport = "BasketBall",
                    Offer_Description = "Argentine vs Maroc éliminatoires",
                    Offer_ImagePath = "27f4ee53-2018-4218-96ba-1a83c1ec5f5c.png",
                    Offer_UnitPrice = 80,
                    OfferTypeId = 2
                },
                new Offer
                {
                    Offer_Code = "BKB03 H",
                    Offer_Sport = "BasketBall",
                    Offer_Description = "Égypte vs République domicaine éliminatoires",
                    Offer_ImagePath = "27f4ee53-2018-4218-96ba-1a83c1ec5f5c.png",
                    Offer_UnitPrice = 80,
                    OfferTypeId = 2
                },
                new Offer
                {
                    Offer_Code = "BKB04 H",
                    Offer_Sport = "BasketBall",
                    Offer_Description = "CAF vs Nouvelle-Zélande éliminatoires",
                    Offer_ImagePath = "27f4ee53-2018-4218-96ba-1a83c1ec5f5c.png",
                    Offer_UnitPrice = 80,
                    OfferTypeId = 2
                },
                new Offer
                {
                    Offer_Code = "BKB05 H",
                    Offer_Sport = "BasketBall",
                    Offer_Description = "AFC1 vs Paraguay éliminatoires",
                    Offer_ImagePath = "27f4ee53-2018-4218-96ba-1a83c1ec5f5c.png",
                    Offer_UnitPrice = 80,
                    OfferTypeId = 2
                },
                new Offer
                {
                    Offer_Code = "BKB08 H",
                    Offer_Sport = "BasketBall",
                    Offer_Description = "France vs États - Unis éliminatoires",
                    Offer_ImagePath = "27f4ee53-2018-4218-96ba-1a83c1ec5f5c.png",
                    Offer_UnitPrice = 80,
                    OfferTypeId = 2
                },

                new Offer
                {
                    Offer_Code = "BKB02 H",
                    Offer_Sport = "BasketBall",
                    Offer_Description = "Argentine vs Maroc éliminatoires",
                    Offer_ImagePath = "27f4ee53-2018-4218-96ba-1a83c1ec5f5c.png",
                    Offer_UnitPrice = 150,
                    OfferTypeId = 2
                },
                new Offer
                {
                    Offer_Code = "BKB03 H",
                    Offer_Sport = "BasketBall",
                    Offer_Description = "Égypte vs République domicaine éliminatoires",
                    Offer_ImagePath = "27f4ee53-2018-4218-96ba-1a83c1ec5f5c.png",
                    Offer_UnitPrice = 150,
                    OfferTypeId = 2
                },
                new Offer
                {
                    Offer_Code = "BKB04 H",
                    Offer_Sport = "BasketBall",
                    Offer_Description = "CAF vs Nouvelle-Zélande éliminatoires",
                    Offer_ImagePath = "27f4ee53-2018-4218-96ba-1a83c1ec5f5c.png",
                    Offer_UnitPrice = 150,
                    OfferTypeId = 2
                },
                new Offer
                {
                    Offer_Code = "BKB05 H",
                    Offer_Sport = "BasketBall",
                    Offer_Description = "AFC1 vs Paraguay éliminatoires",
                    Offer_ImagePath = "27f4ee53-2018-4218-96ba-1a83c1ec5f5c.png",
                    Offer_UnitPrice = 150,
                    OfferTypeId = 2
                },
                new Offer

                {
                    Offer_Code = "BKB08 H",
                    Offer_Sport = "BasketBall",
                    Offer_Description = "France vs États - Unis éliminatoires",
                    Offer_ImagePath = "27f4ee53-2018-4218-96ba-1a83c1ec5f5c.png",
                    Offer_UnitPrice = 150,
                    OfferTypeId = 2
                }
            );

            context.SaveChanges();

        }
    }
}

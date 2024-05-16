using RealEstate.Domain.Entiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RealEstate.Reopsitory.Context
{
    public static class EstateContextSeeding
    {
        public static async Task SeedData(EstateContext context)
        {


            if (!context.Set<Category>().Any())
            {
                // Read File
                var categoryData = await File.ReadAllTextAsync(@"..\RealEstate.Reopsitory\Context\SeedData\Category.json");

                // Covert Data int C# objec

                var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);

                //insert Data into DataBase

                if (categories is not null && categories.Any())
                {
                    await context.Set<Category>().AddRangeAsync(categories);
                    await context.SaveChangesAsync();
                }
            }
            if (!context.Set<Property>().Any())
            {
                //Read File

                var propData = await File.ReadAllTextAsync(@"..\RealEstate.Reopsitory\Context\SeedData\Property.json");
                //convert Data into C# object

                var propSerializere = JsonSerializer.Deserialize<List<Property>>(propData);

                //insert the data into database

                if (propSerializere is not null && propSerializere.Any())
                {
                    await context.Set<Property>().AddRangeAsync(propSerializere);
                    await context.SaveChangesAsync();
                }
            }


        }
    }
}

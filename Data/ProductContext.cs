using EBazar.Models;
using Microsoft.EntityFrameworkCore;

namespace EBazar.Data
{
    public class ProductContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> productsType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=Products; Trusted_Connection=True;");
        }

    }
}

using Microsoft.EntityFrameworkCore;

namespace atelier3.Models
{
    public class ApplicationdbContext : DbContext
    {
        public DbSet<Movie>? movies { get; set; }
        public DbSet<Genre> genres { get; set; }
        public DbSet<MembershipType>? membershipTypes { get; set; }
        public DbSet<Customer> customers { get; set; }

        public ApplicationdbContext(DbContextOptions options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
            string GenreJSon = System.IO.File.ReadAllText("Genres.json");
            List<Genre>? genres = System.Text.Json.JsonSerializer.Deserialize<List<Genre>>(GenreJSon);
            //Seed to categorie
            foreach (Genre c in genres)
                model.Entity<Genre>().HasData(c);
        }
    }
}

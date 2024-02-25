using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options){}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Owner> Owners { get; set; }
		public DbSet<Pokemon> Pokemon { get; set; }
		public DbSet<PokemonOwners> PokemonOwners { get; set; }
		public DbSet<PokemonCategories> PokemonCategories { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Reviewer> Reviewers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PokemonCategories>()
					.HasKey(pc => new { pc.PokemonId, pc.CategoryId });
			modelBuilder.Entity<PokemonCategories>()
					.HasOne(p => p.Pokemon)
					.WithMany(pc => pc.PokemonCategories)
					.HasForeignKey(p => p.PokemonId);
			modelBuilder.Entity<PokemonCategories>()
					.HasOne(p => p.Category)
					.WithMany(pc => pc.PokemonCategories)
					.HasForeignKey(c => c.CategoryId);

			modelBuilder.Entity<PokemonOwners>()
					.HasKey(po => new { po.PokemonId, po.OwnerId });
			modelBuilder.Entity<PokemonOwners>()
					.HasOne(p => p.Pokemon)
					.WithMany(pc => pc.PokemonOwners)
					.HasForeignKey(p => p.PokemonId);
			modelBuilder.Entity<PokemonOwners>()
					.HasOne(p => p.Owner)
					.WithMany(pc => pc.PokemonOwners)
					.HasForeignKey(c => c.OwnerId);
		}
	}
}

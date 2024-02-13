using PokemonReviewApp.Models;
using PokemonReviewApp.Data;

namespace PokemonReviewApp
{
	public class Seed
	{
		private readonly DataContext dataContext;
		public Seed(DataContext context)
		{
			this.dataContext = context;
		}
		public void SeedDataContext()
		{
			if (!dataContext.PokemonOwners.Any())
			{
				var pokemonOwners = new List<PokemonOwners>()
				{
					new PokemonOwners()
					{
						Pokemon = new Pokemons()
						{
							Name = "Pikachu",
							BirthDate = new DateTime(1903,1,1),
							PokemonCategories = new List<PokemonCategories>()
							{
								new PokemonCategories { Category = new Categories() { Name = "Electric"}}
							},
							Reviews = new List<Reviews>()
							{
								new Reviews { Title="Pikachu",Text = "Pickahu is the best pokemon, because it is electric", Rating = 5,
								Reviewer = new Reviewers(){ FirstName = "Teddy", LastName = "Smith" } },
								new Reviews { Title="Pikachu", Text = "Pickachu is the best a killing rocks", Rating = 5,
								Reviewer = new Reviewers(){ FirstName = "Taylor", LastName = "Jones" } },
								new Reviews { Title="Pikachu",Text = "Pickchu, pickachu, pikachu", Rating = 1,
								Reviewer = new Reviewers(){ FirstName = "Jessica", LastName = "McGregor" } },
							}
						},
						Owner = new Owners()
						{
							FirstName = "Jack",
							LastName = "London",
							Gym = "Brocks Gym",
							Country = new Countries()
							{
								Name = "Kanto"
							}
						}
					},
					new PokemonOwners()
					{
						Pokemon = new Pokemons()
						{
							Name = "Squirtle",
							BirthDate = new DateTime(1903,1,1),
							PokemonCategories = new List<PokemonCategories>()
							{
								new PokemonCategories { Category = new Categories() { Name = "Water"}}
							},
							Reviews = new List<Reviews>()
							{
								new Reviews { Title= "Squirtle", Text = "squirtle is the best pokemon, because it is electric", Rating = 5,
								Reviewer = new Reviewers(){ FirstName = "Teddy", LastName = "Smith" } },
								new Reviews { Title= "Squirtle",Text = "Squirtle is the best a killing rocks", Rating = 5,
								Reviewer = new Reviewers(){ FirstName = "Taylor", LastName = "Jones" } },
								new Reviews { Title= "Squirtle", Text = "squirtle, squirtle, squirtle", Rating = 1,
								Reviewer = new Reviewers(){ FirstName = "Jessica", LastName = "McGregor" } },
							}
						},
						Owner = new Owners()
						{
							FirstName = "Harry",
							LastName = "Potter",
							Gym = "Mistys Gym",
							Country = new Countries()
							{
								Name = "Saffron City"
							}
						}
					},
									new PokemonOwners()
					{
						Pokemon = new Pokemons()
						{
							Name = "Venasuar",
							BirthDate = new DateTime(1903,1,1),
							PokemonCategories = new List<PokemonCategories>()
							{
								new PokemonCategories { Category = new Categories() { Name = "Leaf"}}
							},
							Reviews = new List<Reviews>()
							{
								new Reviews { Title="Veasaur",Text = "Venasuar is the best pokemon, because it is electric", Rating = 5,
								Reviewer = new Reviewers(){ FirstName = "Teddy", LastName = "Smith" } },
								new Reviews { Title="Veasaur",Text = "Venasuar is the best a killing rocks", Rating = 5,
								Reviewer = new Reviewers(){ FirstName = "Taylor", LastName = "Jones" } },
								new Reviews { Title="Veasaur",Text = "Venasuar, Venasuar, Venasuar", Rating = 1,
								Reviewer = new Reviewers(){ FirstName = "Jessica", LastName = "McGregor" } },
							}
						},
						Owner = new Owners()
						{
							FirstName = "Ash",
							LastName = "Ketchum",
							Gym = "Ashs Gym",
							Country = new Countries()
							{
								Name = "Millet Town"
							}
						}
					}
				};
				dataContext.PokemonOwners.AddRange(pokemonOwners);
				dataContext.SaveChanges();
			}
		}
	}
}
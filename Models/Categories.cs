﻿namespace PokemonReviewApp.Models
{
	public class Categories
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public ICollection<PokemonCategories> PokemonCategories { get; set; }
    }
}
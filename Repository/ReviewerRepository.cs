﻿using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class ReviewerRepository : IReviewerRepository
	{
		private DataContext _context;
		public ReviewerRepository(DataContext context) {
			this._context = context;
		}
		public Reviewer GetReviewer(int id)
		{
			return _context.Reviewers.Where(r => r.Id == id).FirstOrDefault();
		}

		public ICollection<Reviewer> GetReviewers()
		{
			return _context.Reviewers.OrderBy(r => r.Id).ToList();
		}

		public ICollection<Review> GetReviewsByReviewer(int id)
		{
			return _context.Reviews.Where(r => r.Reviewer.Id == id).ToList();
		}

		public bool ReviewerExists(int id)
		{
			return _context.Reviewers.Any(r => r.Id == id);
		}
	}
}

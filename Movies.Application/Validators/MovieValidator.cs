using FluentValidation;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Validators
{
    public class MovieValidator : AbstractValidator<Movie>
    {
        private readonly IMovieRepository _movieRepository;
        public MovieValidator(IMovieRepository _movieRepository) {
            this._movieRepository = _movieRepository;

            RuleFor(m => m.Id)
                .NotEmpty();

            RuleFor(m => m.Genres)
                .NotEmpty();

            RuleFor(m => m.Title)
            .NotEmpty();

            RuleFor(m => m.YearOfRelease)
            .LessThanOrEqualTo(DateTime.UtcNow.Year);

            RuleFor(m => m.Slug)
                .MustAsync(ValidateSlug)
                .WithMessage("A movie with the same title and year of release already exists.");
        }

        private async Task<bool> ValidateSlug(Movie movie, string slug, CancellationToken cancellationToken)
        {
            var existingMovie = await _movieRepository.GetBySlugAsync(slug);
            
            if (existingMovie is not null)
                return existingMovie.Id == movie.Id;

            return existingMovie is null;
        }
    }
}

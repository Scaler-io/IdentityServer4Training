using Movies.API.Entity;

namespace Movies.API.DataAccess
{
    public class MovieContextSeed
    {
        public static void Seed(MovieContext context)
        {
            if (!context.Movies.Any())
            {
                var movies = new List<Movie>
                {
                   new Movie
                    {
                        Id = 1,
                        Title = "The Shawshank Redemption",
                        Genre = "Drama",
                        ReleaseDate = new DateTime(1994, 10, 14),
                        Owner = "Studio A",
                        ImageUrl = "https://example.com/shawshank-redemption.jpg",
                        Rating = "9"
                    },
                    new Movie
                    {
                        Id = 2,
                        Title = "Inception",
                        Genre = "Sci-Fi",
                        ReleaseDate = new DateTime(2010, 7, 16),
                        Owner = "Studio B",
                        ImageUrl = "https://example.com/inception.jpg",
                        Rating = "8"
                    },
                    new Movie
                    {
                        Id = 3,
                        Title = "The Dark Knight",
                        Genre = "Action",
                        ReleaseDate = new DateTime(2008, 7, 18),
                        Owner = "Studio C",
                        ImageUrl = "https://example.com/the-dark-knight.jpg",
                        Rating = "9"
                    },
                    new Movie
                    {
                        Id = 4,
                        Title = "Pulp Fiction",
                        Genre = "Crime",
                        ReleaseDate = new DateTime(1994, 10, 14),
                        Owner = "Studio D",
                        ImageUrl = "https://example.com/pulp-fiction.jpg",
                        Rating = "8"
                    },
                    new Movie
                    {
                        Id = 5,
                        Title = "Forrest Gump",
                        Genre = "Drama",
                        ReleaseDate = new DateTime(1994, 7, 6),
                        Owner = "Studio E",
                        ImageUrl = "https://example.com/forrest-gump.jpg",
                        Rating = "9"
                    }
                };

                context.Movies.AddRange(movies);
                context.SaveChanges();
            }
        }
    }
}
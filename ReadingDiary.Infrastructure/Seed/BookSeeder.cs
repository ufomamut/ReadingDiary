using ReadingDiary.Domain.Entities;
using ReadingDiary.Infrastructure.Data;


/// <summary>
/// Seeds initial test book data for development and demo purposes.
/// Intended for local/testing environments only.
/// </summary>
public static class BookSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db)
    {
        if (db.Books.Any())
            return;

        var random = new Random();

        DateTime RandomDateInPast(int maxDaysBack)
        {
            return DateTime.UtcNow
                .AddDays(-random.Next(1, maxDaysBack))
                .AddMinutes(-random.Next(0, 1440));
        }

        var books = new List<Book>
            {
                
                // J. R. R. TOLKIEN
                new() { Title = "Společenstvo Prstenu", Author = "J. R. R. Tolkien", Year = 1954, Isbn = "9780261103573", CreatedAt = RandomDateInPast(500) },
                new() { Title = "Dvě věže", Author = "J. R. R. Tolkien", Year = 1954, Isbn = "9780261103580", CreatedAt = RandomDateInPast(500) },
                new() { Title = "Návrat krále", Author = "J. R. R. Tolkien", Year = 1955, Isbn = "9780261103597", CreatedAt = RandomDateInPast(500) },
                new() { Title = "Hobit", Author = "J. R. R. Tolkien", Year = 1937, Isbn = "9780261102217", CreatedAt = RandomDateInPast(500) },
                new() { Title = "Silmarillion", Author = "J. R. R. Tolkien", Year = 1977, Isbn = "9780261102736", CreatedAt = RandomDateInPast(500) },

                
                // ISAAC ASIMOV
                new() { Title = "Nadace", Author = "Isaac Asimov", Year = 1951, Isbn = "9780553293357", CreatedAt = RandomDateInPast(500) },
                new() { Title = "Nadace a říše", Author = "Isaac Asimov", Year = 1952, Isbn = "9780553293371", CreatedAt = RandomDateInPast(500) },
                new() { Title = "Druhá Nadace", Author = "Isaac Asimov", Year = 1953, Isbn = "9780553293364", CreatedAt = RandomDateInPast(500) },
                new() { Title = "Já, robot", Author = "Isaac Asimov", Year = 1950, Isbn = "9780553382563", CreatedAt = RandomDateInPast(500) },
                new() { Title = "Ocelové jeskyně", Author = "Isaac Asimov", Year = 1954, Isbn = "9780553293401", CreatedAt = RandomDateInPast(500) },

                
                // ARTHUR C. CLARKE
                new() { Title = "2001: Vesmírná odysea", Author = "Arthur C. Clarke", Year = 1968, Isbn = "9780451457998", CreatedAt = RandomDateInPast(500) },
                new() { Title = "2010: Druhá vesmírná odysea", Author = "Arthur C. Clarke", Year = 1982, Isbn = "9780345303066", CreatedAt = RandomDateInPast(500) },
                new() { Title = "Setkání s Rámou", Author = "Arthur C. Clarke", Year = 1973, Isbn = "9780575073173", CreatedAt = RandomDateInPast(500) },

                
                // DOUGLAS ADAMS
                new() { Title = "Stopařův průvodce po Galaxii", Author = "Douglas Adams", Year = 1979, Isbn = "9780345391803", CreatedAt = RandomDateInPast(500) },
                new() { Title = "Restaurace na konci vesmíru", Author = "Douglas Adams", Year = 1980, Isbn = "9780345391810", CreatedAt = RandomDateInPast(500) },
                new() { Title = "Život, vesmír a vůbec", Author = "Douglas Adams", Year = 1982, Isbn = "9780345391827", CreatedAt = RandomDateInPast(500) },

                
                // SCI-FI / FANTASY MIX
                new() { Title = "Duna", Author = "Frank Herbert", Year = 1965, Isbn = "9780441172719", CreatedAt = RandomDateInPast(500) },
                new() { Title = "Neuromancer", Author = "William Gibson", Year = 1984, Isbn = "9780441569595", CreatedAt = RandomDateInPast(500) },
                new() { Title = "451 stupňů Fahrenheita", Author = "Ray Bradbury", Year = 1953, Isbn = "9781451673319", CreatedAt = RandomDateInPast(500) },

                
                // BIOGRAFIE / NON-FICTION
                new() { Title = "Steve Jobs", Author = "Walter Isaacson", Year = 2011, Isbn = "9781451648539", CreatedAt = RandomDateInPast(500) },
                new() { Title = "Elon Musk", Author = "Ashlee Vance", Year = 2015, Isbn = "9780062301239", CreatedAt = RandomDateInPast(500) },
                new() { Title = "Surely You’re Joking, Mr. Feynman!", Author = "Richard Feynman", Year = 1985, Isbn = "9780393316049", CreatedAt = RandomDateInPast(500) },

                
                // KLASIK
                new() { Title = "1984", Author = "George Orwell", Year = 1949, Isbn = "9780451524935", CreatedAt = RandomDateInPast(500) },
                new() { Title = "Farma zvířat", Author = "George Orwell", Year = 1945, Isbn = "9780451526342", CreatedAt = RandomDateInPast(500) }
            };

        db.Books.AddRange(books);
        await db.SaveChangesAsync();
    }
}
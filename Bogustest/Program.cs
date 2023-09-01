using Bogus;
using CsvHelper;
using System.Globalization;
using System.Text.Json;

namespace Bogustest {
  internal static class Program {
    static Random rand = new Random();
    static string[] ageCertifications = new[] {"G", "PG", "PG-13", "R", "NC-17", "U", "U/A", "A", "S", "AL", "6", "9", "12", "12A", "15", "18", "18R", "R18", "R21", "M", "MA15+", "R16", "R18+", "X18", "T", "E", "E10+", "EC", "C", "CA", "GP", "M/PG", "TV-Y", "TV-Y7", "TV-G", "TV-PG", "TV-14", "TV-MA" };
    static int titleIds = 0;

    static string[] roles = new[] { "Director", "Producer", "Screenwriter", "Actor", "Actress", "Cinematographer", "Film Editor", "Production Designer", "Costume", "Designer", "Music Composer" };
    static int creditIds = 0;

    const int COUNT_OF_TITLES = 120;
    const int MAX_COUNT_OF_CREDITS = 5;

    static void Main(string[] args) {
      var titles = GenerageTitles(COUNT_OF_TITLES);

      var credits = new List<Credit>();
      foreach (var title in titles) {
        var creditsForTitle = GenerateFakeCreditsForTitle(title.Id, rand.Next(1, MAX_COUNT_OF_CREDITS));
        credits.AddRange(creditsForTitle);
      }

      using (var writer = new StreamWriter("titles.csv"))
      using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) {
        csv.Context.RegisterClassMap<TitleMap>();
        csv.WriteRecords(titles);
      }

      using (var writer = new StreamWriter("credits.csv"))
      using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) {
        csv.Context.RegisterClassMap<CreditMap>();
        csv.WriteRecords(credits);
      }
    }

    static List<Titles> GenerageTitles(int titlesCount) {
      var testTitles = new Faker<Titles>()
        .RuleFor(t => t.Id, f => titleIds++)
        .RuleFor(t => t.Title, f => f.Lorem.Word())
        .RuleFor(t => t.Description, f => f.Lorem.Text())
        .RuleFor(t => t.ReleaseYear, f => f.Random.Number(1950, DateTime.Now.Year))
        .RuleFor(t => t.AgeCertification, f => f.PickRandom(ageCertifications))
        .RuleFor(t => t.Runtime, f => f.Random.Number(60, 180))
        .RuleFor(t => t.Genres, f => string.Join(" ", f.Lorem.Words(rand.Next(1, 5))))
        .RuleFor(t => t.ProductionCountry, f => f.Address.Country())
        .RuleFor(t => t.Seasons, f => f.Random.Number(1, 20));

      return testTitles.Generate(titlesCount);
    }

    static List<Credit> GenerateFakeCreditsForTitle(int titleId, int creditsCount) {
      var testCredits = new Faker<Credit>()
        .RuleFor(c => c.Id, f => creditIds++)
        .RuleFor(c => c.TitleId, f => titleId)
        .RuleFor(c => c.RealName, f => f.Name.FullName())
        .RuleFor(c => c.CharacterName, f => f.Name.FirstName())
        .RuleFor(c => c.Role, f => f.PickRandom(roles));

      return testCredits.Generate(creditsCount);
    }
  }
}
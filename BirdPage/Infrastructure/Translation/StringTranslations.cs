namespace BirdPage.Infrastructure.Translation;

public record StringTranslation(string German, string English);

public class StringTranslations
{
    private static Dictionary<string, StringTranslation> _stringTranslationsDict =
        new()
        {
            {
                "OrderBy", new StringTranslation("Sortierung", "Order by")
            },
            {
                "FilterBy", new StringTranslation("Filter", "Filter by")
            },
            {
                "Birds", new StringTranslation("Vögel", "Birds")
            },
            {
                "Tags", new StringTranslation("Tags", "Tags")
            },
            {
                "Location", new StringTranslation("Ort", "Location")
            },
            {
                "Reset", new StringTranslation("Reset", "Reset")
            },
        };
    public static string GetTranslation(TString s, Language language)
    {
        var stringTranslation = _stringTranslationsDict.Where(kvp => kvp.Key == s.Key).Select(kvp => kvp.Value).Single();
        return language switch
        {
            Language.English => stringTranslation.English,
            Language.German => stringTranslation.German,
            _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
        };
    }
}
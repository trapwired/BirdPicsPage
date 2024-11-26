namespace BirdPage.Infrastructure.Translation;

public record SortingCriteriaTranslation(string German, string English);

public class SortingCriteriaTranslations
{
    private static Dictionary<SortingCriteria, SortingCriteriaTranslation> _sortingCriteriaTranslationsDict =
        new()
        {
            {
                SortingCriteria.Newest, new SortingCriteriaTranslation("Neueste", "Newest")
            },
            {
                SortingCriteria.Oldest, new SortingCriteriaTranslation("Älteste", "Oldest")
            },
            {
                SortingCriteria.BirdsAZ, new SortingCriteriaTranslation("Vögel (A-Z)", "Birds (A-Z)")
            },
            {
                SortingCriteria.BirdsZA, new SortingCriteriaTranslation("Vögel (Z-A)", "Birds (Z-A)")
            },
            {
                SortingCriteria.LocationAZ, new SortingCriteriaTranslation("Ort (A-Z)", "Location (A-Z)")
            },
            {
                SortingCriteria.LocationZA, new SortingCriteriaTranslation("Ort (Z-A)", "Location (Z-A)")
            }
        };
    public static string GetTranslation(SortingCriteria sortingCriteria, Language language)
    {
        var stringTranslation = _sortingCriteriaTranslationsDict[sortingCriteria];
        return language switch
        {
            Language.English => stringTranslation.English,
            Language.German => stringTranslation.German,
            _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
        };
    }
}
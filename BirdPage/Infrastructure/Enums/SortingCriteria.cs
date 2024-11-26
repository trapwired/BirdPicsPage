using BirdPage.Infrastructure.Translation;

namespace BirdPage.Infrastructure;

public enum SortingCriteria
{
    Newest,
    Oldest,
    BirdsAZ,
    BirdsZA,
    LocationAZ,
    LocationZA
}

public static class SortingCriteriaExtensions
{
    public static string Translate(this SortingCriteria sortingCriteria, Language language)
    {
        return SortingCriteriaTranslations.GetTranslation(sortingCriteria, language);
    }
}
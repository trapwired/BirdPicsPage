namespace BirdPage.Infrastructure;

public enum Language
{
    Latin,
    German,
    English
}

public record LanguageTuple(Language UiLanguage, Language BirdNameLanguage);

public static class LanguageTuples
{
    public static readonly LanguageTuple GermanGerman = new(Language.German, Language.German);
    public static readonly LanguageTuple GermanLatin = new LanguageTuple(Language.German, Language.Latin);
    public static readonly LanguageTuple EnglishEnglish = new LanguageTuple(Language.English, Language.English);
    public static readonly LanguageTuple EnglishLatin = new LanguageTuple(Language.English, Language.Latin);
}

public static class LanguageTupleExtensions
{
    internal static LanguageTuple Next(this LanguageTuple languageTuple)
    {
        if (languageTuple == LanguageTuples.GermanGerman) return LanguageTuples.GermanLatin;
        if (languageTuple == LanguageTuples.GermanLatin) return LanguageTuples.EnglishEnglish;
        if (languageTuple == LanguageTuples.EnglishEnglish) return LanguageTuples.EnglishLatin;
        if (languageTuple == LanguageTuples.EnglishLatin) return LanguageTuples.GermanGerman;

        return LanguageTuples.GermanGerman;
    }
}
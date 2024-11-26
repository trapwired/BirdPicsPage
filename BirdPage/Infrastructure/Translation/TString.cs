namespace BirdPage.Infrastructure.Translation;

public record TString(string Key);

public static class TStringExtensions
{
    public static string Translate(this TString s, Language language)
    {
        return StringTranslations.GetTranslation(s, language);
    }
}
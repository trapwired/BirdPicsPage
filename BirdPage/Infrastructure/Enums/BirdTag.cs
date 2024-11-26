using BirdPage.Infrastructure.Translation;

namespace BirdPage.Infrastructure;

public enum BirdTag
{
    InTree,
    Sitting,
    OnGround,
    Flying,
    Several,
    Younglings,
    Swimming,
    Eating,
    InBush,
    OnTop,
}

public static class BirdTagExtensions
{
    public static string Translate(this BirdTag birdTag, Language language)
    {
        return BirdTagTranslations.GetTranslation(birdTag, language);
    }
}
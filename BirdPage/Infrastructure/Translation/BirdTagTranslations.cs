namespace BirdPage.Infrastructure.Translation;

public record BirdTagTranslation(string German, string English);

public static class BirdTagTranslations
{
    private static Dictionary<BirdTag, BirdTagTranslation> _birdTagTranslationsDict =
        new()
        {
            {
                BirdTag.Eating, new BirdTagTranslation("Essend", "eating")
            },
            {
                BirdTag.Flying, new BirdTagTranslation("Fliegend", "flying")
            },
            {
                BirdTag.Several, new BirdTagTranslation("Mehrere", "several")
            },
            {
                BirdTag.Sitting, new BirdTagTranslation("Sitzend", "sitting")
            },
            {
                BirdTag.Swimming, new BirdTagTranslation("Schwimmend", "swimming")
            },
            {
                BirdTag.Younglings, new BirdTagTranslation("mit Jungen", "with younglings")
            },
            {
                BirdTag.InBush, new BirdTagTranslation("Im Busch", "in bush")
            },
            {
                BirdTag.InTree, new BirdTagTranslation("Im Baum", "in tree")
            },
            {
                BirdTag.OnGround, new BirdTagTranslation("Auf dem Boden", "on the ground")
            },
            {
                BirdTag.OnTop, new BirdTagTranslation("Zuoberst", "on top")
            }
        };

    public static string GetTranslation(BirdTag birdName, Language language)
    {
        var birdTagTranslation = _birdTagTranslationsDict[birdName];
        return language switch
        {
            Language.English => birdTagTranslation.English,
            Language.German => birdTagTranslation.German,
            _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
        };
    }
}
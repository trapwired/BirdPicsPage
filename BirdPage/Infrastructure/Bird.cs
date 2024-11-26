namespace BirdPage.Infrastructure;

public record Bird(
    string ImgSmall,
    string ImgBig,
    BirdName BirdName,
    string PicName,
    string Location,
    DateTime Date,
    IEnumerable<BirdTag> Tags)
{
    public string Translate(Language language)
    {
        var birdname = BirdName.Translate(language);
        return $"{birdname} ({Location})";
    }
}
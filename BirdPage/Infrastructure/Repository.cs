using System.Collections.Immutable;
using Newtonsoft.Json;

namespace BirdPage.Infrastructure;

public class BirdDescription
{
    public string Filename { get; set; }
    public string BirdName { get; set; }
    public string Location { get; set; }
    public DateTime DateTaken { get; set; }
    public List<string> Tags { get; set; } = new();
}

public class Repository
{
    private IEnumerable<Bird> _birds;

    public Repository()
    {
        _birds = InitializeBirds();
    }

    private IEnumerable<Bird> InitializeBirds()
    {
        string imagesDirectory = "/Users/fluffyoctopus/dev/BirdPage/BirdPage/images";
        var imageDirectorySmall = Path.Combine(imagesDirectory, "small");
        var imageDirectoryBig = Path.Combine(imagesDirectory, "big");
        var descriptions = CreateAndReadDescriptions(imagesDirectory);

        List<Bird> birds = new List<Bird>();

        foreach (var description in descriptions)
        {
            string base64ImageSmall = ImageToBase64(Path.Combine(imageDirectorySmall, description.Filename));
            string base64ImageBig = ImageToBase64(Path.Combine(imageDirectoryBig, description.Filename));
            var newBird = new Bird(
                base64ImageSmall,
                base64ImageBig,
                ParseBirdName(description.BirdName), 
                description.Filename,
                description.Location, 
                description.DateTaken, 
                ParseBirdTags(description.Tags));
            birds.Add(newBird);
        }

        return birds;
    }

    private IEnumerable<BirdTag> ParseBirdTags(List<string> stringTags)
    {
        var birdTags = new List<BirdTag>();
        foreach (var stringTag in stringTags)
        {
            if (Enum.TryParse(stringTag, out BirdTag birdTag))
            {
                birdTags.Add(birdTag);
            }
            else
            {
                Console.WriteLine($"Invalid bird tag: {stringTag}");
            }
            
        }

        return birdTags;
    }

    private BirdName ParseBirdName(string birdNameString)
    {
        if (Enum.TryParse(birdNameString, out BirdName birdName))
        {
            return birdName;
        }

        Console.WriteLine($"Invalid bird name: {birdNameString}");
        return BirdName.None;
    }

    private string ImageToBase64(string imagePath)
    {
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        string base64String = Convert.ToBase64String(imageBytes);
        return $"data:image/jpeg;base64,{base64String}";
    }

    private IEnumerable<BirdDescription> CreateAndReadDescriptions(string imagesDirectory)
    {
        var descriptionFile = Path.Combine(imagesDirectory, "description.json");

        List<BirdDescription> descriptions = [];

        if (File.Exists(descriptionFile))
        {
            var json = File.ReadAllText(descriptionFile);
            descriptions = JsonConvert.DeserializeObject<List<BirdDescription>>(json);
        }

        var jpgFiles = Directory.GetFiles(imagesDirectory, "*.jpg");
        var pngFiles = Directory.GetFiles(imagesDirectory, "*.png");
        var imageFiles = jpgFiles.Concat(pngFiles).ToArray();


        foreach (var imageFile in imageFiles)
        {
            if (descriptions.Exists(d => d.Filename == Path.GetFileName(imageFile)))
                continue;

            var dateTaken = File.GetCreationTime(imageFile);
            var newDescription = new BirdDescription
            {
                Filename = Path.GetFileName(imageFile),
                DateTaken = dateTaken
            };

            descriptions.Add(newDescription);
        }
        var newJson = JsonConvert.SerializeObject(descriptions, Formatting.Indented);
        File.WriteAllText(descriptionFile, newJson);

        return descriptions;
    }

    public ImmutableArray<Bird> GetAll()
    {
        return _birds.OrderByDescending(bird => bird.Date).ToImmutableArray();
    }
}
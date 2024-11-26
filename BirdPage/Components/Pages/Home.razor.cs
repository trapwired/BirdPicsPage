using BirdPage.Infrastructure;
using BirdPage.Infrastructure.Translation;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BirdPage.Components.Pages;

public partial class Home : ComponentBase
{
    [Inject] private Repository Repository { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    private SortingCriteria SortingCriteria { get; set; }

    private bool _overlayVisible;
    private bool _loading = true;

    private IEnumerable<BirdName> _selectedBirdNames { get; set; }

    public IEnumerable<BirdName> SelectedBirdNames
    {
        get => _selectedBirdNames;
        set
        {
            if (_overlayVisible || _loading)
            {
                return;
            }
            _selectedBirdNames = value;
            UpdateBirdNameFiltering();
        }
    }
    
    private IEnumerable<BirdTag> _selectedBirdTags { get; set; }

    public IEnumerable<BirdTag> SelectedBirdTags
    {
        get => _selectedBirdTags;
        set
        {
            if (_overlayVisible || _loading)
            {
                return;
            }
            _selectedBirdTags = value;
            UpdateBirdTagsFiltering();
        }
    }

    private IEnumerable<string> _selectedBirdLocations { get; set; }

    public IEnumerable<string> SelectedBirdLocations
    {
        get => _selectedBirdLocations;
        set
        {
            if (_overlayVisible || _loading)
            {
                return;
            }
            _selectedBirdLocations = value;
            UpdateLocationFiltering();
        }
    }

    private string BirdNameLabel => new TString("Birds").Translate(Language.UiLanguage);
    private string BirdTagLabel => new TString("Tags").Translate(Language.UiLanguage);
    private string LocationLabel => new TString("Location").Translate(Language.UiLanguage);

    private BirdName[] birdnames = [];
    private BirdTag[] birdtags = [];
    private string[] birdlocations = [];

    private LanguageTuple Language = LanguageTuples.GermanGerman;

    private IEnumerable<Bird> UiBirds;
    private IEnumerable<Bird> AllBirds;

    private IEnumerable<Bird> FilteredByBirdName;
    private IEnumerable<Bird> FilteredByTags;
    private IEnumerable<Bird> FilteredByLocation;

    private BirdName? SelectedBirdnameString;
    public Bird CurrentBird { get; set; }

    private static string _languageIcon =
        "<svg stroke=\"#FFFFFF\" stroke-width=\"1\" stroke-linecap=\"square\" stroke-linejoin=\"miter\" fill=\"none\" color=\"#000000\"> <title id=\"languageIconTitle\">Language</title> <circle cx=\"12\" cy=\"12\" r=\"10\"/> <path stroke-linecap=\"round\" d=\"M12,22 C14.6666667,19.5757576 16,16.2424242 16,12 C16,7.75757576 14.6666667,4.42424242 12,2 C9.33333333,4.42424242 8,7.75757576 8,12 C8,16.2424242 9.33333333,19.5757576 12,22 Z\"/> <path stroke-linecap=\"round\" d=\"M2.5 9L21.5 9M2.5 15L21.5 15\"/> </svg>";

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (firstRender)
        {
            _loading = false;
            
            ResetFilters();
            UpdateDropdowns();
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        AllBirds = Repository.GetAll();
        UiBirds = AllBirds;
        FilteredByBirdName = UiBirds;
        FilteredByTags = UiBirds;
        FilteredByLocation = UiBirds;

        CurrentBird = AllBirds.First();
        _overlayVisible = false;

        SelectedBirdnameString = null;
    }

    private void UpdateDropdowns()
    {
        if (_overlayVisible)
        {
            return;
        }
        birdnames = AllBirds.Select(bird => bird.BirdName).ToHashSet().ToArray()
            .OrderBy(bn => bn.Translate(Language.BirdNameLanguage)).ToArray();
        birdtags = AllBirds.SelectMany(bird => bird.Tags).ToHashSet().ToArray()
            .OrderBy(bt => bt.Translate(Language.UiLanguage)).ToArray();
        birdlocations = AllBirds.Select(bird => bird.Location).ToHashSet().ToArray().Order().ToArray();
    }

    private void UpdateBirdNameFiltering()
    {
        FilteredByBirdName = AllBirds.Where(bird => SelectedBirdNames.Contains(bird.BirdName)).ToArray();
        UpdateAndCombineFiltering();
    }

    private void UpdateLocationFiltering()
    {
        FilteredByLocation = AllBirds.Where(bird => SelectedBirdLocations.Contains(bird.Location)).ToArray();
        UpdateAndCombineFiltering();
    }

    private void UpdateBirdTagsFiltering()
    {
        var selectedBirdTagsSet = SelectedBirdTags.ToHashSet();
        FilteredByTags = AllBirds.Where(bird => bird.Tags.Any(tag => selectedBirdTagsSet.Contains(tag))).ToArray();
        UpdateAndCombineFiltering();
    }

    private void UpdateAndCombineFiltering()
    {
        if (_overlayVisible)
        {
            return;
        }
        UiBirds = FilteredByBirdName.Intersect(FilteredByTags).Intersect(FilteredByLocation);
        UpdateSorting();
        StateHasChanged();
    }

    private void UpdateSorting()
    {
        if (_overlayVisible)
        {
            return;
        }
        UiBirds = SortingCriteria switch
        {
            SortingCriteria.Newest => UiBirds.OrderByDescending(bird => bird.Date),
            SortingCriteria.Oldest => UiBirds.OrderBy(bird => bird.Date),
            SortingCriteria.BirdsAZ => UiBirds.OrderBy(bird => bird.BirdName.Translate(Language.BirdNameLanguage)),
            SortingCriteria.BirdsZA => UiBirds.OrderByDescending(bird =>
                bird.BirdName.Translate(Language.BirdNameLanguage)),
            SortingCriteria.LocationAZ => UiBirds.OrderBy(bird => bird.Location),
            SortingCriteria.LocationZA => UiBirds.OrderByDescending(bird => bird.Location),
            _ => UiBirds.OrderBy(bird => bird.Date)
        };

        StateHasChanged();
    }

    private string GetMultiSelectionBirdText(List<string> selectedValues)
    {
        return selectedValues.Any()
            ? $"{selectedValues.Count} birds have been selected"
            : $"No bird has been selected";
    }

    private string GetMultiSelectionTagText(List<string> selectedValues)
    {
        return selectedValues.Any()
            ? $"{selectedValues.Count} tags have been selected"
            : $"No tag has been selected";
    }

    private string GetMultiSelectionLocationText(List<string> selectedValues)
    {
        var count = selectedValues.Count;
        var location = count == 1 ? "location has" : "locations have";
        return selectedValues.Any()
            ? $"{count} {location} been selected"
            : $"No location has been selected";
    }

    private void SwitchLanguage()
    {
        Language = Language.Next();
        UpdateDropdowns();
        StateHasChanged();
    }
    
        
    private void IWantImageClicked()
    {
        Console.WriteLine("I want image clicked");
        HideOverlay();
    }

    private void ShowOverlay(Bird bird)
    {
        _overlayVisible = true;
        CurrentBird = bird;
        StateHasChanged();
    }

    [JSInvokable]
    public void HideOverlay()
    {
        _overlayVisible = false;
        StateHasChanged();
    }

    private void ResetFilters()
    {
        FilteredByBirdName = UiBirds;
        FilteredByTags = UiBirds;
        FilteredByLocation = UiBirds;
        SelectedBirdNames = AllBirds.Select(b => b.BirdName).ToList();
        SelectedBirdLocations = AllBirds.Select(b => b.Location).ToList();
        SelectedBirdTags = AllBirds.SelectMany(b => b.Tags).ToList();
        UpdateSorting();
    }
}
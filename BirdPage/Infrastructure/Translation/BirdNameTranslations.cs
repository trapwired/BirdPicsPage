namespace BirdPage.Infrastructure.Translation;

public record BirdNameTranslation(string Latin, string German, string English);

public static class BirdNameTranslations
{
    private static Dictionary<BirdName, BirdNameTranslation> _birdNameTranslationsDict =
        new()
        {
            {
                BirdName.PicusViridus,
                new BirdNameTranslation("Picus viridus", "Grünspecht", "European green woodpecker")
            },
            {
                BirdName.MilvusMilvus,
                new BirdNameTranslation("Milvus milvus", "Rotmilan", "Red kite")
            },
            {
                BirdName.ArdeaCinerea,
                new BirdNameTranslation("Ardea cinerea", "Graureiher", "Grey heron")
            },
            {
                BirdName.NettaRufina,
                new BirdNameTranslation("Netta rufina", "Kolbenente", "Red-crested pochard")
            },
            {
                BirdName.CygnusOlor,
                new BirdNameTranslation("Cygnus olor", "Höckerschwan", "Mute swan")
            },
            {
                BirdName.CyanistesCaeruleus,
                new BirdNameTranslation("Cyanistes caeruleus", "Blaumeise", "Eurasian blue tit")
            },
            {
                BirdName.PasserDomesticus,
                new BirdNameTranslation("Passer domesticus", "Hausspatz", "House sparrow")
            },
            {
                BirdName.FalcoTinnunculus,
                new BirdNameTranslation("Falco tinnunculus", "Turmfalke", "Common kestrel")
            },
            {
                BirdName.SturnusVulgaris,
                new BirdNameTranslation("Sturnus vulgaris", "Gemeiner Star", "Common starling")
            },
            {
                BirdName.PodicepsCristatus,
                new BirdNameTranslation("Podiceps cristatus", "Haubentaucher", "Great crested grebe")
            },
            {
                BirdName.PhalacrocoraxCarbo,
                new BirdNameTranslation("Phalacrocorax carbo", "Kormoran", "Great cormorant")
            },
            {
                BirdName.AcrocephalusSchoenobaenus,
                new BirdNameTranslation("Acrocephalus schoenobaenus", "Schilfrohrsänger", "Sedge Warbler")
            },
            {
                BirdName.SylviaAtricapilla,
                new BirdNameTranslation("Sylvia atricapilla", "Mönchsgrasmücke", "Eurasian blackcap")
            },
            {
                BirdName.StreptopeliaDecaocto,
                new BirdNameTranslation("Streptopelia decaocto", "Türkentaube", "Eurasian collared dove")
            },
            {
                BirdName.CarduelisCarduelis,
                new BirdNameTranslation("Carduelis carduelis", "Stieglitz", "European goldfinch")
            },
            {
                BirdName.ButeoButeo,
                new BirdNameTranslation("Buteo buteo", "Mäusebussard", "Common buzzard")
            },
            {
                BirdName.TurdusMerula,
                new BirdNameTranslation("Turdus merula", "Amsel", "Common blackbird")
            },
            {
                BirdName.TroglodytesAedon,
                new BirdNameTranslation("Troglodytes aedon", "Hauszaunkönig", "House wren")
            },
            {
                BirdName.FringillaCoelebs,
                new BirdNameTranslation("Fringilla coelebs", "Buchfink", "Eurasian chaffinch")
            },
            {
                BirdName.ParusMajor,
                new BirdNameTranslation("Parus major", "Kohlmeise", "Great tit")
            },
            {
                BirdName.ErithacusRubecula,
                new BirdNameTranslation("Erithacus rubecula", "Rotkehlchen", "European robin")
            },
            {
                BirdName.AixGalericulata,
                new BirdNameTranslation("Aix galericulata", "Mandarinente", "Mandarin duck")
            },
            {
                BirdName.TurdusPilaris,
                new BirdNameTranslation("Turdus pilaris", "Wachholderdrossel", "Fieldfare")
            },
            {
                BirdName.PeriparusAter,
                new BirdNameTranslation("Periparus ater", "Tannenmeise", "Coal tit")
            },
            {
                BirdName.None,
                new BirdNameTranslation("Unknown", "Unbekannt", "Unknown")
            },
        };

    public static string GetTranslation(BirdName birdName, Language language)
    {
        var birdnameTranslation = _birdNameTranslationsDict[birdName];
        return language switch
        {
            Language.Latin => birdnameTranslation.Latin,
            Language.English => birdnameTranslation.English,
            Language.German => birdnameTranslation.German,
            _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
        };
    }
}
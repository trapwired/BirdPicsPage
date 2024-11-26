using BirdPage.Infrastructure.Translation;

namespace BirdPage.Infrastructure;

public enum BirdName
{
    PicusViridus,
    MilvusMilvus,
    ArdeaCinerea,
    NettaRufina,
    CygnusOlor,
    CyanistesCaeruleus,
    PasserDomesticus,
    FalcoTinnunculus,
    SturnusVulgaris,
    PodicepsCristatus,
    PhalacrocoraxCarbo,
    AcrocephalusSchoenobaenus,
    SylviaAtricapilla,
    StreptopeliaDecaocto,
    CarduelisCarduelis,
    ButeoButeo,
    TurdusMerula,
    TroglodytesAedon,
    FringillaCoelebs,
    ParusMajor,
    ErithacusRubecula,
    AixGalericulata,
    TurdusPilaris,
    PeriparusAter,
    None,
}

public static class BirdNameExtensions
{
    public static string Translate(this BirdName birdName, Language language)
    {
        return BirdNameTranslations.GetTranslation(birdName, language);
    }
}
using System.Collections.Generic;
using Verse;

namespace AgeUpGenes;

public class Choices : IExposable
{
    public GeneClassification selectedGene;
    public List<GeneClassification> geneChoices;
    public GeneType? type;
    public bool ShouldDoChoice = Rand.Chance(AgeUpGenesMod.settings.GeneEventChance);

    public void ExposeData()
    {
        Scribe_Deep.Look(ref selectedGene, "selectedGene");
        Scribe_Collections.Look(ref geneChoices, "geneChoices", LookMode.Deep);
        Scribe_Values.Look(ref type, "type");
        Scribe_Values.Look(ref ShouldDoChoice, "ShouldDoChoice");
    }
}

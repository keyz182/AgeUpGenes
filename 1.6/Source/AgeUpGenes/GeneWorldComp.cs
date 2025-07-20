using System.Collections.Generic;
using System.Linq;
using RimWorld.Planet;
using Verse;

namespace AgeUpGenes;

public class GeneWorldComp(World world) : WorldComponent(world)
{
    public static GeneWorldComp Instance => Find.World.GetComponent<GeneWorldComp>();

    public Dictionary<Pawn, Choices> ChoiceLookup = new();

    private List<Pawn> pawnsKeys = [];
    private List<Choices> choicesValues = [];

    public override void ExposeData()
    {
        if (Scribe.mode == LoadSaveMode.LoadingVars || Scribe.mode == LoadSaveMode.PostLoadInit || Scribe.mode == LoadSaveMode.ResolvingCrossRefs)
        {
            if(pawnsKeys.Count <= 0)
                Scribe_Collections.Look(ref pawnsKeys, "pawns", LookMode.Reference);
            if(choicesValues.Count <= 0)
                Scribe_Collections.Look(ref choicesValues, "choices", LookMode.Deep);
            if (pawnsKeys.Count == choicesValues.Count && choicesValues.Count > 0)
            {
                ChoiceLookup = pawnsKeys.Zip(choicesValues, (pawn, choices) => new { pawn, choices }).ToDictionary(x => x.pawn, x => x.choices);
            }
        }

        if (Scribe.mode == LoadSaveMode.Saving)
        {
            pawnsKeys = ChoiceLookup.Keys.ToList();
            choicesValues = ChoiceLookup.Values.ToList();
            Scribe_Collections.Look(ref pawnsKeys, "pawns", LookMode.Reference);
            Scribe_Collections.Look(ref choicesValues, "choices", LookMode.Deep);

        }
    }
}

using RimWorld;

namespace AgeUpGenes;

[DefOf]
public static class AgeUpGenesDefOf
{
    // Remember to annotate any Defs that require a DLC as needed e.g.
    // [MayRequireBiotech]
    // public static GeneDef YourPrefix_YourGeneDefName;
    
    static AgeUpGenesDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(AgeUpGenesDefOf));
}

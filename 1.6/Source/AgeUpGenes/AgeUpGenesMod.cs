using HarmonyLib;
using UnityEngine;
using Verse;

namespace AgeUpGenes;

public class AgeUpGenesMod : Mod
{
    public static Settings settings;

    public AgeUpGenesMod(ModContentPack content) : base(content)
    {
        Log.Message("Hello world from AgeUpGenes");

        // initialize settings
        settings = GetSettings<Settings>();
#if DEBUG
        Harmony.DEBUG = true;
#endif
        Harmony harmony = new("keyz182.rimworld.AgeUpGenes.main");	
        harmony.PatchAll();
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        base.DoSettingsWindowContents(inRect);
        settings.DoWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return "AgeUpGenes_SettingsCategory".Translate();
    }
}

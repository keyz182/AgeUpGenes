using UnityEngine;
using Verse;

namespace AgeUpGenes;

public class Settings : ModSettings
{
    public float GeneEventChance = 1f;

    public float GoodGeneChance = 1f / 4f;
    public float BadGeneChance = 1f / 4f;
    public float NeutralGeneChance = 1f / 4f;
    public float RandomGeneChance = 1f / 4f;

    private float ScrollViewHeight = 0;
    public Vector2 scrollPosition = Vector2.zero;

    public void AdjustChanceRatios(float change, ref float chanceA, ref float chanceB, ref float chanceC)
    {
        float total = chanceA + chanceB + chanceC;
        float aRatio = chanceA / total;
        float bRatio = chanceB / total;
        float cRatio = chanceC / total;

        chanceA += change * aRatio;
        chanceB += change * bRatio;
        chanceC += change * cRatio;
    }

    public void DoWindowContents(Rect wrect)
    {
        Rect viewRect = new(0, 0, wrect.width - 20, ScrollViewHeight);
        ScrollViewHeight = 0;
        scrollPosition = GUI.BeginScrollView(new Rect(0, 50, wrect.width, wrect.height - 50), scrollPosition, viewRect);

        Listing_Standard options = new();
        options.Begin(viewRect);


        GeneEventChance = options.SliderLabeled("AUP_FP_GeneEventChance".Translate(GeneEventChance * 100), GeneEventChance, 0f, 1f, tooltip: "AUP_FP_GeneEventChance_Tooltip");
        ScrollViewHeight += 30f;

        float GoodGeneChanceUpd = options.SliderLabeled("AUP_FP_GoodGeneChance".Translate(GoodGeneChance * 100), GoodGeneChance, 0f, 1f, tooltip: "AUP_FP_GoodGeneChance_Tooltip");
        ScrollViewHeight += 30f;
        float BadGeneChanceUpd = options.SliderLabeled("AUP_FP_BadGeneChance".Translate(BadGeneChance * 100), BadGeneChance, 0f, 1f, tooltip: "AUP_FP_BadGeneChance_Tooltip");
        ScrollViewHeight += 30f;
        float NeutralGeneChanceUpd = options.SliderLabeled(
            "AUP_FP_NeutralGeneChance".Translate(NeutralGeneChance * 100),
            NeutralGeneChance,
            0f,
            1f,
            tooltip: "AUP_FP_NeutralGeneChance_Tooltip"
        );
        ScrollViewHeight += 30f;
        float RandomGeneChanceUpd = options.SliderLabeled(
            "AUP_FP_RandomGeneChance".Translate(RandomGeneChance * 100),
            RandomGeneChance,
            0f,
            1f,
            tooltip: "AUP_FP_RandomGeneChance_Tooltip"
        );
        ScrollViewHeight += 30f;

        if (!Mathf.Approximately(GoodGeneChance, GoodGeneChanceUpd))
        {
            AdjustChanceRatios(GoodGeneChance - GoodGeneChanceUpd, ref BadGeneChance, ref NeutralGeneChance, ref RandomGeneChance);
            GoodGeneChance = GoodGeneChanceUpd;
        }
        else if (!Mathf.Approximately(BadGeneChance, BadGeneChanceUpd))
        {
            AdjustChanceRatios(BadGeneChance - BadGeneChanceUpd, ref GoodGeneChance, ref NeutralGeneChance, ref RandomGeneChance);
            BadGeneChance = BadGeneChanceUpd;
        }
        else if (!Mathf.Approximately(NeutralGeneChance, NeutralGeneChanceUpd))
        {
            AdjustChanceRatios(NeutralGeneChance - NeutralGeneChanceUpd, ref GoodGeneChance, ref BadGeneChance, ref RandomGeneChance);
            NeutralGeneChance = NeutralGeneChanceUpd;
        }
        else if (!Mathf.Approximately(RandomGeneChance, RandomGeneChanceUpd))
        {
            AdjustChanceRatios(RandomGeneChance - RandomGeneChanceUpd, ref GoodGeneChance, ref BadGeneChance, ref NeutralGeneChance);
            RandomGeneChance = RandomGeneChanceUpd;
        }


        options.End();
    }

    public override void ExposeData()
    {
        Scribe_Values.Look(ref GeneEventChance, "GeneEventChance", 1f);
        Scribe_Values.Look(ref GoodGeneChance, "GoodGeneChance", 1f / 4f);
        Scribe_Values.Look(ref BadGeneChance, "BadGeneChance", 1f / 4f);
        Scribe_Values.Look(ref NeutralGeneChance, "NeutralGeneChance", 1f / 4f);
        Scribe_Values.Look(ref RandomGeneChance, "RandomGeneChance", 1f / 4f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCUpgradeChances
{
    NPCIntelligence intel;

    public NPCUpgradeChances(NPCIntelligence i)
    {
        intel = i;
    }

    public float First()
    {
        // Chances are on a scale of 0-12

        if (intel == NPCIntelligence.Idiot)
        {
            return 1f;
        }
        else if (intel == NPCIntelligence.Silly)
        {
            return 2f;
        }
        else if (intel == NPCIntelligence.Average)
        {
            return 4f;
        }
        else if (intel == NPCIntelligence.Bright)
        {
            return 7f;
        }
        else if (intel == NPCIntelligence.Genius)
        {
            return 9f;
        }
        else
        {
            return 3f;
        }
    }

    public float Second()
    {
        // Chances are on a scale of 0-12

        if (intel == NPCIntelligence.Idiot)
        {
            return 3f;
        }
        else if (intel == NPCIntelligence.Silly)
        {
            return 5f;
        }
        else if (intel == NPCIntelligence.Average)
        {
            return 8f;
        }
        else if (intel == NPCIntelligence.Bright)
        {
            return 10f;
        }
        else if (intel == NPCIntelligence.Genius)
        {
            return 11f;
        }
        else
        {
            return 6f;
        }
    }

    public float Third()
    {
        // Chances are on a scale of 0-12

        if (intel == NPCIntelligence.Idiot)
        {
            return 12f;
        }
        else if (intel == NPCIntelligence.Silly)
        {
            return 12f;
        }
        else if (intel == NPCIntelligence.Average)
        {
            return 12f;
        }
        else if (intel == NPCIntelligence.Bright)
        {
            return 12f;
        }
        else if (intel == NPCIntelligence.Genius)
        {
            return 12f;
        }
        else
        {
            return 9f;
        }
    }
}

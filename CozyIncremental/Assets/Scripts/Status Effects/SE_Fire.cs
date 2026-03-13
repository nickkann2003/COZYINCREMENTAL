using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Fire : StatusEffect
{
    private float fireBaseValue = 0.1f;
    public SE_Fire(Bouba b, StatusEffectMods m = null, float initialStacks = 0) : base(b, m, initialStacks)
    {
        // Set name, description, and sprite
        displayName = "Fire";
        displayDescription = "Deals burning damage over time";
        displaySprite = Resources.Load<Sprite>("Art/Icons/Status Effect Icons/FireIcon");
    }

    protected override void Trigger()
    {
        bouba.BoubaProc(stacks*fireBaseValue);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect
{
    public Bouba bouba;
    public float stacks;
    public StatusEffectMods mods;

    private float rDecayTime;

    public StatusEffect(Bouba b, StatusEffectMods m = null, float initialStacks = 0)
    {
        mods = m;
        if(mods == null)
            mods = new StatusEffectMods();

        bouba = b;

        stacks = initialStacks;
    }

    public void AddStacks(float stacksToAdd)
    {
        if(mods.maxStacks > stacks)
        {
            float add = stacksToAdd * mods.stackMultiplier + mods.flatStackAddition;
            if(mods.maxStacks < stacks + add)
            {
                stacks = mods.maxStacks;
            }
        }

        // Reset decay time when stacking is attempted
        rDecayTime = mods.decayTime;
    }

    public void UpdateTick(float deltaTime)
    {
        // If tick triggers
        if (mods.Tick(deltaTime))
        {
            Trigger();
        }
    }

    /// <summary>
    /// Runs whenever a tick of this is triggered
    /// </summary>
    protected virtual void Trigger()
    {

    }

}

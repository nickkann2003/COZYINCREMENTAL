using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class StatusEffect
{
    public Bouba bouba;
    public float stacks;
    public StatusEffectMods mods;

    public string displayName;
    public string displayDescription;
    public Sprite displaySprite;

    private float rDecayTime;

    private float secondDecayTime;

    public bool active
    {
        get { return stacks > 0; }
    }

    public StatusEffect(Bouba b, StatusEffectMods m = null, float initialStacks = 0)
    {
        mods = m;
        if (mods == null)
            mods = new StatusEffectMods();

        bouba = b;

        stacks = initialStacks;

        displayName = "base status effect";
        displayDescription = "base status effect description";
        displaySprite = null;
    }

    public void AddStacks(float stacksToAdd)
    {
        if (mods.maxStacks > stacks || mods.maxStacks == -1)
        {
            float add = stacksToAdd * mods.stackMultiplier + mods.flatStackAddition;
            if(mods.maxStacks != -1)
            {
                if (mods.maxStacks < stacks + add)
                {
                    stacks = mods.maxStacks;
                }
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
            TickDecay();
        }

        // If full second has passed
        if (secondDecayTime <= 0)
        {
            TimeDecay();
            secondDecayTime = 1f;
        }

        // If rDecay time is less than 0, remove all stacks
        if (rDecayTime <= 0)
        {
            stacks = 0;
        }

        secondDecayTime -= deltaTime;
        rDecayTime -= deltaTime;
    }

    /// <summary>
    /// Runs whenever a tick of this is triggered
    /// </summary>
    protected virtual void Trigger()
    {

    }

    /// <summary>
    /// Runs logic to decay stacks on tick
    /// </summary>
    protected virtual void TickDecay()
    {
        stacks -= mods.pDecayPerTick * stacks + mods.flatDecayPerTick;
        if (stacks < 1)
        {
            stacks = 0;
        }
    }

    /// <summary>
    /// Runs logic to decay stacks on a once-per-second basis
    /// </summary>
    protected virtual void TimeDecay()
    {
        stacks -= mods.pDecayPerSecond * stacks + mods.flatDecayPerSecond;
        if(stacks < 1)
        {
            stacks = 0;
        }
    }

}

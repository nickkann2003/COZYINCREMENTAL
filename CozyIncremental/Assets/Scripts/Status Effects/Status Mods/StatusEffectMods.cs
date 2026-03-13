using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Status Effect Mod")]
public class StatusEffectMods : ScriptableObject
{

    // ------------------------------ Stack Decay ----------------------------------
    [Header("Stack Decay")]
    /// <summary>
    /// Flat stack loss per tick of effect
    /// </summary>
    public float flatDecayPerTick;

    /// <summary>
    /// Percent stack loss per tick of effect
    /// </summary>
    public float pDecayPerTick;

    /// <summary>
    /// Flat decay per second
    /// </summary>
    public float flatDecayPerSecond;

    /// <summary>
    /// Percentage of stacks decay per second
    /// </summary>
    public float pDecayPerSecond;

    /// <summary>
    /// Time until all stacks decay, if not refreshed
    /// </summary>
    public float decayTime;

    /// <summary>
    /// Decay time per stack, ONLY TRACKED IF STACKS DON'T REFRESH
    /// </summary>
    public float perStackDecayTime;

    // ------------------------------- Stack Refresh -----------------------------------
    [Header("Stack Refresh")]
    /// <summary>
    /// True if new stacks refresh old
    /// </summary>
    public bool stacksRefresh;

    /// <summary>
    /// If this status effect can have more than one stack
    /// </summary>
    public bool canStack;

    // ---------------------------- Stack Maximums ---------------------------------
    [Header("Stack Maximum")]
    /// <summary>
    /// Maximum stacks allowed, -1 if uncapped
    /// </summary>
    public float maxStacks = -1f;

    // ---------------------------- Stack Addition ---------------------------------
    [Header("Stack Addition")]
    /// <summary>
    /// Base stacks per time this status effect is applied, before multipliers and flat additions
    /// </summary>
    public float baseStacksPerApplication = 1f;

    /// <summary>
    /// Multiplier for applied stacks
    /// </summary>
    public float stackMultiplier = 1f;

    /// <summary>
    /// Flat stacks added when any amount of stacks are added
    /// </summary>
    public float flatStackAddition;

    // --------------------------- Status Effect Tick Variables --------------------------------
    [Header("Tick Variables")]
    /// <summary>
    /// Cooldown between ticking effects of this status effect
    /// </summary>
    public float tickCooldown = 1f;
    public float currentTickCooldown;

    /// <summary>
    /// Amount of time this status effect triggers per tick
    /// </summary>
    public float triggersPerTick = 1f;

    /// <summary>
    /// Multiplier to deltaTime for tick cooldown
    /// </summary>
    public float tickCooldownRate = 1f;

    /// <summary>
    /// Multiplier for the maximum cooldown
    /// </summary>
    public float tickMaxCooldownMultiplier=1f;

    // ---------------------------- Functions ----------------------------
    /// <summary>
    /// Updates this status effect mod by deltaTime, returning True if a tick happened this frame
    /// </summary>
    /// <param name="deltaTime">Time between frames</param>
    /// <returns>True if tick happened</returns>
    public bool Tick(float deltaTime)
    {
        currentTickCooldown -= deltaTime * tickCooldownRate;
        if(currentTickCooldown < 0)
        {
            currentTickCooldown = tickCooldown*tickMaxCooldownMultiplier;
            return true;
        }
        return false;
    }
}

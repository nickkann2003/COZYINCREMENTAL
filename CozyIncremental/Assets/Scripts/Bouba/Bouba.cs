using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bouba : MonoBehaviour
{
    public int boubaEarned;
    private float boubaPrivate;

    private float boubaPerClick;

    // Leveling stats
    [SerializeField]
    private float nextLevel = 10f;
    [SerializeField]
    private float levelProgress = 0;
    [SerializeField]
    private int level = 0;
    [SerializeField]
    private int skillPoints = 0;
    [SerializeField]
    public List<LevelReward> levelUpRewards = new List<LevelReward>();
    private Dictionary<int, List<LevelReward>> rewardsDictionary = new Dictionary<int, List<LevelReward>>();

    [Header("Visible Stats")]
    public float health = 1f;

    [Header("Status Effects")]
    public List<StatusEffect> statusEffects = new List<StatusEffect>();

    // Every X Click triggers
    [Header("Every X Clicks")]
    public List<EveryXClicks> everyXClicks = new List<EveryXClicks>();

    // Start is called before the first frame update
    void Start()
    {
        AssembleRewards();
        CalculateStats();
    }

    // Update is called once per frame
    void Update()
    {
        List<StatusEffect> removals = new List<StatusEffect>();

        // Loop over and tick all status effects
        foreach (StatusEffect e in statusEffects)
        {
            if (e.active)
            {
                e.UpdateTick(Time.deltaTime);
            }
            else
            {
                removals.Add(e);
            }  
        }

        // remove all status effects that are no longer active
        foreach (StatusEffect e in removals)
        {
            statusEffects.Remove(e);
        }
    }

    public void BoubaClicked()
    {
        // Count for click effects
        foreach(EveryXClicks c in everyXClicks)
        {
            c.Click();
        }

        GainBouba(boubaPerClick);
        GainLevelProgress(boubaPerClick);
    }

    public void BoubaProc(float amount)
    {
        GainBouba(amount, true, false);
        GainLevelProgress(amount, true, false);
    }

    private void LevelUp()
    {
        nextLevel = nextLevel*1.5f + 10f;
        levelProgress = 0;
        skillPoints += 1;
        level += 1;
        if (rewardsDictionary.ContainsKey(level))
        {
            foreach(LevelReward r in rewardsDictionary[level])
            {
                r.AttemptReward(level);
            }
        }
    }

    private void GainBouba(float bouba, bool ignoreMult = false, bool triggerProcs = true)
    {
        if (ignoreMult)
        {
            // Ignore bouba multipliers
            boubaPrivate += bouba;
        }
        else
        {
            // Apply bouba multipliers
            boubaPrivate += bouba;
        }

        boubaEarned = (int)boubaPrivate;
    }

    private void GainLevelProgress(float progress, bool ignoreMult = false, bool triggerProcs = true)
    {
        if (ignoreMult)
        {
            // Ignore level multipliers
            levelProgress += progress;
        }
        else
        {
            // Apply level multipliers
            levelProgress += progress;
        }
        if(levelProgress >= nextLevel)
        {
            LevelUp();
        }
    }

    private void AssembleRewards()
    {
        foreach(LevelReward r in levelUpRewards)
        {
            if (rewardsDictionary.ContainsKey(r.level))
            {
                rewardsDictionary[r.level].Add(r);
            }
            else
            {
                rewardsDictionary.Add(r.level, new List<LevelReward>() { r });
            }
        }
    }

    /// <summary>
    /// Triggers calculations for damage and other effects using current bouba stats
    /// </summary>
    public void CalculateStats()
    {
        // Bouba per click
        boubaPerClick = 0.2f + (1f / 5f * health);
    }

    // -------------------------- Skill Point Functions -------------------------
    public void SpendSkillPoints(int points)
    {
        skillPoints -= points;
    }

    public bool CanAffordSkillPoints(int points)
    {
        return skillPoints >= points;
    }

    // -------------------------- Status Effect Functions -------------------------
    public StatusEffect GetStatusEffect<T>()
    {
        return statusEffects.Where(e => e.GetType() == typeof(T)).FirstOrDefault();
    }

    public bool HasAnyStatusEffect<T>()
    {
        return GetStatusEffect<T>() != null;
    }

    public bool HasStatusEffect(StatusEffect effect)
    {
        return statusEffects.Contains(effect);
    }

    public void ApplyStatusEffect(StatusEffect effect)
    {
        if (HasStatusEffect(effect))
            return;

        statusEffects.Add(effect);
    }
}

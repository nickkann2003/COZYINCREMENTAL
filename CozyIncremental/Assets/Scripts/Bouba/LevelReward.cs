using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class LevelReward
{
    public int level;
    public UnityEvent reward;
    public bool redeemOnLoad = false;

    public LevelReward(int level, UnityEvent reward)
    {
        this.level = level;
        this.reward = reward;
    }

    public bool AttemptReward(int l)
    {
        if(l == level)
        {
            RedeemReward();
            return true;
        }
        return false;
    }

    public void RedeemReward()
    {
        reward.Invoke();
    }

    /// <summary>
    /// Attempts to redeem this reward as part of a save load
    /// </summary>
    /// <param name="l">CURRENT LEVEL OF THE PLAYER</param>
    public void LoadRedeem(int l) 
    {
        if(l >= level && redeemOnLoad)
        {
            reward.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bouba : MonoBehaviour
{
    public int boubaEarned;
    private float boubaPrivate;

    public float boubaPerClick;

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

    // Start is called before the first frame update
    void Start()
    {
        AssembleRewards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BoubaClicked()
    {
        GainBouba(boubaPerClick);
        GainLevelProgress(boubaPerClick);
    }

    private void LevelUp()
    {
        nextLevel = (nextLevel * nextLevel * 0.2f) + 10f;
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

    private void GainBouba(float bouba)
    {
        boubaPrivate += bouba;
        boubaEarned = (int)boubaPrivate;
    }

    private void GainLevelProgress(float progress)
    {
        levelProgress += progress;
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoubaMood : MonoBehaviour
{
    [SerializeField]
    public List<MoodSprite> moods;
    private Mood currentMood;
    private SpriteRenderer boubaRenderer;

    private float cooldown = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boubaRenderer = GetComponent<SpriteRenderer>();
        SwitchMood(Mood.NORMAL);
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown <= 0)
        {
            SwitchMood(currentMood + 1);
            cooldown = 5f;
        }
        cooldown -= Time.deltaTime;
    }

    public void SwitchMood(Mood m)
    {
        currentMood = m;
        boubaRenderer.sprite = GetSprite(m);
    }

    private Sprite GetSprite(Mood m)
    {
        Sprite found = boubaRenderer.sprite;
        foreach(MoodSprite ms in moods)
        {
            if(ms.mood == m)
                found = ms.sprite;
        }
        return found;
    }
}

public enum Mood
{
    NORMAL,
    HAPPY,
    SAD,
    THINKING
}

[Serializable]
public class MoodSprite
{
    [SerializeField] public Mood mood;
    [SerializeField] public Sprite sprite;
}

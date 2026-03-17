using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EveryXClicks
{
    public int clicksPerTrigger;
    private int clicks;

    public UnityEvent onTriggerEvents;

    /// <summary>
    /// Creates an EveryXClicks counter
    /// </summary>
    /// <param name="c">Clicks per trigger</param>
    /// <param name="e">UnityEvents to trigger</param>
    public EveryXClicks(int c, UnityEvent e)
    {
        clicksPerTrigger = c;
        onTriggerEvents = e;
    }

    /// <summary>
    /// Count a number of clicks for this obj
    /// </summary>
    /// <param name="c">Clicks to count, default 1</param>
    public void Click(int c = 1)
    {
        clicks += c;

        // Trigger if over clicks
        if(clicks >= clicksPerTrigger)
        {
            Trigger();
            clicks = 0;
        }
    }

    /// <summary>
    /// Triggers click effects
    /// </summary>
    public void Trigger()
    {
        onTriggerEvents.Invoke();
    }
}

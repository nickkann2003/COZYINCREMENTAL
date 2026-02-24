using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoubaContainer : MonoBehaviour
{
    private float baseSize = 1f;
    private float baseMultiplier = 1f;
    private float multiplier = 1f;
    public void MultiplierAdd(float m)
    {
        multiplier += m;
        AdjustSize();
    }

    public void MultiplyBaseSize(float m)
    {
        baseMultiplier += m;
        AdjustSize();
    }

    public void SetBaseSize(float s)
    {
        baseSize = s;
        AdjustSize();
    }

    private void AdjustSize()
    {
        transform.localScale = new Vector3(baseSize * baseMultiplier * multiplier, baseSize * baseMultiplier * multiplier, baseSize * baseMultiplier * multiplier);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoubaContainer : MonoBehaviour
{
    private float baseSize = 1f;
    private float baseMultiplier = 1f;
    private float multiplier = 1f;

    private float tempSizeMax = 1.5f;
    private float ogSizeMax = 1.5f;
    private float tempSizeMin = 1f;
    private float recentDamage = 0f;
    private float incMax = 2f;

    private void Update()
    {
        float tempSize = Mathf.Clamp(1 + (recentDamage/20f), tempSizeMin, Mathf.Clamp(tempSizeMax, ogSizeMax, incMax));
        AdjustSize(tempSize);
    }

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

    public void ContainerDamage(float dmg = 0)
    {
        StartCoroutine(damage(dmg));
    }

    private void AdjustSize(float mult = 1f)
    {
        Vector3 lerped = Vector3.Lerp(transform.localScale, new Vector3(baseSize * baseMultiplier * multiplier * mult, baseSize * baseMultiplier * multiplier * mult, baseSize * baseMultiplier * multiplier * mult), 0.3f);
        //transform.localScale = new Vector3(baseSize * baseMultiplier * multiplier * mult, baseSize * baseMultiplier * multiplier * mult, baseSize * baseMultiplier * multiplier * mult);
        transform.localScale = lerped;
    }

    private IEnumerator damage(float dmg = 0)
    {
        recentDamage += dmg*2.5f;
        tempSizeMax += 0.1f;
        yield return new WaitForSeconds(0.2f);
        tempSizeMax -= 0.1f;
        recentDamage -= dmg * 1.5f;
        yield return new WaitForSeconds(4f);
        recentDamage -= dmg;
    }
}

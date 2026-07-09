using UnityEngine;

public class VisualComboController : MonoBehaviour
{
    public SO_VisualComboChain combo;
    public float currentCombo;
    private float internalCombo;
    public Material backgroundMaterial;

    private ComboItem previous;
    private ComboItem next;

    public RectTransform comboMeterMask;
    public RectTransform comboMeter;
    public float comboMeterMax;

    private float comboMeterPixelWidth;
    private float comboMaskMinX;
    private float comboMaskMaxX;
    private float comboMaxX;
    private float comboMinX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        comboMeterPixelWidth = comboMeterMask.sizeDelta.x;

        comboMaskMinX = comboMeterMask.anchoredPosition.x - comboMeterPixelWidth;
        comboMaskMaxX = comboMeterMask.anchoredPosition.x;

        comboMinX = comboMeter.anchoredPosition.x+comboMeterPixelWidth;
        comboMaxX = comboMeter.anchoredPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        GetNextAndPrevious();

        internalCombo = Mathf.Lerp(internalCombo, currentCombo, 0.175f*Time.deltaTime);
        if(internalCombo > 0f)
        {
            float meterProgress = (currentCombo) / comboMeterMax;
            comboMeterMask.anchoredPosition = new Vector2(Mathf.Lerp(comboMaskMinX, comboMaskMaxX, meterProgress), comboMeterMask.anchoredPosition.y);
            comboMeter.anchoredPosition = new Vector2(Mathf.Lerp(comboMinX, comboMaxX, meterProgress), comboMeter.anchoredPosition.y);
            Debug.Log(internalCombo + ", " + comboMinX + ", " + comboMaxX + ", " + meterProgress);
            Debug.Log("LERP: " + Mathf.Lerp(comboMinX, comboMaxX, meterProgress));
        }

        // CASE: Between two thresholds
        if (internalCombo > previous.comboExitThreshold && internalCombo < next.comboEnterThreshold)
        {
            float lerpAmt = (internalCombo - previous.comboExitThreshold) / (next.comboEnterThreshold - previous.comboExitThreshold);
            LerpComboItem(previous, next, lerpAmt);
        }

        // CASE: In previous
        if(internalCombo < previous.comboExitThreshold)
        {
            SetMaterialToComboItem(previous);
        }

        //  CASE: In next
        if(internalCombo > next.comboEnterThreshold)
        {
            SetMaterialToComboItem(next);
        }
    }

    /// <summary>
    /// Sets the material values to a specific combo item's values
    /// </summary>
    /// <param name="item">Item to set material values to</param>
    private void SetMaterialToComboItem(ComboItem item)
    {
        SO_VisualCombo c = item.comboItem;

        // Cell color
        backgroundMaterial.SetColor("_Cell_Color", c.cellColor);
        // Resolution
        backgroundMaterial.SetFloat("_Resolution", c.resolution);
        // Zoom
        backgroundMaterial.SetFloat("_Zoom", c.zoom);
        // Wave change sin
        backgroundMaterial.SetFloat("_WaveChangeSin", c.waveSin);
        // Base speed
        backgroundMaterial.SetFloat("_BaseSpeed", c.waveSpeed);
        // Time divide
        backgroundMaterial.SetFloat("_TimeDivide", c.timeDivide);
        // Detail resolution multiplier
        backgroundMaterial.SetFloat("_DetailResolutionMultiplier", c.detailResolutionMultiplier);
        // Speed
        backgroundMaterial.SetFloat("_Speed", c.speed);
        // Highlight bright
        backgroundMaterial.SetColor("_HighlightBright", c.highlightBright);
        // Highlight dark
        backgroundMaterial.SetColor("_HighlightDark", c.highlightDark);
        // Highlight visibility
        backgroundMaterial.SetFloat("_HighlightsVisibility", c.highlightVisibility);
        // Flucatuation Divider
        backgroundMaterial.SetFloat("_FluctuationDivider", c.fluctatonDivider);
        // Fluctuation minimum
        backgroundMaterial.SetFloat("_FluctionMinimum", c.flucationMinimum);
    }

    /// <summary>
    /// Sets the material values to a lerped value between two combo items
    /// </summary>
    /// <param name="itemOne">First item</param>
    /// <param name="itemTwo">Second item</param>
    /// <param name="lerp">Lerp amount, 0 to 1</param>
    private void LerpComboItem(ComboItem itemOne, ComboItem itemTwo, float lerp)
    {
        SO_VisualCombo c = itemOne.comboItem;
        SO_VisualCombo c2 = itemTwo.comboItem;

        // Cell color
        backgroundMaterial.SetColor("_Cell_Color", Color.Lerp(c.cellColor, c2.cellColor, lerp));
        // Resolution
        backgroundMaterial.SetFloat("_Resolution", c.resolution + lerp*(c2.resolution-c.resolution));
        // Zoom
        backgroundMaterial.SetFloat("_Zoom", c.zoom + lerp * (c2.zoom - c.zoom));
        // Wave change sin
        backgroundMaterial.SetFloat("_WaveChangeSin", c.waveSin + lerp * (c2.waveSin - c.waveSin));
        // Base speed
        backgroundMaterial.SetFloat("_BaseSpeed", c.waveSpeed + lerp * (c2.waveSpeed - c.waveSpeed));
        // Time divide
        backgroundMaterial.SetFloat("_TimeDivide", c.timeDivide + lerp * (c2.timeDivide - c.timeDivide));
        // Detail resolution multiplier
        backgroundMaterial.SetFloat("_DetailResolutionMultiplier", c.detailResolutionMultiplier + lerp * (c2.detailResolutionMultiplier - c.detailResolutionMultiplier));
        // Speed
        backgroundMaterial.SetFloat("_Speed", c.speed + lerp * (c2.speed - c.speed));
        // Highlight bright
        backgroundMaterial.SetColor("_HighlightBright", Color.Lerp(c.highlightBright, c2.highlightBright, lerp));
        // Highlight dark
        backgroundMaterial.SetColor("_HighlightDark", Color.Lerp(c.highlightDark, c2.highlightDark, lerp));
        // Highlight visibility
        backgroundMaterial.SetFloat("_HighlightsVisibility", c.highlightVisibility + lerp * (c2.highlightVisibility - c.highlightVisibility));
        // Flucatuation Divider
        backgroundMaterial.SetFloat("_FluctuationDivider", c.fluctatonDivider + lerp * (c2.fluctatonDivider - c.fluctatonDivider));
        // Fluctuation minimum
        backgroundMaterial.SetFloat("_FluctionMinimum", c.flucationMinimum + lerp * (c2.flucationMinimum - c.flucationMinimum));
    }

    private void GetNextAndPrevious()
    {
        for (int i = 0; i < combo.comboChain.Count; i++)
        {
            if (currentCombo < combo.comboChain[i].comboEnterThreshold)
            {
                next = combo.comboChain[i];
                if (i == 0)
                {
                    previous = combo.comboChain[i];
                }
                else
                {
                    previous = combo.comboChain[i - 1];
                }
            }
        }
    }
}

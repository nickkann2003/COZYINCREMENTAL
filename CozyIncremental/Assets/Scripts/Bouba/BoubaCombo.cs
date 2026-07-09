using System.Collections;
using UnityEngine;

public class BoubaCombo : MonoBehaviour
{
    public VisualComboController comboVisuals;
    public BoubaContainer container;

    private float comboBase = 1f;
    private float comboMult = 1f;
    private float currentCombo;


    public float pDecayPerSecond = 0.01f;
    public float fDecayPerSecond = 0.01f;
    private float decayTickRate;

    public AnimationCurve comboPerBouba;

    private void Start()
    {
        StartCoroutine(ComboDecay());
    }

    public void addCombo(float boubaAmount)
    {
        float comboAdd = boubaAmount * comboPerBouba.Evaluate(currentCombo);
        comboAdd *= comboMult;
        currentCombo += comboAdd;
    }

    /// <summary>
    /// Runs every decayTickRate seconds and applies decay
    /// </summary>
    /// <returns></returns>
    IEnumerator ComboDecay()
    {
        // Apply decay
        currentCombo = currentCombo * (1f - (pDecayPerSecond * decayTickRate));
        currentCombo = currentCombo - (fDecayPerSecond * decayTickRate);
        currentCombo = Mathf.Max(currentCombo, 1f);

        // Apply visuals
        //comboVisuals.currentCombo = currentCombo;

        yield return new WaitForSeconds(decayTickRate);
        StartCoroutine(ComboDecay());
    }
}

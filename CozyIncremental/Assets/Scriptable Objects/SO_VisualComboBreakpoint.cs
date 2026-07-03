using UnityEngine;

[CreateAssetMenu(fileName = "Breakpoint", menuName = "Scriptable Objects/Visual Combos/Breakpoint")]
public class SO_VisualCombo : ScriptableObject
{
    public Color cellColor = Color.blue;
    public float resolution = 28f;
    public float zoom = 14.8f;
    public float waveSin = 13f;
    public float waveSpeed = 0.0025f;
    public float timeDivide;
    public float detailResolutionMultiplier = 2f;
    public float speed;
    public Color highlightBright = Color.blueViolet;
    public Color highlightDark = Color.violet;
    public float highlightVisibility = 0.5f;
    public float fluctatonDivider;
    public float flucationMinimum;
}

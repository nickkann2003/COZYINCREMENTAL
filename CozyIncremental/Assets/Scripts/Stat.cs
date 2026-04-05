using UnityEngine;

public class Stat
{
    private float baseValue;
    private float flatIncrease;
    private float percentIncrease;
    private float finalValue;

    public float BaseValue { get => baseValue; set { baseValue = value; Recalculate(); } }
    public float FlatIncrease { get => flatIncrease; set { flatIncrease = value; Recalculate(); } }
    public float PercentIncrease { get => percentIncrease; set { percentIncrease = value; Recalculate(); } }
    public float FinalValue { get => finalValue; }

    // constructor
    public Stat(float baseValue)
    {
        this.baseValue = baseValue;
        this.flatIncrease = 0f;
        this.percentIncrease = 0f;
        this.finalValue = baseValue;
    }

    private void Recalculate()
    {
        finalValue = (baseValue + flatIncrease) * (1 + percentIncrease);
    }
}

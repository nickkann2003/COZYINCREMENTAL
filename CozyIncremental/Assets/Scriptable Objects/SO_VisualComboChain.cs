using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combo Chain", menuName = "Scriptable Objects/Visual Combos/Combo Chain")]
public class SO_VisualComboChain : ScriptableObject
{
    public List<ComboItem> comboChain;
}

[System.Serializable]
public class ComboItem
{
    public float comboEnterThreshold;
    public float comboExitThreshold;
    public SO_VisualCombo comboItem;
}

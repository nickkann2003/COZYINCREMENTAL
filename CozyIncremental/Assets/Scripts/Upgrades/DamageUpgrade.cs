using System;
using UnityEngine;

[Serializable]
public  class DamageUpgrade : UpgradeBase 
{
    [SerializeField]public float baseDamageIncrease;
    [SerializeField]public float flatDamageIncrease;
    [SerializeField]public float percentDamageIncrease;

    public DamageUpgrade(float baseIncrease, float flatIncrease, float percentIncrease) : base("Damage Upgrade", "Increases amount gained per click")
    {
        baseDamageIncrease = baseIncrease;
        flatDamageIncrease = flatIncrease;
        percentDamageIncrease = percentIncrease;
    }

    public override void ApplyUpgrade(Bouba bouba)
    {
        bouba.boubaPerClick.BaseValue += baseDamageIncrease;
        bouba.boubaPerClick.FlatIncrease += flatDamageIncrease;
        bouba.boubaPerClick.PercentIncrease += percentDamageIncrease;
    }

    public override void RemoveUpgrade(Bouba bouba)
    {
        bouba.boubaPerClick.BaseValue -= baseDamageIncrease;
        bouba.boubaPerClick.FlatIncrease -= flatDamageIncrease;
        bouba.boubaPerClick.PercentIncrease -= percentDamageIncrease;
    }
}

using System;
using UnityEngine;

[Serializable]
public class UpgradeBase : IUpgrade
{
    public string Name;
    public string Description;

    public UpgradeBase(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public virtual void ApplyUpgrade(Bouba bouba)
    {
        throw new System.NotImplementedException();
    }

    public virtual void RemoveUpgrade(Bouba bouba)
    {
        throw new System.NotImplementedException();
    }
}

[Flags]
public enum UpgradeTypes
{
    NONE = 0,
    DAMAGE = 1 << 0,
    HEALTH = 1 << 1,
    SPEED = 1 << 2,
    ALL = ~0
}

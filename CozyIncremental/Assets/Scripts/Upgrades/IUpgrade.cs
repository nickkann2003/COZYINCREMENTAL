using System;
using UnityEngine;

public interface IUpgrade
{
    public abstract void ApplyUpgrade(Bouba bouba);
    public abstract void RemoveUpgrade(Bouba bouba);
}

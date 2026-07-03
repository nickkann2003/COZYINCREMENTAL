using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Bounce Trigger Info", menuName = "Scriptable Objects/Bounce Triggers/Trigger Info")]
public class BounceTriggerInfo : ScriptableObject
{
    [Header("Starting Position")]
    public Vector3 startPos;

    [Header("Bounce Vars")]
    public float bounceDuration = 1f;
    public Vector3 bounceTo;
    public AnimationCurve bounceCurve;
    public AnimationCurve rotationCurve;

    [Header("Targeting Delay")]
    public float targetDuration = 0.2f;

    [Header("Hit Vars")]
    public float hitDuration = 0.2f;
    public Vector3 target;

    [Header("Bounces")]
    public int numBounces = 1;

    [Header("Size")]
    public float sizeMin;
    public float sizeMax;

    [Header("Hit Events")]
    public UnityEvent onHitEvent;
    public UnityEvent finalHitEvent;

    [Header("Particles")]
    public GameObject hitParticles;
}

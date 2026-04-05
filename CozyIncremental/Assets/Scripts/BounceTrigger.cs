using System;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;


public class BounceTrigger : MonoBehaviour
{
    public BounceTriggerInfo info;

    private Vector3 startPos;
    private float bounceDuration = 1f;
    private float cBD = 0f;
    private Vector3 bounceTo;
    private AnimationCurve bounceCurve;
    private Vector3 bounceControl;
    private float targetDuration = 0.2f;
    private float cTD = 0f;
    private float hitDuration = 0.2f;
    private float cHD = 0f;
    private Vector3 target;
    private int numBounces = 1;
    private UnityEvent onHitEvent;
    private UnityEvent finalHitEvent;
    private GameObject hitParticles;

    public Func<Vector3> calcBounceTo;

    public BounceSpawner spawner;
    private int state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }

    public void Create(BounceTriggerInfo i = null)
    {
        info = i == null ? info : i;
        
        state = 0;

        bounceControl = (startPos + bounceTo) / 2f + (Vector3.up);
        startPos = info.startPos;
        bounceDuration = info.bounceDuration;
        //bounceTo = info.bounceTo;
        bounceCurve = info.bounceCurve;
        targetDuration = info.targetDuration;
        hitDuration = info.hitDuration;
        target = info.target;
        numBounces = info.numBounces;
        onHitEvent = info.onHitEvent;
        finalHitEvent = info.finalHitEvent;
        hitParticles = info.hitParticles;
        bounceTo = calcBounceTo();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0:
                float t = bounceCurve.Evaluate(Mathf.Sqrt(cBD / bounceDuration));
                transform.position = CalculateQuadraticBezierPoint(t, startPos, bounceControl, bounceTo);
                cBD += Time.deltaTime;
                if (cBD >= bounceDuration)
                {
                    cBD = 0f;
                    state = 1;
                }
                break;
            case 1:
                cTD += Time.deltaTime;
                if (cTD >= targetDuration)
                {
                    cTD = 0f;
                    state = 2;
                }
                break;
            case 2:
                transform.position = Vector3.Lerp(bounceTo, target, cHD / hitDuration);
                cHD += Time.deltaTime;
                if (cHD >= hitDuration*0.9f)
                {
                    cHD = 0f;
                    onHitEvent.Invoke();
                    Instantiate(hitParticles).transform.position = transform.position;
                    numBounces--;
                    if (numBounces > 0)
                    {
                        state = 0;
                        bounceTo = calcBounceTo();
                    }
                    else
                    {
                        finalHitEvent.Invoke();
                        DisableSelf();
                    }
                }
                break;
        }
    }

    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // Formula: B(t) = (1-t)^2 * P0 + 2*(1-t)*t * P1 + t^2 * P2
        return Mathf.Pow(1f - t, 2f) * p0 + 2f * (1f - t) * t * p1 + Mathf.Pow(t, 2f) * p2;
    }

    private void DisableSelf()
    {
        spawner.inactiveTriggers.Push(this);
        gameObject.SetActive(false);
    }
}

[Serializable]
public class BounceTriggerInfo
{
    [Header("Starting Position")]
    public Vector3 startPos;

    [Header("Bounce Vars")]
    public float bounceDuration = 1f;
    public Vector3 bounceTo;
    public AnimationCurve bounceCurve;

    [Header("Targeting Delay")]
    public float targetDuration = 0.2f;

    [Header("Hit Vars")]
    public float hitDuration = 0.2f;
    public Vector3 target;

    [Header("Bounces")]
    public int numBounces = 1;

    [Header("Hit Events")]
    public UnityEvent onHitEvent;
    public UnityEvent finalHitEvent;

    [Header("Particles")]
    public GameObject hitParticles;
}

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BounceSpawner : MonoBehaviour
{

    public SO_BounceSpawner spawnerValues;

    private GameObject prefab;
    private Vector3 start;
    private float arcDeg;

    private float minDistance;
    private float maxDistance;

    private Transform bounceTriggerPool;
    public Stack<BounceTrigger> inactiveTriggers = new Stack<BounceTrigger>();

    private TriggerInfo triggerValues;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerValues = new TriggerInfo(Bouba.instance.bounceTriggerValues);
        bounceTriggerPool = new GameObject("BounceTriggers Pool").transform;

        prefab = spawnerValues.prefab;
        start = spawnerValues.start;
        arcDeg = spawnerValues.arcDegrees;
        minDistance = spawnerValues.minDistance;
        maxDistance = spawnerValues.maxDistance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {

        BounceTrigger trigger;
        if(inactiveTriggers.Count <= 0)
        {
            GameObject created = Instantiate(prefab, bounceTriggerPool);
            trigger = created.GetComponent<BounceTrigger>();
            trigger.spawner = this;
        }
        else
        {
            trigger = inactiveTriggers.Pop();
            trigger.gameObject.SetActive(true);
        }

        triggerValues.startPos = transform.position;

        trigger.calcBounceTo = GetBounceTo;
        trigger.Create(triggerValues);
    }

    public Vector3 GetBounceTo()
    {
        float r = Random.Range(minDistance, maxDistance);
        float a = Random.Range(90 - arcDeg / 2f, 90 + arcDeg / 2f);
        Vector3 bounceVector = new Vector3(Mathf.Cos(a * Mathf.Deg2Rad) * r, Mathf.Sin(a * Mathf.Deg2Rad) * r, 0);
        bounceVector += transform.position;
        return bounceVector;
    }
}

public class TriggerInfo
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
    public TriggerInfo(BounceTriggerInfo t)
    {
        startPos = t.startPos;
        bounceDuration = t.bounceDuration;
        bounceTo = t.bounceTo;
        bounceCurve = t.bounceCurve;

        // THIS IS KEY, animation curves were being adjusted on the scriptable object itself
        rotationCurve = new AnimationCurve();
        rotationCurve.CopyFrom(t.rotationCurve);

        targetDuration = t.targetDuration;
        hitDuration = t.hitDuration;
        target = t.target;
        numBounces = t.numBounces;
        sizeMin = t.sizeMin;
        sizeMax = t.sizeMax;
        onHitEvent = t.onHitEvent;
        finalHitEvent = t.finalHitEvent;
        hitParticles = t.hitParticles;
    }
}

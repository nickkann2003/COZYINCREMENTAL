using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BounceSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 start;
    public float arcDeg;

    [SerializeField]
    public float minDistance;
    public float maxDistance;

    private Transform bounceTriggerPool;
    public Stack<BounceTrigger> inactiveTriggers = new Stack<BounceTrigger>();

    private BounceTriggerInfo triggerValues;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerValues = Bouba.instance.bounceTriggerValues;
        bounceTriggerPool = new GameObject("BounceTriggers Pool").transform;
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

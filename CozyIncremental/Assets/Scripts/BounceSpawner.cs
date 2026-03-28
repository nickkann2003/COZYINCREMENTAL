using UnityEngine;

public class BounceSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 start;
    public float arcDeg;

    [SerializeField]
    public float minDistance;
    public float maxDistance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        GameObject created = Instantiate(prefab, transform);
        BounceTrigger trigger = created.GetComponent<BounceTrigger>();
        trigger.startPos = transform.position;
        float r = Random.Range(minDistance, maxDistance);
        float a = Random.Range(90 - arcDeg / 2f, 90 + arcDeg / 2f);
        Vector3 bounceVector = new Vector3(Mathf.Cos(a * Mathf.Deg2Rad) * r, Mathf.Sin(a * Mathf.Deg2Rad) *r, 0);
        trigger.bounceTo = transform.position + bounceVector;
    }
}

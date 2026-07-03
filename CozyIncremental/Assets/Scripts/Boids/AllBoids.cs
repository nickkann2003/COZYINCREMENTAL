using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBoids : MonoBehaviour
{
    public static List<Transform> boids = new List<Transform>();
    public GameObject topLeft;
    public GameObject botRight;

    public int boidSpawns = 0;
    public GameObject boidPrefab;

    public static float left;
    public static float right;
    public static float top;
    public static float bottom;

    public static float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        boids.Clear();
        Boid[] bds = FindObjectsOfType<Boid>();
        foreach (Boid b in bds)
        {
            boids.Add(b.transform);
            b.transform.parent = transform;
        }
        for(int i = 0; i < boidSpawns; i++)
        {
            GameObject b = Instantiate(boidPrefab);
            b.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0);
            boids.Add(b.transform);
        }

        left = topLeft.transform.position.x;
        top = topLeft.transform.position.y;
        right = botRight.transform.position.x;
        bottom = botRight.transform.position.y;

        time += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

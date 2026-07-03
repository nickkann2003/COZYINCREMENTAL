using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private Vector3 direction = Vector3.right;
    private float speed = 2f;
    private List<Transform> otherPos = new List<Transform>();

    private float detectionRange = 5f;

    public float avoidRange = 1f;
    public float avoidForce = 1f;

    public float alignmentRange = 2f;
    public float alignmentForce = 1f;

    public float cohesionDistance = 2f;
    public float cohesionForce = 1f;

    private float timeRandom;
    // Start is called before the first frame update
    void Start()
    {
        direction.x = Random.Range(-1f, 1f);
        direction.y = Random.Range(-1f, 1f);

        timeRandom = Random.Range(-5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        // Rule applications
        getBoidsInRange(detectionRange, otherPos);


        // Normalize and look
        direction.z = 0;
        direction.Normalize();
        transform.right = direction;

        // Move
        transform.position += speed * direction * Time.deltaTime;

        // Rotate over time
        direction.x += Mathf.Sin(AllBoids.time + timeRandom) * Time.deltaTime;
        direction.y += Mathf.Cos(AllBoids.time + timeRandom) * Time.deltaTime;

        // Bounds check
        boundsCheck();

    }

    private void boundsCheck()
    {
        if (transform.position.x < AllBoids.left)
            transform.position = new Vector3(AllBoids.right, transform.position.y, 0);

        if (transform.position.x > AllBoids.right)
            transform.position = new Vector3(AllBoids.left, transform.position.y, 0);

        if (transform.position.y < AllBoids.bottom)
            transform.position = new Vector3(transform.position.x, AllBoids.top, 0);

        if (transform.position.y > AllBoids.top)
            transform.position = new Vector3(transform.position.x, AllBoids.bottom, 0);
    }

    private List<Transform> getBoidsInRange(float detectR, List<Transform> t)
    {
        t.Clear();

            Vector3 averagePos = Vector3.zero;
        foreach(Transform other in AllBoids.boids)
        {
            if(other == transform)
            {
                continue;
            }
            float dist = Vector3.Distance(transform.position, other.position);
            if (dist < detectR)
            {
                t.Add(other);
                if(dist < cohesionDistance)
                {
                    averagePos += other.position;
                }
                if(dist < avoidRange)
                {
                    Avoid(other, avoidForce/dist);
                }
                if(dist < alignmentRange)
                {
                    Alignment(other, Mathf.Min(alignmentForce, alignmentForce/dist));
                }
            }
        }
            averagePos = averagePos / t.Count;
            Cohesion(averagePos, cohesionForce);

        return t;
    }

    private void Avoid(Transform t, float avoidForce)
    {
        direction += (transform.position - t.position).normalized * Time.deltaTime;
    }

    private void Alignment(Transform t, float aForce)
    {
        direction += t.right * aForce * Time.deltaTime;
    }

    private void Cohesion(Vector3 aPos, float cForce)
    {
        direction += (aPos - transform.position).normalized * cForce * Time.deltaTime;              
    }
}

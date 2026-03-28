using System.Net;
using UnityEngine;
using UnityEngine.Events;

public class BounceTrigger : MonoBehaviour
{
    [Header("Starting Position")]
    public Vector3 startPos;

    [Header("Bounce Vars")]
    public float bounceDuration = 1f;
    private float cBD = 0f;
    public Vector3 bounceTo;
    public AnimationCurve bounceCurve;
    private Vector3 bounceControl;

    [Header("Targeting Delay")]
    public float targetDuration = 0.2f;
    private float cTD = 0f;

    [Header("Hit Vars")]
    public float hitDuration = 0.2f;
    private float cHD = 0f;
    public Vector3 target;

    [Header("Bounces")]
    public int numBounces = 1;

    [Header("Hit Events")]
    public UnityEvent onHitEvent;
    public UnityEvent finalHitEvent;

    [Header("Particles")]
    public GameObject hitParticles;


    private int state;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bounceControl = (startPos + bounceTo) / 2f + (Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0:
                transform.position = CalculateQuadraticBezierPoint(cBD/bounceDuration, startPos, bounceControl, bounceTo);
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
                if (cHD >= hitDuration)
                {
                    cHD = 0f;
                    onHitEvent.Invoke();
                    numBounces--;
                    if (numBounces > 1)
                    {
                        state = 0;
                        numBounces--;
                    }
                    else
                    {
                        finalHitEvent.Invoke();
                        Destroy(gameObject);
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
}

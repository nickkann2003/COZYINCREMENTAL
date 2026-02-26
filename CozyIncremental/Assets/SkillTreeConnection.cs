using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SkillTreeConnection : MonoBehaviour
{
    public SkillTreeNode other;
    public Vector2 startPoint;
    public Vector2 endPoint;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;

        if (other != null)
        {
            endPoint = other.transform.position;
        }


        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint/2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (other)
            endPoint = other.transform.position;
        Gizmos.DrawSphere(startPoint + (Vector2)transform.position, 0.1f);
        Gizmos.DrawSphere(endPoint + (Vector2)transform.position, 0.1f);
        Gizmos.DrawLine(startPoint, endPoint);
    }
}

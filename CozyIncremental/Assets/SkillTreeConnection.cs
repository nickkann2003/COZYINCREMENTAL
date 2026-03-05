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
    private SkillNodeState state;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;

        if (other != null)
        {
            endPoint = other.transform.position;
        }


        lineRenderer.startWidth = 0.05f * transform.lossyScale.x;
        lineRenderer.endWidth = 0.05f * transform.lossyScale.x;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.startWidth = 0.05f * transform.lossyScale.x;
        lineRenderer.endWidth = 0.05f * transform.lossyScale.x;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }

    public void SetState(SkillNodeState newState)
    {
        if (state == newState)
            return;

        switch (newState)
        {
            case SkillNodeState.INACTIVE:
                lineRenderer.startColor = SkillTreeColors.darkBrown;
                lineRenderer.endColor = SkillTreeColors.darkBrown;
                break;
            case SkillNodeState.ACTIVE:
                lineRenderer.startColor = SkillTreeColors.darkPeach;
                lineRenderer.endColor = SkillTreeColors.darkPeach;
                break;
            case SkillNodeState.HOVER:
                lineRenderer.startColor = SkillTreeColors.peach;
                lineRenderer.endColor = SkillTreeColors.peach;
                break;
            case SkillNodeState.SELECTED:
                lineRenderer.startColor = SkillTreeColors.mint;
                lineRenderer.endColor = SkillTreeColors.mint;
                break;
            case SkillNodeState.BOUGHT:
                lineRenderer.startColor = SkillTreeColors.darkPeach;
                lineRenderer.endColor = SkillTreeColors.darkPeach;
                break;
            case SkillNodeState.NONE:
                break;
        }
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

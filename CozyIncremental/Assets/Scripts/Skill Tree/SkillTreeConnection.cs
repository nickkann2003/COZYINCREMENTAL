using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SkillTreeConnection : MonoBehaviour
{
    public SkillTreeNode other;
    public Vector3 startPoint;
    public Vector3 endPoint;
    private LineRenderer lineRenderer;
    private SkillNodeState state;
    // Start is called before the first frame update
    void Start()
    {
        CreateLine();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLine();
    }

    private void CreateLine()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;

        if (other != null)
        {
            startPoint = transform.position;
            endPoint = other.transform.position;
        }


        lineRenderer.startWidth = 0.05f * transform.lossyScale.x;
        lineRenderer.endWidth = 0.05f * transform.lossyScale.x;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
        lineRenderer.colorGradient.colorKeys[0].color = SkillTreeColors.darkBrown;
        lineRenderer.colorGradient.colorKeys[1].color = SkillTreeColors.darkBrown;
    }

    private void UpdateLine()
    {
        if (other != null)
        {
            startPoint = transform.position;
            endPoint = other.transform.position;
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);
            lineRenderer.startWidth = 0.05f * transform.lossyScale.x;
            lineRenderer.endWidth = 0.05f * transform.lossyScale.x;
        }
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

#if UNITY_EDITOR
    private void OnValidate()
    {
        CreateLine();
    }
#endif

}

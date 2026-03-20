using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]
public class SkillTreeConnection : MonoBehaviour
{
    public SkillTreeNode other;
    public Vector3 startPoint;
    public Vector3 endPoint;
    private LineRenderer lineRenderer;
    private SkillNodeState state = SkillNodeState.NONE;
    private bool buyable = false;

    private void Awake()
    {
        CreateLine();
    }
    // Start is called before the first frame update
    void Start()
    {
        UnityAction a = new UnityAction(() => SetState(SkillNodeState.ACTIVE));
        other.SubscribeToBuy(a);
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
            lineRenderer.endWidth = 0.05f * other.transform.lossyScale.x;
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
                state = SkillNodeState.INACTIVE;
                break;
            case SkillNodeState.ACTIVE:
                lineRenderer.startColor = SkillTreeColors.brown;
                lineRenderer.endColor = SkillTreeColors.brown;
                state = SkillNodeState.ACTIVE;
                break;
            case SkillNodeState.HOVER:
                lineRenderer.startColor = SkillTreeColors.peach;
                lineRenderer.endColor = SkillTreeColors.peach;
                state = SkillNodeState.HOVER;
                break;
            case SkillNodeState.SELECTED:
                if (buyable)
                {
                    lineRenderer.startColor = SkillTreeColors.mint;
                    lineRenderer.endColor = SkillTreeColors.mint;
                    other.SetState(SkillNodeState.ACTIVE);
                }
                else
                {
                    lineRenderer.startColor = SkillTreeColors.darkBrown;
                    lineRenderer.endColor = SkillTreeColors.darkBrown;
                    other.SetState(SkillNodeState.INACTIVE);
                }
                state = SkillNodeState.SELECTED;
                break;
            case SkillNodeState.BOUGHT:
                lineRenderer.startColor = SkillTreeColors.brown;
                lineRenderer.endColor = SkillTreeColors.brown;
                state = SkillNodeState.BOUGHT;
                break;
            case SkillNodeState.NONE:
                break;
        }
    }

    public void SetBuyable(bool b)
    {
        buyable = b;
        if (state == SkillNodeState.SELECTED)
        {
            lineRenderer.startColor = SkillTreeColors.mint;
            lineRenderer.endColor = SkillTreeColors.mint;
            other.SetState(SkillNodeState.ACTIVE);
        }
        else
        {
            lineRenderer.startColor = SkillTreeColors.darkBrown;
            lineRenderer.endColor = SkillTreeColors.darkBrown;
            other.SetState(SkillNodeState.INACTIVE);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        CreateLine();
    }
#endif

}

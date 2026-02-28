using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillTreeNode : MonoBehaviour
{
    private SkillNodeState state = SkillNodeState.ACTIVE;

    [Header("Skill Art")]
    [SerializeField] private Sprite icon;
    [SerializeField] private Sprite background;

    [Header("Item References")]
    [SerializeField] private SpriteRenderer bgObj;
    [SerializeField] private SpriteRenderer iconObj;

    [Header("Connections")]
    public List<SkillTreeConnection> outgoingConnections = new List<SkillTreeConnection>();

    [Header("Events")]
    public UnityEvent onBuyEvents;

    // Start is called before the first frame update
    void Start()
    {
        foreach(SkillTreeConnection con in outgoingConnections)
        {
            con.startPoint = transform.position;
        }
        SetIcon();
        SetState(SkillNodeState.INACTIVE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(SkillNodeState newState)
    {
        if(state == newState) return;
        switch (newState)
        {
            case SkillNodeState.INACTIVE:
                bgObj.color = SkillTreeColors.inactiveBack;
                iconObj.color = SkillTreeColors.inactiveIcon;
                break;
            case SkillNodeState.ACTIVE:
                bgObj.color = SkillTreeColors.activeBack;
                iconObj.color = SkillTreeColors.activeIcon;
                break;
            case SkillNodeState.HOVER:
                bgObj.color = SkillTreeColors.hoverBack;
                iconObj.color = SkillTreeColors.hoverIcon;
                break;
            case SkillNodeState.SELECTED:
                break;
            case SkillNodeState.BOUGHT:
                bgObj.color = SkillTreeColors.boughtBack;
                iconObj.color = SkillTreeColors.boughtIcon;
                onBuyEvents.Invoke();
                break;
        }
    }

    private void SetIcon()
    {
        iconObj.sprite = icon;
        bgObj.sprite = background;
    }

    private void OnDrawGizmosSelected()
    {
        SetIcon();
    }

    public void Hover()
    {
        if(state == SkillNodeState.ACTIVE)
        {
            SetState(SkillNodeState.HOVER);
        }
    }

    public void UnHover()
    {
        if(state == SkillNodeState.HOVER)
        {
            SetState(SkillNodeState.ACTIVE);
        }
    }

    public void Click()
    {

    }
}

public enum SkillNodeState
{
    INACTIVE,
    ACTIVE,
    HOVER,
    SELECTED,
    BOUGHT
}
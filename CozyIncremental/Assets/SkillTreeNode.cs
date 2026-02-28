using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.Events;

public class SkillTreeNode : MonoBehaviour
{
    private SkillNodeState state = SkillNodeState.NONE;

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

    public bool isRoot = false;

    private ClickableObject clickable;

    // Start is called before the first frame update
    void Start()
    {
        foreach(SkillTreeConnection con in GetComponentsInChildren<SkillTreeConnection>())
        {
            if(outgoingConnections.Contains(con)) continue;
            outgoingConnections.Add(con);
        }
        clickable = GetComponent<ClickableObject>();

        foreach(SkillTreeConnection con in outgoingConnections)
        {
            con.startPoint = transform.position;
        }

        SetIcon();
        
        if (isRoot)
        {
            SetState(SkillNodeState.ACTIVE);
        }
        else
        {
            SetState(SkillNodeState.INACTIVE);
        }
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
                state = SkillNodeState.INACTIVE;
                clickable.Disable();
                break;
            case SkillNodeState.ACTIVE:
                state = SkillNodeState.ACTIVE;
                clickable.Enable();
                break;
            case SkillNodeState.HOVER:
                state = SkillNodeState.HOVER;
                break;
            case SkillNodeState.SELECTED:
                break;
            case SkillNodeState.BOUGHT:
                state = SkillNodeState.BOUGHT;
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

    public void OnClick()
    {
        // Buy logic
    }
}

public enum SkillNodeState
{
    INACTIVE,
    ACTIVE,
    HOVER,
    SELECTED,
    BOUGHT,
    NONE
}
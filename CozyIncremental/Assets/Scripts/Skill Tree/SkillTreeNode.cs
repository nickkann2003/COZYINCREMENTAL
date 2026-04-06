using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public List<SkillTreeNode> connectedNodes = new List<SkillTreeNode>();
    public GameObject skillTreeConnectionPrefab;
    private List<SkillTreeConnection> outgoingConnections = new List<SkillTreeConnection>();

    [Header("Events")]
    public UnityEvent onBuyEvents;

    [Header("Description")]
    public string internalName;
    public string skillName;
    public string description;

    public bool isRoot = false;

    private ClickableObject clickable;

    // All upgrades tied to this skill node
    // NOTE: To add upgrades, add the type to the enum, add a field for the upgrade, add it to the SkillNodeEditor, and add it to the upgrades list in Start()
    [Header("Upgrades")]
    public UpgradeTypes upgradeTypes;
    [field: NonSerialized] public DamageUpgrade damageUpgrade;
    private List<IUpgrade> upgrades = new List<IUpgrade>();


    // Start is called before the first frame update
    void Start()
    {
        // Store all upgrades
        upgrades.Add(damageUpgrade);


        foreach(SkillTreeConnection con in GetComponentsInChildren<SkillTreeConnection>())
        {
            if(outgoingConnections.Contains(con)) continue;
            outgoingConnections.Add(con);
        }
        foreach(SkillTreeNode n in connectedNodes)
        {
            SkillTreeConnection con = Instantiate(skillTreeConnectionPrefab, transform).GetComponent<SkillTreeConnection>();
            con.other = n;
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

        // Add this skill to all skills
        SkillTree.instance.allSkills.Add(this);
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
                foreach(SkillTreeConnection con in outgoingConnections)
                {
                    con.SetState(SkillNodeState.INACTIVE);
                }
                clickable.Disable();
                break;
            case SkillNodeState.ACTIVE:
                state = SkillNodeState.ACTIVE;
                foreach (SkillTreeConnection con in outgoingConnections)
                {
                    con.SetState(SkillNodeState.INACTIVE);
                }
                clickable.Enable();
                break;
            case SkillNodeState.HOVER:
                state = SkillNodeState.HOVER;
                break;
            case SkillNodeState.SELECTED:
                foreach (SkillTreeConnection con in outgoingConnections)
                {
                    con.SetState(SkillNodeState.SELECTED);
                }
                state = SkillNodeState.SELECTED;
                break;
            case SkillNodeState.BOUGHT:
                state = SkillNodeState.BOUGHT;
                foreach (SkillTreeConnection con in outgoingConnections)
                {
                    con.SetState(SkillNodeState.SELECTED);
                }
                onBuyEvents.Invoke();
                break;
        }
    }

    private void SetIcon()
    {
        iconObj.sprite = icon;
        bgObj.sprite = background;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        SetIcon();
        gameObject.name = "Skill - " + internalName + " - " + connectedNodes.Count + " (" + skillName + ")";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (SkillTreeNode n in connectedNodes)
        {
            Gizmos.DrawLine(transform.position, n.transform.position);
        }

    }
#endif

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
        if(state != SkillNodeState.INACTIVE && state != SkillNodeState.BOUGHT && state != SkillNodeState.SELECTED)
        {
            SkillTree.instance.SelectSkill(this);
            SetState(SkillNodeState.SELECTED);
        }
    }

    public void SetAffordNext(bool canAfford)
    {
        foreach (SkillTreeConnection con in outgoingConnections)
        {
            con.SetBuyable(canAfford);
        }
    }

    public void SubscribeToBuy(UnityAction call)
    {
        onBuyEvents.AddListener(call);
    }

    /// <summary>
    /// Applies all upgrades tied to this skill tree node
    /// </summary>
    /// <param name="b"></param>
    public void ApplyAllUpgrades(Bouba b)
    {

    }

    /// <summary>
    /// Removes all upgrades tied to this skill tree node
    /// </summary>
    /// <param name="b"></param>
    public void RemoveAllUpgrades(Bouba b)
    {

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
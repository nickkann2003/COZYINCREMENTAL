using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillTreeNode : MonoBehaviour
{
    private SkillNodeState state = SkillNodeState.INACTIVE;
    [Header("Item References")]
    [SerializeField] private GameObject inactive;
    [SerializeField] private GameObject active;
    [SerializeField] private GameObject hovered;
    [SerializeField] private GameObject bought;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(SkillNodeState newState)
    {
        if(state == newState) return;
        DeactivateAll();
        switch (newState)
        {
            case SkillNodeState.INACTIVE:
                inactive.SetActive(true);
                break;
            case SkillNodeState.ACTIVE:
                active.SetActive(true);
                break;
            case SkillNodeState.HOVER:
                break;
            case SkillNodeState.SELECTED:
                hovered.SetActive(true);
                break;
            case SkillNodeState.BOUGHT:
                bought.SetActive(true);
                onBuyEvents.Invoke();
                break;
        }
    }

    private void DeactivateAll()
    {
        inactive.SetActive(false);
        active.SetActive(false);
        hovered.SetActive(false);
        bought.SetActive(false);
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
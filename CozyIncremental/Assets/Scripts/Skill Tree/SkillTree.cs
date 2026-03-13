using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public static SkillTree instance;

    public List<SkillTreeNode> allSkills;
    public Bouba bouba;
    public List<SkillTreeNode> selected;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SkillTreeNode[] nodes = GetComponentsInChildren<SkillTreeNode>();
        allSkills.AddRange(nodes);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PurchaseSelected()
    {
        foreach(SkillTreeNode node in selected)
        {
            node.SetState(SkillNodeState.BOUGHT);
        }
        selected.Clear();
    }

    public void SelectSkill(SkillTreeNode node)
    {
        if (CheckAffordSkills())
        {
            node.SetState(SkillNodeState.SELECTED);
            selected.Add(node); 
        }
    }

    public bool CheckAffordSkills()
    {
        if(bouba.CanAffordSkillPoints(selected.Count + 1))
        {
            return true;
        }

        return false;
    }

    private void runAffordCheck()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;
    public Animator bouba;
    public Animator skillTree;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Update()
    {
        // When the p key is pressed, switch between bouba and skill tree
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (state == GameState.BOUBA)
            {
                state = GameState.SKILLTREE;
            }
            else
            {
                state = GameState.BOUBA;
            }
            ChangeState(state);
        }
    }

    public void ChangeState(GameState s)
    {
        switch (s)
        {
            case GameState.BOUBA:
                skillTree.SetTrigger("close");
                bouba.SetTrigger("open");
                break;
            case GameState.SKILLTREE:
                bouba.SetTrigger("close");
                skillTree.SetTrigger("open");
                break;
        }
    }
}

public enum GameState
{
    BOUBA,
    SKILLTREE,
}

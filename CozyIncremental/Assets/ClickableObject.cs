using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickableObject : MonoBehaviour
{
    private Animator animator;
    public UnityEvent onClick;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hover()
    {
        animator.SetTrigger("Highlighted");
    }

    public void Click()
    {
        animator.SetTrigger("Pressed");
        onClick.Invoke();
    }

    public void UnClick()
    {
        animator.SetTrigger("Highlighted");

    }

    public void UnHover()
    {
        animator.SetTrigger("Normal");
    }

    public void Disable()
    {

    }

    public void Enable()
    {

    }

    private void OnMouseEnter()
    {
        Hover();
    }

    private void OnMouseDown()
    {
        Click();
    }

    private void OnMouseUp()
    {
        UnClick();
    }

    private void OnMouseExit()
    {
        UnHover();
    }
}

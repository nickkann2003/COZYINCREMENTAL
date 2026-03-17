using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickableObject : MonoBehaviour
{
    #nullable enable
    [SerializeField] private Animator? animator;
    public UnityEvent? onClick;
    public UnityEvent? onHover;
    public UnityEvent? unHover;

    protected bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Hover()
    {
        if (animator)
            animator?.SetTrigger("Highlighted");
        onHover?.Invoke();
    }

    public virtual void Click()
    {
        if (animator)
            animator?.SetTrigger("Pressed");
        onClick?.Invoke();
    }

    public virtual void UnClick()
    {
        if (animator)
            animator?.SetTrigger("Highlighted");

    }

    public virtual void UnHover()
    {
        if(animator)
            animator?.SetTrigger("Normal");
        unHover?.Invoke();
    }

    public virtual void Disable()
    {
        if (animator)
            animator?.SetBool("active", false);
        active = false;
    }

    public virtual void Enable()
    {
        if (animator)
            animator?.SetBool("active", true);
        active = true;
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

    private void OnEnable()
    {
        if (animator)
            animator?.SetBool("active", active);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickableObject : MonoBehaviour
{
    [SerializeField] private Animator? animator;
    public UnityEvent onClick;
    public UnityEvent onHover;
    public UnityEvent unHover;
    private bool active = true;

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
        animator?.SetTrigger("Highlighted");
        onHover.Invoke();
    }

    public void Click()
    {
        animator?.SetTrigger("Pressed");
        onClick.Invoke();
    }

    public void UnClick()
    {
        animator?.SetTrigger("Highlighted");

    }

    public void UnHover()
    {
        animator?.SetTrigger("Normal");
        unHover.Invoke();
    }

    public void Disable()
    {
        animator?.SetBool("active", false);
        active = false;
    }

    public void Enable()
    {
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
        animator?.SetBool("active", active);
    }
}

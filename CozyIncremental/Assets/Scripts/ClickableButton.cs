using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class ClickableButton : ClickableObject
{
    private SpriteRenderer r;

    [SerializeField]  private Sprite hoverSprite;
    [SerializeField]  private Sprite pressedSprite;
    [SerializeField]  private Sprite normalSprite;
    [SerializeField]  private Sprite disabledSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        r = GetComponent<SpriteRenderer>();
    }

    public override void Hover()
    {
        base.Hover();
        if(active)
            r.sprite = hoverSprite;
    }

    public override void Click()
    {
        base.Click();
        if(active)
            r.sprite = pressedSprite;
    }

    public override void UnClick()
    {
        base.UnClick();
        if(active)
            r.sprite = hoverSprite;
    }

    public override void UnHover()
    {
        base.UnHover();
        if(active)
            r.sprite = normalSprite;
    }

    public override void Disable()
    {
        base.Disable();
        r.sprite = disabledSprite;
    }

    public override void Enable()
    {
        base.Enable();
        r.sprite = normalSprite;
    }
}

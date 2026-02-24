using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorShifter : MonoBehaviour
{
    public Color color = Color.gray;
    public Color targetColor;
    public float transitionRate;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        color = Color.Lerp(color, targetColor, transitionRate*Time.deltaTime);
        spriteRenderer.color = color;
    }
}

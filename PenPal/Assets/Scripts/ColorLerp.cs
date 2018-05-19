using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColorLerp : MonoBehaviour
{

    // Use this for initialization
    public enum PlayTrigger
    {
        Start,
        Enable,
        None
    }
    public PlayTrigger playOn = PlayTrigger.None;
    public Color startColor = Color.white;
    public Color endColor = Color.white;
    public float duration = 0;
    public float initialDelay = 0;
    private SpriteRenderer spriteRenderer;
    private IEnumerator colorCoroutine;
    void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        colorCoroutine = colorChange(startColor,endColor);
        
    }
    void OnEnable()
    {
        if (playOn == PlayTrigger.Enable)
            startColorChange(1);
    }
    void Start()
    {
        if (playOn == PlayTrigger.Start)
            startColorChange(1);
    }


    private IEnumerator colorChange(Color start, Color end)
    {
        spriteRenderer.color = start;
        while (initialDelay > 0)
        {
            initialDelay -= Time.deltaTime;
            yield return null;
        }
        float elapsedTime = 0;
        if (duration == 0)
        {
            spriteRenderer.color = end;
        }
        while (elapsedTime < duration)
        {
            spriteRenderer.color = Color.Lerp(start,
                end, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = end;
    }
    public void startColorChange(int direction)
    {
        StopCoroutine(colorCoroutine);
        if(direction == 1)
        {
            colorCoroutine = colorChange(startColor,endColor);

        }
        else
        {
            colorCoroutine = colorChange(endColor, startColor);

        }
        StartCoroutine(colorCoroutine);
    }
    public void setColors(Color newStartColor, Color newEndColor)
    {
        startColor = newStartColor;
        endColor = newEndColor;
    }
}

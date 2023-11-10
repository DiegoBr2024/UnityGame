using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class BlinkSprite : MonoBehaviour
{
    public float Interval;

    private SpriteRenderer spriteRenderer;

    private float nextStateChange;
    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        nextStateChange = Time.time + Interval;


    }

    private void Update()
    {
        if (Time.time > nextStateChange)
        {


            spriteRenderer.enabled = !spriteRenderer.enabled;
            nextStateChange = Time.time + Interval;

        }
    }

}

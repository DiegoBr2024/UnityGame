using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class BlinkTileMapColor : MonoBehaviour
{
    public float Interval;

    public Color color1;
    public Color color2;

    private Tilemap tilemap;

    private float nextStateChange;

    private bool isColor1;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        tilemap.color = color1;
        isColor1 = true;
        nextStateChange = Time.time + Interval;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextStateChange)
        {

            tilemap.color = isColor1 ? color2 : color1;
            isColor1 = !isColor1;
            nextStateChange = Time.time + Interval;
        }
    }
}

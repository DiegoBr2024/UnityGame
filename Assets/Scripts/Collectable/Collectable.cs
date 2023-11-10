using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public bool IsVictoryCodition;

    public int Score;

    public event Action<int, Collectable> Oncollected;



    private void OnTriggerEnter2D(Collider2D collision)
    {

        Oncollected?.Invoke(Score, this);
        Destroy(gameObject);
    }


}

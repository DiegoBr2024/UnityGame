using System.Collections.Generic;
using UnityEngine;


public class GhostHouse : MonoBehaviour
{
    List<GhostAI> allghost;
    public float LeaveHouseInterval;
    public float leavehosetimer;
    private void Awake()
    {
        allghost = new List<GhostAI>();
        leavehosetimer = LeaveHouseInterval;
    }

    private void Update()
    {
        if (allghost.Count > 0)
        {
            leavehosetimer -= Time.deltaTime;
            if (leavehosetimer <= 0)
            {
                leavehosetimer += LeaveHouseInterval;
                allghost[0].LeaveHouse();
                allghost.RemoveAt(0);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (allghost.Count == 0)
        {
            leavehosetimer = LeaveHouseInterval;
        }
        var ghost = collision.GetComponent<GhostAI>();
        collision.GetComponent<GhostAI>().recover();
        allghost.Add(ghost);
    }
}

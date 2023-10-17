using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(pacman))]
public class GhostMove : MonoBehaviour
{
    private pacman motor;
    private Vector2 boxsize;

    private LayerMask colionslayermask;
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<pacman>();
        motor.onalinedwithgrid += Motoronalinewithgrid;
        Motoronalinewithgrid();
        boxsize = GetComponent<BoxCollider>().size;
        
    }
    private void Motoronalinewithgrid()
    {
        var pacman = GameObject.FindWithTag("Player").transform;

        var closesdistance = float.MaxValue;
        Direction finaldirection = Direction.none;

        var dist = Vector2.Distance(transform.position + Vector3.up, pacman.position);
        if (dist <= closesdistance)
        {
            closesdistance = dist;
            finaldirection = Direction.up;
        }
         dist = Vector2.Distance(transform.position + Vector3.left, pacman.position);
        if (dist <= closesdistance)
        {
            closesdistance = dist;
            finaldirection = Direction.left;
        }
        dist = Vector2.Distance(transform.position + Vector3.down, pacman.position);
        if (dist <= closesdistance)
        {
            closesdistance = dist;
            finaldirection = Direction.down;
        }
        dist = Vector2.Distance(transform.position + Vector3.right, pacman.position);
        if (dist <= closesdistance)
        {
            closesdistance = dist;
            finaldirection = Direction.right;
        }



        motor.SetMoveDirection(finaldirection);
    }

   /* private bool checkdiurection(Direction direction)
    {
        switch(direction){

            case Direction.up:

                Physics2D.BoxCast(transform.position, boxsize,0,Vector2.up,1f,)

                break;


        }
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}

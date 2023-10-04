using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{

    none,
    up,
    left,
    down,
    right
}


public class pacman : MonoBehaviour
{

    public float MoveSpeed;

    public Rigidbody2D rigidbody;
    public Vector2 CurrentMovimentDirection;
    public Vector2 desiredMovimentDirection;

    private Vector2 boxsize;

    // Start is called before the first frame update
    void Start()
    {
        boxsize = GetComponent<BoxCollider2D>().size;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public event Action<Direction> OnDirectionChaged;

    public Direction currentmovedirection
    {
        get
        {
            if (CurrentMovimentDirection.y > 0)//up
            {
                return Direction.up;
            }
            if (CurrentMovimentDirection.x < 0)//left
            {
                return Direction.left;
            }
            if (CurrentMovimentDirection.x > 0)// right
            {
                return Direction.right;
            }
            if (CurrentMovimentDirection.y < 0)//down
            {
                return Direction.down;
            }
            return Direction.none;
        }
    }

    public void SetMoveDirection(Direction newdirection)
    {
        switch (newdirection)
        {
            default:
            case Direction.none:
                desiredMovimentDirection = Vector2.zero;
                break;

            case Direction.up:
                desiredMovimentDirection = Vector2.up;
                break;

            case Direction.down:
                desiredMovimentDirection = Vector2.down;
                break;

            case Direction.right:
                desiredMovimentDirection = Vector2.right;
                break;

            case Direction.left:
                desiredMovimentDirection = Vector2.left;
                break;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float movedistance = MoveSpeed * Time.fixedDeltaTime;
        var nextposition = rigidbody.position + CurrentMovimentDirection *movedistance;

        if (CurrentMovimentDirection.y > 0)//up
        {
            var maxY = Mathf.CeilToInt(rigidbody.position.y);
            if (nextposition.y >= maxY)
            {
                transform.position = new Vector2(rigidbody.position.x, maxY);
                movedistance = nextposition.y - maxY;
            }
        } 
        if (CurrentMovimentDirection.x < 0)//left
        {
            var minX = Mathf.FloorToInt(rigidbody.position.x);
            if (nextposition.x <= minX)
            {
                transform.position = new Vector2(minX, rigidbody.position.y);
                movedistance = minX -nextposition.x ;
            }
        } 
        if (CurrentMovimentDirection.x > 0 )// right
        {
            var maxX = Mathf.CeilToInt(rigidbody.position.x);
            if (nextposition.x >= maxX)
            {
                transform.position = new Vector2(maxX, rigidbody.position.y);
                movedistance = nextposition.x - maxX;
            }
        }  
        if (CurrentMovimentDirection.y < 0)//down
        {
            var minX = Mathf.FloorToInt(rigidbody.position.y);
            if (nextposition.y <= minX)
            {
                transform.position = new Vector2(rigidbody.position.x, minX );
                movedistance = minX- nextposition.y;
            }
        }
        

       
       Physics2D.SyncTransforms();
        if ( rigidbody.position.y == Mathf.CeilToInt(rigidbody.position.y) )
        {
            if ( rigidbody.position.x == Mathf.CeilToInt(rigidbody.position.x )) {
               

                if (CurrentMovimentDirection != desiredMovimentDirection)
                {
                    
                    if (!Physics2D.BoxCast(rigidbody.position, boxsize, 0, desiredMovimentDirection, 0.6f, 1 << LayerMask.NameToLayer("colisor")))
                    {
                        CurrentMovimentDirection = desiredMovimentDirection;
                        OnDirectionChaged?.Invoke(currentmovedirection);
                    }

                }
                if (Physics2D.BoxCast(rigidbody.position, boxsize, 0, CurrentMovimentDirection, 0.6f, 1 << LayerMask.NameToLayer("colisor")))
                {
                    CurrentMovimentDirection = Vector2.zero;
                    OnDirectionChaged?.Invoke(currentmovedirection);
                }
            }

        } 
       
        rigidbody.MovePosition(rigidbody.position + CurrentMovimentDirection * movedistance);



    }
}
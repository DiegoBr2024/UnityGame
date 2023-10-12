using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPac : MonoBehaviour
{
    public pacman motor;

    public Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        motor.OnDirectionChaged += Motor_OnDirectionChaged;
    }

    private void Motor_OnDirectionChaged(Direction direction)
    {
        switch (direction)
        {
            case Direction.none:
               
                break; 

            case Direction.up:
                transform.rotation = Quaternion.Euler(0,0,180);
                break; 

            case Direction.left:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break; 

            case Direction.right:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;

            case Direction.down:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
            

    }
}

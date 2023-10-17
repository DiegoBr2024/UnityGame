using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPac : MonoBehaviour
{
    public pacman motor;

    public Animator animator;
    private void Start()
    {
       
        motor.OnDirectionChaged += Motor_OnDirectionChaged;
    }

    private void Motor_OnDirectionChaged(Direction direction)
    {
        switch (direction)
        {
            case Direction.none:
                animator.SetBool("Moving", false);
                break; 

            case Direction.up:
                transform.rotation = Quaternion.Euler(0,0,90);
                animator.SetBool("Moving", true);
                break; 

            case Direction.left:
                animator.SetBool("Moving", true);
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break; 

            case Direction.right:
                animator.SetBool("Moving", true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;

            case Direction.down:
                animator.SetBool("Moving", true);
                transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
        }
            

    }
}

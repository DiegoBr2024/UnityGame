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
                break; 

            case Direction.up:
                break; 

            case Direction.left:
                break; 

            case Direction.right:
                break;

            case Direction.down:
                break;
        }
            

    }
}

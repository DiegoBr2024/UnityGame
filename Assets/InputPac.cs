using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(pacman))] 
public class InputPac : MonoBehaviour
{
    private pacman motor;
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<pacman>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {

            motor.SetMoveDirection(Direction.up);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {

            motor.SetMoveDirection(Direction.left);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {

            motor.SetMoveDirection(Direction.down);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {

            motor.SetMoveDirection(Direction.right);
        }
    }
}

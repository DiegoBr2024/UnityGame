using System;
using UnityEngine;

[RequireComponent(typeof(pacman))]
public class GhostMove : MonoBehaviour
{
    private pacman motor;
    private Vector2 boxsize;

    private LayerMask colionslayermask;
    private Vector2 TargetMovingLocation;

    public void allowreversed()
    {
        allowreversedirection = true;
    }
    public event Action OnUpdateMoveTarget;
    private bool allowreversedirection;
    public void setTargetMovelocation(Vector2 targetmove)
    {
        TargetMovingLocation = targetmove;
    }
    // Start is called before the first frame update
    void Awake()
    {
        motor = GetComponent<pacman>();
        motor.onalinedwithgrid += Motoronalinewithgrid;
        Motoronalinewithgrid();
        boxsize = GetComponent<BoxCollider2D>().size;
        allowreversedirection = false;
    }

    public pacman Pacman { get { return motor; } }
    private void Motoronalinewithgrid()
    {
        OnUpdateMoveTarget?.Invoke();
        changemovedirection();
    }

    private void changemovedirection()
    {
        var pacman = GameObject.FindWithTag("Player").transform;

        var closesdistance = float.MaxValue;
        Direction finaldirection = Direction.none;

        UpdateDirectionFinal(Direction.up, Vector3.up, ref closesdistance, ref finaldirection);
        UpdateDirectionFinal(Direction.left, Vector3.left, ref closesdistance, ref finaldirection);
        UpdateDirectionFinal(Direction.right, Vector3.right, ref closesdistance, ref finaldirection);
        UpdateDirectionFinal(Direction.down, Vector3.down, ref closesdistance, ref finaldirection);






        motor.SetMoveDirection(finaldirection);
        allowreversedirection = false;
    }

    private void UpdateDirectionFinal(Direction direcao, Vector3 offset, ref float closesdistance, ref Direction FinalDirection)
    {
        var pac = GameObject.Find("Pacman").transform.position;

        var dist = Vector2.Distance(transform.position + offset, TargetMovingLocation);
        if (checkdiurection(direcao))
        {
            if (dist < closesdistance)
            {
                closesdistance = dist;
                FinalDirection = direcao;
            }
        }
    }

    private bool checkdiurection(Direction direction)
    {
        switch (direction)
        {

            case Direction.up:
                return !Physics2D.BoxCast(transform.position, boxsize, 0, Vector2.up, 1f, motor.colisionLayer) && motor.currentmovedirection != Direction.down || allowreversedirection;

            case Direction.left:
                return !Physics2D.BoxCast(transform.position, boxsize, 0, Vector2.left, 1f, motor.colisionLayer) && motor.currentmovedirection != Direction.right || allowreversedirection;

            case Direction.right:
                return !Physics2D.BoxCast(transform.position, boxsize, 0, Vector2.right, 1f, motor.colisionLayer) && motor.currentmovedirection != Direction.left || allowreversedirection;

            case Direction.down:
                return !Physics2D.BoxCast(transform.position, boxsize, 0, Vector2.down, 1f, motor.colisionLayer) && motor.currentmovedirection != Direction.up || allowreversedirection;

        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System;
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
    private Vector3 InitialPosition;

    public Rigidbody2D rigidbory1;
    public Vector2 CurrentMovimentDirection;
    public Vector2 desiredMovimentDirection;

    private Vector2 boxsize;
    private LayerMask layer;



    // Start is called before the first frame update
    void Start()
    {

        desiredMovimentDirection = Vector2.zero;
        CurrentMovimentDirection = Vector2.zero;
        //layer = 1 << LayerMask.NameToLayer("collider") | 1 << LayerMask.NameToLayer("Gates"); esse codigo faz o mesmo que o de baixo
        layer = LayerMask.GetMask(new string[] { "collider", "Gates" }); //este codigo faz o mesmo que o decima

        boxsize = GetComponent<BoxCollider2D>().size;
        rigidbory1 = GetComponent<Rigidbody2D>();
        InitialPosition = transform.position;
        ColliderWithGates(true);
    }

    public void ResetPosition()
    {
        desiredMovimentDirection = Vector2.zero;
        CurrentMovimentDirection = Vector2.zero;
        transform.position = InitialPosition;
        onResetPosition?.Invoke();
    }

    public LayerMask colisionLayer
    {
        get => layer;
    }

    public void ColliderWithGates(bool shouldCollider)
    {
        if (shouldCollider)
        {
            layer = LayerMask.GetMask(new string[] { "collider", "Gates" });
        }
        else
        {
            layer = LayerMask.GetMask(new string[] { "collider" });
        }
    }

    public event Action OnDisabled;
    public event Action onResetPosition;
    public event Action onalinedwithgrid;
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

            default:
            case Direction.none:
                CurrentMovimentDirection = Vector2.zero;
                break;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float movedistance = MoveSpeed * Time.fixedDeltaTime;
        var nextposition = rigidbory1.position + CurrentMovimentDirection * movedistance;

        if (CurrentMovimentDirection.y > 0)//up
        {
            var maxY = Mathf.CeilToInt(rigidbory1.position.y);
            if (nextposition.y >= maxY)
            {
                transform.position = new Vector2(rigidbory1.position.x, maxY);
                movedistance = nextposition.y - maxY;
            }
        }
        if (CurrentMovimentDirection.x < 0)//left
        {
            var minX = Mathf.FloorToInt(rigidbory1.position.x);
            if (nextposition.x <= minX)
            {
                transform.position = new Vector2(minX, rigidbory1.position.y);
                movedistance = minX - nextposition.x;
            }
        }
        if (CurrentMovimentDirection.x > 0)// right
        {
            var maxX = Mathf.CeilToInt(rigidbory1.position.x);
            if (nextposition.x >= maxX)
            {
                transform.position = new Vector2(maxX, rigidbory1.position.y);
                movedistance = nextposition.x - maxX;
            }
        }
        if (CurrentMovimentDirection.y < 0)//down
        {
            var minX = Mathf.FloorToInt(rigidbory1.position.y);
            if (nextposition.y <= minX)
            {
                transform.position = new Vector2(rigidbory1.position.x, minX);
                movedistance = minX - nextposition.y;
            }
        }



        Physics2D.SyncTransforms();
        if (rigidbory1.position.y == Mathf.CeilToInt(rigidbory1.position.y)
            && rigidbory1.position.x == Mathf.CeilToInt(rigidbory1.position.x) || CurrentMovimentDirection == Vector2.zero)
        {


            onalinedwithgrid?.Invoke();
            if (CurrentMovimentDirection != desiredMovimentDirection)
            {

                if (!Physics2D.BoxCast(rigidbory1.position, boxsize, 0, desiredMovimentDirection, 1f, layer))
                {
                    CurrentMovimentDirection = desiredMovimentDirection;
                    OnDirectionChaged?.Invoke(currentmovedirection);
                }

            }
            if (Physics2D.BoxCast(rigidbory1.position, boxsize, 0, CurrentMovimentDirection, 1f, layer))
            {
                CurrentMovimentDirection = Vector2.zero;
                OnDirectionChaged?.Invoke(currentmovedirection);
            }


        }

        rigidbory1.MovePosition(rigidbory1.position + CurrentMovimentDirection * movedistance);



    }
    private void OnDisable()
    {
        OnDisabled?.Invoke();
    }

}

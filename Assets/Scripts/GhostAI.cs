using UnityEngine;

[RequireComponent(typeof(GhostMove))]
public class GhostAI : MonoBehaviour
{
    private GhostMove ghostMove;
    private Transform pacman;

    public void StopMoving()
    {
        ghostMove.Pacman.enabled = false;
    }

    public void Reset()
    {
        ghostMove.Pacman.ResetPosition();
    }

    public void StartMoving()
    {
        ghostMove.Pacman.enabled = true;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        ghostMove = GetComponent<GhostMove>();
        ghostMove.OnUpdateMoveTarget += GhostMove_OnUpdateMoveTarget;

        pacman = GameObject.Find("Pacman").transform;
    }

    private void GhostMove_OnUpdateMoveTarget()
    {
        ghostMove.setTargetMovelocation(pacman.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Life>().RemoveLife();
        }
    }
}

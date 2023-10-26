using UnityEngine;

[RequireComponent(typeof(GhostMove))]
public class GhostAI : MonoBehaviour
{
    private GhostMove ghostMove;
    private Transform pacman;
<<<<<<< Updated upstream
=======

    private void Start()
    {


    }

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
>>>>>>> Stashed changes
    // Start is called before the first frame update
    private void Start()
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
}

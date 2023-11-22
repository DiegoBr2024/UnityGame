using System;
using UnityEngine;

[RequireComponent(typeof(GhostMove))]



public class GhostAI : MonoBehaviour
{
    private GhostMove ghostMove;
    private Transform pacman;

    private bool Leavehouse;

    public enum GhostState
    {
        Active,
        vulnerable,
        vulnerabilytEnding,
        defeated
    }



    public void recover()
    {
        ghostMove.Pacman.ColliderWithGates(true);
        state = GhostState.Active;
        OnGhosStatehanged?.Invoke(state);
        Leavehouse = false;
    }

    public void LeaveHouse()
    {
        ghostMove.Pacman.ColliderWithGates(false);
        Leavehouse = true;
    }

    public void SetVunerable(float Duration)
    {
        VunerabilidadeTimer = Duration;
        state = GhostState.vulnerable;
        OnGhosStatehanged?.Invoke(state);
        ghostMove.allowreversed();
    }

    public float vunerabilitEndingTime;

    private GhostState state;

    private float VunerabilidadeTimer;

    public event Action<GhostState> OnGhosStatehanged;

    public void StopMoving()
    {
        ghostMove.Pacman.enabled = false;
    }

    public void Reset()
    {
        ghostMove.Pacman.ResetPosition();
        state = GhostState.Active;
        OnGhosStatehanged?.Invoke(state);
        Leavehouse = false;
    }

    public void StartMoving()
    {
        ghostMove.Pacman.enabled = true;
    }
    // Start is called before the first frame update
    private void Start()
    {
        ghostMove = GetComponent<GhostMove>();
        ghostMove.OnUpdateMoveTarget += GhostMove_OnUpdateMoveTarget;

        pacman = GameObject.Find("Pacman").transform;
        state = GhostState.Active;
        Leavehouse = false;
    }



    private void GhostMove_OnUpdateMoveTarget()
    {
        // ghostMove.setTargetMovelocation(pacman.transform.position);
        switch (state)
        {
            case GhostState.Active:
                if (Leavehouse)
                {
                    if (transform.position == new Vector3(0, 3, 0))
                    {
                        Leavehouse = false;
                        ghostMove.Pacman.ColliderWithGates(true);
                        ghostMove.setTargetMovelocation(pacman.transform.position);
                    }
                    else ghostMove.setTargetMovelocation(new Vector3(0, 3, 0));

                }
                else
                {
                    ghostMove.setTargetMovelocation(pacman.transform.position);
                }
                break;

            case GhostState.vulnerable:
            case GhostState.vulnerabilytEnding:
                ghostMove.setTargetMovelocation((transform.position - pacman.position) * 2);
                break;

            case GhostState.defeated:
                ghostMove.setTargetMovelocation(Vector2.zero);
                break;

        }

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GhostState.Active:
                OnGhosStatehanged?.Invoke(state);
                break;

            case GhostState.vulnerable:

                VunerabilidadeTimer -= Time.deltaTime;
                if (VunerabilidadeTimer <= vunerabilitEndingTime)
                {
                    state = GhostState.vulnerabilytEnding;
                    OnGhosStatehanged?.Invoke(state);
                }
                break;


            case GhostState.vulnerabilytEnding:
                VunerabilidadeTimer -= Time.deltaTime;
                if (VunerabilidadeTimer <= 0)
                {
                    state = GhostState.Active;
                    OnGhosStatehanged?.Invoke(state);
                }

                break;



        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (state)
        {
            case GhostState.Active:
                if (collision.CompareTag("Player"))
                {
                    collision.GetComponent<Life>().RemoveLife();
                }
                break;

            case GhostState.vulnerable:
            case GhostState.vulnerabilytEnding:
                if (collision.CompareTag("Player"))
                {

                    ghostMove.Pacman.ColliderWithGates(false);
                    state = GhostState.defeated;
                    OnGhosStatehanged?.Invoke(state);
                }
                break;

        }

    }
}

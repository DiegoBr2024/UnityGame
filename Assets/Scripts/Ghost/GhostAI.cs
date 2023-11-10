using System;
using UnityEngine;

[RequireComponent(typeof(GhostMove))]



public class GhostAI : MonoBehaviour
{
    private GhostMove ghostMove;
    private Transform pacman;

    public enum GhostState
    {
        Active,
        vulnerable,
        vulnerabilytEnding,
        defeated
    }

    public void SetVunerable(float Duration)
    {
        VunerabilidadeTimer = Duration;
        state = GhostState.vulnerable;
        OnGhosStatehanged?.Invoke(state);
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
    }



    private void GhostMove_OnUpdateMoveTarget()
    {
        ghostMove.setTargetMovelocation(pacman.transform.position);
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
                    state = GhostState.defeated;
                    OnGhosStatehanged?.Invoke(state);
                }
                break;

        }

    }
}

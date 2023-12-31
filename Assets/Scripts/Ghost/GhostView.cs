using UnityEngine;

public class GhostView : MonoBehaviour
{
    public enum GhostType
    {
        Blinky,
        Pinky,
        Inky,
        Clyd
    }
    public pacman pacman;
    public GhostAI ghostAI;
    public Animator animator;

    public GhostType type;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetInteger("GhostType", (int)type);
        pacman.OnDirectionChaged += Pacman_OnDirectionChaged;
        ghostAI.OnGhosStatehanged += GhostAI_OnGhosStatehanged;
    }

    private void GhostAI_OnGhosStatehanged(GhostAI.GhostState GhostState)
    {
        animator.SetInteger("State", (int)GhostState);


    }

    private void Pacman_OnDirectionChaged(Direction direction)
    {
        animator.SetInteger("Direction", (int)direction - 1);
    }
}

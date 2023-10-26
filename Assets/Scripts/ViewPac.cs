using UnityEngine;

public class ViewPac : MonoBehaviour
{
    public pacman motor;
    public Life life;
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip LifeLostsong;
    private void Start()
    {


        life.OnLifeRemoved += Life_OnLifeRemoved;
        motor.OnDirectionChaged += Motor_OnDirectionChaged;
        motor.onResetPosition += Motor_onResetPosition;
        motor.OnDisabled += Motor_OnDisabled;


    }

    private void Motor_OnDisabled()
    {
        animator.speed = 0;
    }

    private void Motor_onResetPosition()
    {
        animator.SetBool("Moving", false);
        animator.SetBool("Dead", false);
    }

    private void Life_OnLifeRemoved(int obj)
    {
        animator.speed = 1;
        audioSource.PlayOneShot(LifeLostsong);
        animator.SetBool("Moving", false);
        animator.SetBool("Dead", true);
    }

    private void Motor_OnDirectionChaged(Direction direction)
    {


        switch (direction)
        {
            case Direction.none:
                animator.SetBool("Moving", false);
                animator.speed = 1;
                break;

            case Direction.up:

                transform.rotation = Quaternion.Euler(0, 0, 90);
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

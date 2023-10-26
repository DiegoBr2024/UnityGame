using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject ReadyMessage;

    public GameObject GameOverMessage;

    public AudioSource AudioSource;

    public AudioClip begginMusic;

    public GameManager manager;
    // Start is called before the first frame update
    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        AudioSource.PlayOneShot(begginMusic);
        manager.OnGameStated += Manager_OnGameStated;
        manager.OnGameOver += Manager_OnGameOver;

    }

    private void Manager_OnGameOver()
    {
        GameOverMessage.SetActive(true);
    }

    private void Manager_OnGameStated()
    {
        ReadyMessage.SetActive(false);
    }

    // Update is called once per frame

}

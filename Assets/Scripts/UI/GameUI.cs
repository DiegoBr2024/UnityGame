using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject ReadyMessage;

    public GameObject GameOverMessage;

    public BlinkTileMapColor BlinkTileMap;

    public AudioSource AudioSource;

    public AudioClip begginMusic;

    public GameManager manager;
    // Start is called before the first frame update
    private void Start()
    {
        print(gameObject.name);
        manager = FindObjectOfType<GameManager>();
        AudioSource.PlayOneShot(begginMusic);
        manager.OnGameStated += Manager_OnGameStated;
        manager.OnGameOver += Manager_OnGameOver;
        manager.OnVictory += Manager_OnVictory;

    }

    private void Manager_OnVictory()
    {
        BlinkTileMap.enabled = true;
    }

    private void Manager_OnGameStated()
    {
        ReadyMessage.SetActive(false);
    }
    private void Manager_OnGameOver()
    {
        GameOverMessage.SetActive(true);
    }



    // Update is called once per frame

}

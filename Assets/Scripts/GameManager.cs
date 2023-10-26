using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private enum GameState
    {
        Starting,
        Playing,
        GameOver,
        Victory,
        LifeLost

    }
    public float LifeLostTimer;
    private float LifeTimer;
    private GhostAI[] allGhost;
    private pacman pacman1;
    public bool IsGameOver;
    private int VictoryCount;
    public float StartupTimer;
    private GameState gameState;
    // Start is called before the first frame update
    void Awake()
    {
        VictoryCount = 0;
        var allcoletable = FindObjectsOfType<Collectable>();
        foreach (var coleteble in allcoletable)
        {
            VictoryCount++;
            coleteble.Oncollected += Coleteble_Oncollected;
        }
        gameState = GameState.Starting;

        var player = GameObject.FindWithTag("Player");
        pacman1 = player.GetComponent<pacman>();

        allGhost = FindObjectsOfType<GhostAI>();
        StopAllCharacters();

        player.GetComponent<Life>().OnLifeRemoved += Pacman_OnLifeRemoved;
    }

    private void Pacman_OnLifeRemoved(int RemaniningLives)
    {
        StopAllCharacters();
        LifeTimer = LifeLostTimer;
        gameState = GameState.LifeLost;
        IsGameOver = RemaniningLives <= 0;
    }

    private void Coleteble_Oncollected(int obj, Collectable collectable)
    {
        VictoryCount--;
        if (VictoryCount <= 0)
        {
            OnVictory?.Invoke();
            StopAllCharacters();
            gameState = GameState.Victory;
        }
        collectable.Oncollected -= Coleteble_Oncollected;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (gameState)
        {
            case GameState.Starting:
                StartupTimer -= Time.deltaTime;
                if (StartupTimer <= 0)
                {
                    gameState = GameState.Playing; StartAllCharacters(); OnGameStated?.Invoke();

                }

                break;
            case GameState.GameOver:
            case GameState.Victory:
                if (Input.anyKey) { SceneManager.LoadScene(0); }
                break;

            case GameState.LifeLost:
                LifeTimer -= Time.deltaTime;
                if (LifeTimer <= 0)
                {
                    if (IsGameOver)
                    {
                        gameState = GameState.GameOver;
                        OnGameOver?.Invoke();
                    }
                    else
                    {

                        ResetAllCharacters();
                        gameState = GameState.Playing;

                    }
                }
                break;

        }
    }

    public event Action OnGameStated;
    public event Action OnVictory;
    public event Action OnGameOver;

    private void ResetAllCharacters()
    {
        pacman1.ResetPosition();
        foreach (var ghost in allGhost)
        {

            ghost.Reset(); //possivel erro

        }

        StartAllCharacters();
    }

    private void StopAllCharacters()
    {
        pacman1.enabled = false;
        foreach (var ghost in allGhost)
        {

            ghost.StopMoving();
        }

    }
    private void StartAllCharacters()
    {
        pacman1.enabled = true;
        foreach (var ghost in allGhost)
        {

            ghost.StartMoving();

        }

    }
}

using System;
using UnityEngine;

public class GameScore : MonoBehaviour
{

    private int currentScore;

    private int highScore;

    public int CurrentScore { get { return currentScore; } }
    public int HighScore { get { return highScore; } }

    public event Action<int> OnScoreChanged;
    public event Action<int> OnHighScoreChanged;

    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        var allcolletle = FindObjectsOfType<Collectable>();

        foreach (Collectable c in allcolletle)
        {
            c.Oncollected += C_Oncollected;
        }


    }

    private void C_Oncollected(int Score, Collectable colletble)
    {
        OnScoreChanged?.Invoke(Score);
        currentScore += Score;
        if (currentScore >= highScore)
        {
            highScore = currentScore;

            OnHighScoreChanged?.Invoke(highScore);

        }
        colletble.Oncollected -= C_Oncollected;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }
    // Update is called once per frame
    void Update()
    {

    }
}

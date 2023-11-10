using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI CurrentScoreText;

    public TextMeshProUGUI HighScoreText;

    // Start is called before the first frame update
    void Start()
    {
        var GameScore = FindObjectOfType<GameScore>();
        GameScore.OnScoreChanged += GameScore_OnScoreChanged;
        GameScore.OnHighScoreChanged += GameScore_OnHighScoreChanged;
        HighScoreText.text = GameScore.HighScore.ToString();
    }



    private void GameScore_OnScoreChanged(int ScoreChanged)
    {
        CurrentScoreText.text = (ScoreChanged + int.Parse(CurrentScoreText.text)).ToString();
    }

    private void GameScore_OnHighScoreChanged(int HighScore)
    {
        HighScoreText.text = HighScore.ToString();
    }

    // Update is called once per frame

}

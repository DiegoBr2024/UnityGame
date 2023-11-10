using UnityEngine;

public class LivesUI : MonoBehaviour
{
    public GameObject[] Lives;
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindWithTag("Player");
        var life = player.GetComponent<Life>();
        life.OnLifeRemoved += Life_OnLifeRemoved;
        UpdatesLivesSprites(life.Lives);
    }


    private void Life_OnLifeRemoved(int RemainingLives)
    {
        UpdatesLivesSprites(RemainingLives);
    }

    private void UpdatesLivesSprites(int CurrentLives)
    {
        for (int i = 3; i < Lives.Length; i++)
        {
            Lives[i].SetActive(i < CurrentLives);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

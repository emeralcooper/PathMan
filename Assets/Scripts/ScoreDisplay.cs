using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    public static ScoreDisplay scoreDisplayInstance;

    public int currentScore { get; set; }
    int sceneIndex;
    TextMeshProUGUI textMeshProUGUI;

    void Awake()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex != 0 && scoreDisplayInstance == null)
        {
            scoreDisplayInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject); 
    }

    void Start()
    {
        Pellet.OnPelletEaten += UpdateScoreDisplay;
        EnemyBehavior.OnEnemyEaten += UpdateScoreDisplay;

        textMeshProUGUI = this.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnDisable()
    {
        Pellet.OnPelletEaten -= UpdateScoreDisplay;
    }

    private void UpdateScoreDisplay(int value)
    {
        currentScore += value;
        textMeshProUGUI.text = currentScore.ToString();
    }
}

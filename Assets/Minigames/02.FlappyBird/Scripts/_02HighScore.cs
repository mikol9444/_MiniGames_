using TMPro;
using System.IO;
using UnityEngine;

public class _02HighScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    private float highScore;
    private string saveFilePath;
    public static _02HighScore Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }
    private void Start()
    {
        // Set the save file path
        saveFilePath = Path.Combine(Application.persistentDataPath, "highscore.json");

        // Load the high score from the save file
        LoadHighScore();

        UpdateHighScoreText();
    }

    public void UpdateHighScore(float currentScore)
    {
        if (currentScore > highScore)
        {
            // Update the high score if the current score is higher
            highScore = currentScore;
            SaveHighScore();
            UpdateHighScoreText();
        }
    }

    public void UpdateHighScoreText()
    {
        if(highScoreText)
        // Update the UI text with the current high score
        highScoreText.text = "High Score: " + highScore.ToString("F2") + " m";
    }

    private void SaveHighScore()
    {
        // Serialize the high score data to JSON and save it to a file
        string json = JsonUtility.ToJson(new HighScoreData(highScore));
        File.WriteAllText(saveFilePath, json);
    }

    public void LoadHighScore()
    {
        // Load the high score data from a JSON file
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);
            highScore = data.highScore;
        }
        else
        {
            highScore = 0f;
        }
    }

    private class HighScoreData
    {
        public float highScore;

        public HighScoreData(float highScore)
        {
            this.highScore = highScore;
        }
    }
}

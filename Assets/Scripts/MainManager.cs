using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text highcoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;
    private int hightscore = 0;
    private string highscorePlayerName;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        //Load highscore data
        LoadHighscoreData();

        // Show players name right from the beginning
        AddPoint(0);


    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMenu();
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{NameManagerSingelton.Instance.playerName}'s Score : {m_Points}";

        if (m_Points > hightscore) // Write new high score
        {
            hightscore = m_Points;
            highcoreText.text = $"Best Score : {NameManagerSingelton.Instance.playerName}'s : {hightscore}";
            SaveHighscoreData();
            
        }
        else if (m_Points == 0 && hightscore == 0)
        { // If no high score exists          

            highcoreText.text = $"No Highscore Yet";
        }
        else 
        { // Write latest high score          

            highcoreText.text = $"Best Score : {highscorePlayerName}'s : {hightscore}";
        }
    }

    public HighscoreSerialization LoadHighscoreData()
    {
        HighscoreSerialization data = null;
        // load latest high score
        string path = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<HighscoreSerialization>(json);

            hightscore = data.highscore;
            highscorePlayerName = data.highscorePlayerName;
            
        }
        return data;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);      
    }

    private void SaveHighscoreData()
    {
        //Save high score to JSON
        HighscoreSerialization data = new HighscoreSerialization();
        data.highscore = hightscore;
        data.highscorePlayerName = NameManagerSingelton.Instance.playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

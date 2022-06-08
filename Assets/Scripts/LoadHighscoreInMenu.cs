using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadHighscoreInMenu : MonoBehaviour
{
    private string currentHighscore;

    // Start is called before the first frame update
    void Start()
    {

        HighscoreSerialization highscoreData = new MainManager().LoadHighscoreData();
        if (highscoreData != null)
            GetComponent<Text>().text = $"Best Score: {highscoreData.highscorePlayerName} : {highscoreData.highscore}";
        else
            GetComponent<Text>().text = $"No Best Score Yet!";
    }

    // Update is called once per frameww
    void Update()
    {
        
    }
}

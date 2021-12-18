using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public GameObject gameFinishedView;
    public Text targetText;
    public void onRestart()
    {
        SceneManager.LoadScene(0);
    }

    public void onNextLevel()
    {
        CreateLevelManager.Instance.nextLevel();
        SceneManager.LoadScene(0);
        EventPool.Trigger("changeLevel");
    }

    public void onPreviousLevel()
    {
        CreateLevelManager.Instance.previousLevel();
        SceneManager.LoadScene(0);
        EventPool.Trigger("changeLevel");
    }

    public void updateTargetText()
    {
        targetText.text = CreateLevelManager.Instance.currentRequiredString();
    }

    public void finishLevel()
    {
        gameFinishedView.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {

        EventPool.OptIn("changeLevel", updateTargetText);
        EventPool.OptIn("finishLevel", finishLevel);
        updateTargetText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

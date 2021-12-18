using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevelManager : Singleton<CreateLevelManager>
{

    int currentLevel = 0;
    string[] levels = new string[]
    {
        "一",
        "二",
        "十",
        "三",
        "山",
        "日",
        "八",
        "人",
        "什",
        "夫",
    };

    public string currentRequiredString()
    {
        return levels[currentLevel];
    }
    public void nextLevel()
    {
        currentLevel++;
        if(currentLevel>= levels.Length)
        {
            currentLevel = 0;
        }
    }

    public void previousLevel()
    {
        currentLevel--;
        if (currentLevel < 0)
        {
            currentLevel = levels.Length-1;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.parent.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

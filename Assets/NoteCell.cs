using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteCell : MonoBehaviour
{

    public Letter resultImage;
    public Letter machineImage;
    public Letter input0Image;
    public Letter input1Image;
    public GameObject unlockedContent;
    public GameObject lockedContent;
    RuleInfo ruleInfo;

    Dictionary<string, string> machineName = new Dictionary<string, string>()
    {
        {"mergeV","竖合成" },
        {"mergeH","横合成" },
        {"mergeC","中央合成" },
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void init(RuleInfo info)
    {
        ruleInfo = info;


        resultImage.init( info.name, Vector3.one, false);
        if (machineName.ContainsKey(info.machine))
        {
            machineImage.GetComponentInChildren<Text>().text = machineName[info.machine];
        }
        else
        {
            machineImage.GetComponentInChildren<Text>().text = "还没实现";
        }
        input0Image.init(info.input0, Vector3.one, false);
        input1Image.init(info.input1, Vector3.one, false);
        updateLock();

    }

    public void updateLock()
    {
        bool _unlock = RuleManager.Instance.isUnlocked(ruleInfo);
        if (_unlock)
        {
            unlockedContent.SetActive(true);
            lockedContent.SetActive(false);
        }
        else
        {
            unlockedContent.SetActive(false);
            lockedContent.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

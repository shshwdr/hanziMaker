using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBookController : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {

        foreach(var row in RuleManager.Instance.ruleInfoList)
        {
            if(row.machine!="" && row.input0 != "")
            {
                var prefab = Resources.Load<GameObject>("noteCell");
                var go = Instantiate(prefab, transform);

                go.GetComponent<NoteCell>().init(row);
            }
        }


    }

    public void updateBook()
    {
        foreach(var child in GetComponentsInChildren<NoteCell>())
        {
            child.updateLock();
        }
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            RuleManager.Instance. forceUnlock = !RuleManager.Instance.forceUnlock;
            updateBook();
        }
    }
}

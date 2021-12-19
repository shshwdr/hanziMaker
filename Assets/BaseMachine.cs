using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMachine : MonoBehaviour
{
    public GeneralMachine generalMachine;


    public string outputStr;
    public void createLetters()
    {
        foreach (var attach in generalMachine.output.allAttaches)
        {
            var prefab = Resources.Load<GameObject>("letter");
            var go = Instantiate(prefab, generalMachine.output.transform.position, Quaternion.identity);
            go.GetComponent<Letter>().init(outputStr, attach.transform.position);
            go.transform.parent = attach.currentLine.transform;
        }
    }
    public virtual void work()
    {

    }
    public virtual string outputString()
    {
        Debug.LogError("no function implemented for outputString for " + gameObject.name);
        return "";
    }
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        generalMachine = GetComponent<GeneralMachine>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

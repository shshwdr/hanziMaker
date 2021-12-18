using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMachine : MonoBehaviour
{
    public GeneralMachine generalMachine;
    public virtual void work()
    {

    }
    public virtual string outputString()
    {
        Debug.LogError("no function implemented for outputString for " + gameObject.name);
        return "";
    }
    // Start is called before the first frame update
    void Awake()
    {
        generalMachine = GetComponent<GeneralMachine>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

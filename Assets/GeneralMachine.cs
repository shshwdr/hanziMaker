using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMachine : MonoBehaviour
{
    public float produceTime = 0.3f;
    float currentProduceTime = 0;

    public inoutPut output;
    public List<inoutPut> inputs = new List<inoutPut>();
    void getAllInputs()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(var inout in GetComponentsInChildren<inoutPut>())
        {
            if (inout.isInput)
            {
                inputs.Add(inout);
            }
            else
            {
                if (output)
                {
                    Debug.LogError("does not support multiple outputs yet");
                }
                else
                {
                    output = inout;
                }
            }
        }
    }



    public void work()
    {
        // Debug.Log()
        GetComponent<BaseMachine>().work();
    }

    // Update is called once per frame
    void Update()
    {
        currentProduceTime += Time.deltaTime;
        if (currentProduceTime > produceTime)
        {
            currentProduceTime = 0;
            work();
        }
    }
}

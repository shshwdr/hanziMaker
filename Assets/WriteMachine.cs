using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteMachine : BaseMachine
{
    public override void work()
    {
        base.work();

        var input = generalMachine.inputs[0];

        var inputString = input.getString();
        if(inputString == CreateLevelManager.Instance.currentRequiredString())
        {

            EventPool.Trigger("finishLevel");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

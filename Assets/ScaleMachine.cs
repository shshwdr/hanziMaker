using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMachine : BaseMachine
{
    public char modifyType;
    public override string outputString()
    {
        return outputStr;
    }

    public override void work()
    {
        base.work();
        if(generalMachine.inputs.Count != 1)
        {
            Debug.LogError("modify machine can only have one input "+ modifyType);
        }
        var input = generalMachine.inputs[0];

            var inputString = input.getString();

        if (generalMachine.output.allAttaches.Count>0 && inputString!="")
        {

            outputStr = RuleManager.stringAddModifier(inputString, modifyType);

            createLetters();

        }
    }


}

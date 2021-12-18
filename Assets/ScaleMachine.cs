using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMachine : BaseMachine
{
    public char modifyType;
    string outputStr;
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

        if (generalMachine.output.attachedPut && inputString!="")
        {

            outputStr = RuleManager.stringAddModifier(inputString, modifyType);

            var prefab = Resources.Load<GameObject>("letter");
            var go = Instantiate(prefab, generalMachine.output.transform.position, Quaternion.identity);
            go.GetComponent<Letter>().init(outputStr, generalMachine.output.attachedPut.transform.position);


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

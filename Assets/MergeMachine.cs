using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeMachine : BaseMachine
{

    public string mergeType;

    string outputStr;
    public override string outputString()
    {
        return outputStr;
    }

    public override void work()
    {
        base.work();

        if (!RuleManager.Instance.ruleInfoByMachine.ContainsKey(mergeType))
        {
            Debug.LogError("no merge machine existed " + mergeType);
        }

        foreach (var input in generalMachine.inputs)
        {
            if (!input.attachedPut)
            {
                return;
            }
        }

        //improve - don't calculate it everytime
        if (generalMachine.output.attachedPut)
        {
            bool foundOne = false;
            foreach(var row in RuleManager.Instance.ruleInfoByMachine[mergeType])
            {
                bool canCreate = true;
                for(int i = 0; i < row.inputs.Count;i++)
                {

                    //todo, make sure no extra letter in either side
                    if (i<generalMachine.inputs.Count &&  row.inputs[i] != generalMachine.inputs[i].getString())
                    {
                        canCreate = false;
                        break;
                    }
                }
                if (canCreate)
                {
                    if (foundOne)
                    {
                        Debug.LogError("found multiple for " + gameObject.name);
                    }
                    foundOne = true;
                    outputStr = row.name;
                    var prefab = Resources.Load<GameObject>("letter");
                    var go = Instantiate(prefab, generalMachine.output.transform.position, Quaternion.identity);
                    go.GetComponent<Letter>().init(row.displayName, generalMachine.output.attachedPut.transform.position);

                    //todo - don't break for debug
                    //break;
                }
            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

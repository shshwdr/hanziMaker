using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeMachine : BaseMachine
{

    public string mergeType;
    public bool ignoreOrder = false;
    public override string outputString()
    {
        return outputStr;
    }


    public bool canCreateResult(List<string> currentInputs, bool shouldCreate, bool _ignoreOrder, char ignoreModify = '\0', string _mergeType = "")
    {
        bool foundOne = false;

        string useMergeType = mergeType;
        if (_mergeType != "")
        {
            useMergeType = _mergeType;
        }

        foreach (var row in RuleManager.Instance.ruleInfoByMachine[useMergeType])
        {
            bool canCreate = true;

            List<string> ruleInputs = new List<string>();
            foreach (string ruleInput in row.inputs)
            {
                if (ruleInput != "")
                {
                    ruleInputs.Add(ruleInput);
                }
            }

            if (ruleInputs.Count != currentInputs.Count)
            {

                continue;
            }

            if (_ignoreOrder)
            {

                ruleInputs.Sort();
                currentInputs.Sort();

            }

            {
                for (int i = 0; i < row.inputs.Count; i++)
                {
                    if (ignoreModify != '\0')
                    {
                        if (!RuleManager.checkStringTheSame(ruleInputs[i], currentInputs[i], ignoreModify))
                        {
                            canCreate = false;
                            break;
                        }
                    }
                    else
                    {

                        if (i < ruleInputs.Count && ruleInputs[i] != currentInputs[i])
                        {
                            canCreate = false;
                            break;
                        }
                    }
                }
            }

            if (canCreate)
            {
                if (shouldCreate)
                {

                    if (foundOne)
                    {
                        Debug.LogError("found multiple for " + gameObject.name);
                    }
                    foundOne = true;
                    outputStr = row.name;
                    createLetters();
                }
                else
                {
                    return true;
                }

                //todo - don't break for debug
                //break;
            }
        }
        return foundOne;
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

        List<string> currentInputs = new List<string>();
        foreach (var machineInput in generalMachine.inputs)
        {
            var str = machineInput.getString();
            if (str != "")
            {

                currentInputs.Add(str);
            }
        }

        //improve - don't calculate it everytime
        if (generalMachine.output.allAttaches.Count>0)
        {
            bool foundOne = canCreateResult(currentInputs, true, ignoreOrder);

            if (!foundOne)
            {
                CreateErrorMessage(currentInputs);
            }

        }
    }

    void CreateErrorMessage(List<string> currentInputs)
    {
        generalMachine.errorPanel.SetActive(true);
        string errorText = "合并失败！";
        if (!ignoreOrder && canCreateResult(currentInputs, false, true))
        {
            errorText += "改变一下输入顺序试试。";
        }
        else if (canCreateResult(currentInputs, false, true, 's'))
        {
            errorText += "改变一下输入文字的大小试试";
        }
        else
        {
            bool canTryDifferentType = false;
            foreach (var type in RuleManager.otherMergeTypes(mergeType))
            {
                canTryDifferentType = canCreateResult(currentInputs, false, true, '\0', type);
                if (canTryDifferentType)
                {
                    break;
                }
            }
            if (!canTryDifferentType)
            {

                if (canCreateResult(currentInputs, false, true, 'r'))
                {
                    errorText += "旋转一下输入文字试试";
                }
                else if (canCreateResult(currentInputs, false, true, 'm'))
                {
                    errorText += "翻转一下输入文字试试";
                }
            }
        }
        generalMachine.errorText.text = errorText;
    }

}

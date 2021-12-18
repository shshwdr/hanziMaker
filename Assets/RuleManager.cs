using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleInfo
{
    public string name;
    public string displayName;
    public string machine;
    public string input0;
    public string input1;
    public string input2;
    public string input3;

    public List<string> inputs = new List<string>();
}

public class RuleManager : Singleton<RuleManager>
{

    List<RuleInfo> ruleInfoList;
    public Dictionary<string, List<RuleInfo>> ruleInfoByMachine = new Dictionary<string, List<RuleInfo>>();
    // Start is called before the first frame update
    void Start()
    {

        ruleInfoList = CsvUtil.LoadObjects<RuleInfo>("Rule");
        foreach (var info in ruleInfoList)
        {
            if (!ruleInfoByMachine.ContainsKey(info.machine))
            {
                ruleInfoByMachine[info.machine] = new List<RuleInfo>();
            }
            ruleInfoByMachine[info.machine].Add( info);
            if (info.input0!=null && info.input0 != "")
            {
                info.inputs.Add(info.input0);
            }
            if (info.input1 != null && info.input1 != "")
            {
                info.inputs.Add(info.input1);
            }
            if (info.input2 != null && info.input2!= "")
            {
                info.inputs.Add(info.input2);
            }
            if (info.input3 != null && info.input3 != "")
            {
                info.inputs.Add(info.input3);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

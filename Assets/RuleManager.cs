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
    public Dictionary<string, RuleInfo> ruleInfoByName = new Dictionary<string, RuleInfo>();
   static string[] mergeTypes = new string[]
    {
        "mergeC",
        "mergeV",
        "mergeH",

    };

    static public string[] otherMergeTypes(string currentMergeType)
    {
        List<string> res = new List<string>();
        foreach(var type in mergeTypes)
        {
            if(type != currentMergeType)
            {
                res.Add(type);
            }
        }
        return res.ToArray();
    }

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
            ruleInfoByName[info.name] = info;
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

    static public Dictionary<char, char> stringToDictionary(string input, char ignoreChar = '\0')
    {
        Dictionary<char, char> res = new Dictionary<char, char>();
        foreach (var letter in input)
        {
            //scale, mirror, rotate
            if(letter!='s' && letter != 'm'&& letter != 'r')
            {
                res['f'] = letter;
            }
            else
            {
                if(ignoreChar == letter)
                {
                    continue;
                }
                if (res.ContainsKey(letter))
                {
                    res[letter] = (char)((int)res[letter] + 1);
                }
                else
                {
                    res[letter] = (char)1;
                }
            }
        }
        return res;
    }

    static string addChar(Dictionary<char,char> dict, char c, char ignoreChar = '\0')
    {

        string res = "";
        if (dict.ContainsKey(c) && ignoreChar!=c)
        {
            for (int i = 0; i < (int)dict[c]; i++)
            {
                res += c;
            }
        }
        return res;
    }

    static public string dictionaryToString(Dictionary<char,char> dict, char ignoreChar = '\0' )
    {
        string res = "";
        res += addChar(dict, 'm', ignoreChar);
        res += addChar(dict, 'r', ignoreChar);
        res += addChar(dict, 's', ignoreChar);
        res += dict['f'];
        return res;
    }

    static public bool checkStringTheSame(string str1, string str2, char ignoreChar)
    {

        var dict1 = stringToDictionary(str1, ignoreChar);
        var dict2 = stringToDictionary(str2, ignoreChar);
        var newStr1 = dictionaryToString(dict1);
        var newStr2 = dictionaryToString(dict2);
        return newStr1 == newStr2;
    }

    static public string stringAddModifier(string str, char modifier)
    {
        var dict = stringToDictionary(str);
        //todo purify string dict
        var originValue = 0;
        if (dict.ContainsKey(modifier))
        {
            originValue = (int)dict[modifier];
        }
        dict[modifier] = (char)(originValue + 1);
        var newString = dictionaryToString(dict);
        return newString;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

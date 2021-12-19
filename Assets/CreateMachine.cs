using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMachine : BaseMachine
{
    public string createLetter;
    protected override void Awake()
    {
        base.Awake();
        outputStr = createLetter;
    }
    public override void work()
    {
        base.work();

            createLetters();

    }

    public override string outputString()
    {
        return createLetter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

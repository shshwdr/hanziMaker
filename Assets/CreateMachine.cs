using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMachine : BaseMachine
{
    public string createLetter;
    public override void work()
    {
        base.work();

        if (generalMachine.output.attachedPut)
        {

            var prefab = Resources.Load<GameObject>("letter");
            var go = Instantiate(prefab, generalMachine.output.transform.position, Quaternion.identity);
            go.GetComponent<Letter>().init(createLetter, generalMachine.output.attachedPut.transform.position);
        }

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

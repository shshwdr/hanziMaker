using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMachine : BaseMachine
{
    public string createLetter;
    GeneralMachine generalMachine;
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
    // Start is called before the first frame update
    void Start()
    {
        generalMachine = GetComponent<GeneralMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

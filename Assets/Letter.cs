using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    string letter;
    public Text text;
    Vector3 targetPosition;
    public float moveSpeed = 1f;
    public void init(string l,Vector3 position)
    {

        var dict = RuleManager.stringToDictionary(l);
        
        letter = ""+dict['f'];
        if (dict.ContainsKey('s'))
        {
            text.transform.localScale = Vector3.one * 0.5f;
        }
        if (dict.ContainsKey('r'))
        {
            Quaternion rotationAmount = Quaternion.Euler(0, 0, 90);
            text. transform.rotation = transform.rotation * rotationAmount;
        }

        if (dict.ContainsKey('m'))
        {
            var scale = text.transform.localScale;
            text.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }


        text.text = letter;
        targetPosition = position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dir = (targetPosition - transform.position).normalized;
        transform.Translate(dir * moveSpeed * Time.deltaTime);

        if((transform.position - targetPosition).magnitude <= 0.1f)
        {
            Destroy(gameObject);
        }
    }
}

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
        letter = l;
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

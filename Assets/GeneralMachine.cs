using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GeneralMachine : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float produceTime = 0.3f;
    float currentProduceTime = 0;

    public GameObject errorPanel;
    public Text errorText;

    public inoutPut output;
    public List<inoutPut> inputs = new List<inoutPut>();
    void getAllInputs()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        errorPanel.SetActive(false);
        foreach(var inout in GetComponentsInChildren<inoutPut>())
        {
            if (inout.isInput)
            {
                inputs.Add(inout);
            }
            else
            {
                if (output)
                {
                    Debug.LogError("does not support multiple outputs yet");
                }
                else
                {
                    output = inout;
                }
            }
        }
    }



    public void work()
    {
        // Debug.Log()
        GetComponent<BaseMachine>().work();
    }

    // Update is called once per frame
    void Update()
    {
        currentProduceTime += Time.deltaTime;
        if (currentProduceTime > produceTime)
        {
            currentProduceTime = 0;
            work();
        }
    }



    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
       // currentDragging = null;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Utils.getMousePosition;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
       // currentDragging = Instantiate(preload, Utils.getMousePosition, Quaternion.identity);
    }
}

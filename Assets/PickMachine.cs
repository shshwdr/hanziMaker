using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickMachine : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler

{
    public string pickMachineString;
    GameObject preload;

    GameObject currentDragging;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        preload = Resources.Load<GameObject>("machine/" + pickMachineString);
        var go = Instantiate(preload, transform.position, Quaternion.identity);

        go.transform.parent = transform;
        go.transform.localScale = Vector3.one;
        Destroy(go.GetComponent<GeneralMachine>().errorPanel);
        go.GetComponent<GeneralMachine>().enabled = false;
        go.GetComponent<BoxCollider2D>().enabled = false;

        GetComponent<BoxCollider2D>().offset = go.GetComponent<BoxCollider2D>().offset;
        GetComponent<BoxCollider2D>().size = go.GetComponent<BoxCollider2D>().size;
        foreach (var inout in go.GetComponentsInChildren<inoutPut>())
        {
            Destroy(inout.gameObject);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        currentDragging = null;
    }
    public void OnDrag(PointerEventData eventData)
    {
        currentDragging.transform.position = Utils.getMousePosition;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        currentDragging = Instantiate(preload, Utils.getMousePosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickMachine : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler

{
    public string pickMachineString;
    GameObject preload;
    GameObject removeAreaObject;
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
        LayerMask mask = LayerMask.GetMask("removeArea");
        Collider2D col = Physics2D.OverlapCircle(Utils.getMousePosition, 0.1f, mask);
        if (col)
        {
            Destroy(currentDragging);
        }
        if (removeAreaObject)
        {

            removeAreaObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        currentDragging = null;
    }
    public void OnDrag(PointerEventData eventData)
    {
        currentDragging.transform.position = Utils.getMousePosition;
        LayerMask mask = LayerMask.GetMask("removeArea");
        Collider2D col = Physics2D.OverlapCircle(Utils.getMousePosition, 0.1f, mask);
        if (col)
        {
            removeAreaObject = col.gameObject;
            col.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            if (removeAreaObject)
            {

                removeAreaObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
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

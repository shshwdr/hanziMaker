using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class inoutPut : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler

{
    LineRenderer currentLine;
    inoutPut currentLineAttach;

    inoutPut currentAttach;

    public bool isInput;



    public Material lineMaterial;
    public float collideRadius = 0.2f;
    public SpriteRenderer renderer;

    public inoutPut attachedPut
    {
        get
        {
            return currentAttach;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        var mousePosition = getMousePosition;
        DrawLine(mousePosition, mousePosition, Color.red);
    }
    Vector3 getMousePosition { get
        {
            var mouseP = Input.mousePosition;
            mouseP.z = 5.0f;
            var worldMouseP = Camera.main.ScreenToWorldPoint(mouseP);
            Debug.Log("mouse position " + mouseP + " "+ worldMouseP);
            //return Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return worldMouseP;
            //return 
        }
    }
    
    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        currentLine = lr;
        // lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.material = lineMaterial;



        lr.startColor = Color.red;
        lr.endColor = Color.red;


        //GameObject.Destroy(myLine, duration);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        if (currentLineAttach)
        {
            attach(currentLineAttach);
            currentLineAttach.attach(this);
            currentLineAttach.releaseTryAttach();
        }
        else
        {
            Destroy(currentLine.gameObject);
        }


        currentLine = null;
    }
    public void OnDrag(PointerEventData eventData)
    {
        var mousePosition = getMousePosition;

        if (currentLineAttach)
        {
            if((currentLineAttach.transform.position - mousePosition).magnitude >= collideRadius)
            {
                currentLineAttach.releaseTryAttach();
                currentLineAttach = null;
            }
        }

        foreach( var item in Physics2D.OverlapCircleAll(mousePosition, collideRadius))
        {
            var newInOut = item.GetComponent<inoutPut>();
            if (newInOut && newInOut!=this)
            {
                if(currentLineAttach && newInOut == currentLineAttach)
                {
                    currentLineAttach.releaseTryAttach();
                }
                else
                {

                }

                newInOut.selectToTryAttach();
                currentLineAttach = newInOut;
                break;
            }
        }

        currentLine.SetPosition(1, mousePosition);
    }

    public void selectToTryAttach()
    {
        renderer.transform.localScale = Vector3.one * 1.5f;
    }

    public void releaseTryAttach()
    {

        renderer.transform.localScale = Vector3.one;
    }

    public void attach(inoutPut inout)
    {
        currentAttach = inout;
    }

    public void releaseAttach()
    {
        currentAttach = null;
    }


}

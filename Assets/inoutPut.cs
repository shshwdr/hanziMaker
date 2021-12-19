using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class inoutPut : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler

{
    //List<LineRenderer> currentLines;
    List<inoutPut> currentAttachss = new List<inoutPut>();
    Dictionary<inoutPut, LineRenderer> currentLinesByAttach = new Dictionary<inoutPut, LineRenderer>();

    LineRenderer currentDraggingLine;
    inoutPut currentDraggingAttach;


    public bool isInput;



    public Material lineMaterial;
    public float collideRadius = 0.2f;
    public SpriteRenderer renderer;

    public inoutPut attachedPut
    {
        get
        {
            if(currentAttachss.Count == 0)
            {
                return null;
            }
            return currentAttachss[0];
        }
    }

    public List<inoutPut> allAttaches { get { return currentAttachss; } }

    public LineRenderer currentLine { get {
            if (!isInput)
            {
                Debug.LogError("hmm should not use this function if is not input");
            }
            if (!currentLinesByAttach.ContainsKey(attachedPut))
            {
                Debug.LogError("attach not existed for line");
            }
            return currentLinesByAttach[attachedPut]; }
    }

    public string getString()
    {
        if (!isInput)
        {
            Debug.LogError("should not ask input's string");
            return "";
        }
        if (!attachedPut)
        {
            return "";
        }
        return attachedPut.GetComponentInParent<BaseMachine>().outputString();
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        // if is input, remove current line and attach, reuse that line
        // if is output, create new line

        if (isInput)
        {
            currentDraggingLine = currentLine;
            currentDraggingAttach = attachedPut;
            attachedPut.releaseAttach(this);
            releaseAttach(attachedPut);
        }
        else
        {


            var mousePosition = transform.position;
            DrawLine(mousePosition, mousePosition, Color.red);
        }

    }
    
    
    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.parent = transform;
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        currentDraggingLine = lr;
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

        if (currentDraggingAttach)
        {
            attach(currentDraggingAttach,currentDraggingLine);
            currentDraggingAttach.attach(this, currentDraggingLine);
            currentDraggingLine.SetPosition(1, currentDraggingAttach.transform.position);
        }
        else
        {
            Destroy(currentDraggingLine.gameObject);
        }
        if (currentDraggingAttach)
        {
            currentDraggingAttach.releaseTryAttach();
        }
        releaseTryAttach();
    }
    public void OnDrag(PointerEventData eventData)
    {
        var mousePosition = Utils.getMousePosition;

        if (currentDraggingAttach)
        {
            if((currentDraggingAttach.transform.position - mousePosition).magnitude >= collideRadius)
            {
                currentDraggingAttach.releaseTryAttach();
                releaseTryAttach();
            }
        }

        foreach( var item in Physics2D.OverlapCircleAll(mousePosition, collideRadius))
        {
            var newInOut = item.GetComponent<inoutPut>();
            //todo find the closest inoutput to attach
            if (newInOut && newInOut!=this && newInOut.isInput!=isInput)
            {
                //if(currentDraggingAttach && newInOut == currentLineAttach)
                //{
                //    currentLineAttach.releaseTryAttach();
                //}
                //else
                //{

                //}

                newInOut.selectToTryAttach();
                currentDraggingAttach = newInOut;
                break;
            }
        }

        currentDraggingLine.SetPosition(1, mousePosition);
    }

    public void selectToTryAttach()
    {
        renderer.transform.localScale = Vector3.one * 1.5f;
    }

    public void releaseTryAttach()
    {

        currentDraggingAttach = null;
        renderer.transform.localScale = Vector3.one;
    }

    public void attach(inoutPut inout, LineRenderer line)
    {
        if (isInput)
        {
            if(currentLinesByAttach.Count!=0 || currentAttachss.Count != 0)
            {
                Debug.LogError("input can only have one attache");
            }
        }

        if (currentAttachss.Count != currentLinesByAttach.Count)
        {

            Debug.LogError("attach and line not the same");
        }

        currentLinesByAttach[inout] = line;
        currentAttachss.Add  ( inout);
    }

    public void releaseAttach(inoutPut inout)
    {
        currentLinesByAttach.Remove(inout);
        currentAttachss.Remove(inout);
    }

    public void releaseAllAttach()
    {
        currentLinesByAttach.Clear();
        currentAttachss.Clear();
    }


    private void OnDestroy()
    {
        foreach (var attach in currentAttachss)
        {
            if (allAttaches.Count > 1)
            {

                attach.releaseAllAttach();
            }
            else
            {

                attach.releaseAttach(attachedPut);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class inoutPut : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler

{
    //List<LineRenderer> currentLines;
    List<inoutPut> currentAttachss = new List<inoutPut>();
    Dictionary<inoutPut, MachineLine> currentLinesByAttach = new Dictionary<inoutPut, MachineLine>();

    MachineLine currentDraggingLine;

    public bool isInput;

    public GeneralMachine machine;

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

    public MachineLine currentLine { get {
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
        Debug.Log("OnBeginDrag " + isInput) ;

        // if is input, remove current line and attach, reuse that line
        // if is output, create new line

        if (isInput && attachedPut)
        {
            currentLine.init(attachedPut);
            currentDraggingLine = currentLine; 
            attachedPut.releaseAttach(this); 
            releaseAttach(attachedPut);
        }
        else
        {

            var mousePosition = transform.position;
            DrawLine(mousePosition, mousePosition, Color.red);
        }


        //else if (isInput && attachedPut)
        //{
        //    attachedPut.currentDraggingLine = currentLine;
        //    attachedPut.currentDraggingAttach = attachedPut;

        //    passToOpposite = true;
        //    attachedPut.isPassed = true;
        //    //attachedPut.releaseAttach(this);
        //    //releaseAttach(attachedPut);
        //    tempPassAttach = attachedPut;
        //    attachedPut.releaseAttach(this);
        //    releaseAttach(attachedPut);
        //    tempPassAttach.OnBeginDrag(eventData);
        //}
    }

    private void Awake()
    {
        machine = GetComponentInParent<GeneralMachine>();
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        //GameObject myLine = new GameObject();
        //myLine.transform.position = start;
        //myLine.AddComponent<LineRenderer>();
       // LineRenderer lr = myLine.GetComponent<LineRenderer>();
        // lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        //lr.SetWidth(0.1f, 0.1f);
       // lr.material = lineMaterial;



        var linePrefab = Resources.Load<GameObject>("line");
        var line = Instantiate(linePrefab,start,Quaternion.identity);
        currentDraggingLine = line.GetComponent<MachineLine>();
        currentDraggingLine.init(this);


        line.transform.parent = transform.parent;

        //GameObject.Destroy(myLine, duration);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentDraggingLine)
        {
            currentDraggingLine.OnEndDrag();
            currentDraggingLine = null;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (currentDraggingLine)
        {
            currentDraggingLine.OnDrag();
        }
    }


    public void attach(inoutPut inout, MachineLine line)
    {
        if (isInput)
        {
            if(currentLinesByAttach.Count!=0 || currentAttachss.Count != 0)
            {
                Destroy( currentLinesByAttach[attachedPut].gameObject);
                attachedPut.releaseAttach(this);
                releaseAllAttach();
                //Debug.LogError("input can only have one attache");
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

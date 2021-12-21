using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineLine : MonoBehaviour
{
    inoutPut input;
    inoutPut output;

    inoutPut currentStartPut;
    inoutPut currentDraggingAttach;
    public float collideRadius = 0.2f;

    Vector3 start;
    Vector3 end;

    LineRenderer lr;

    public void init(inoutPut _put)
    {

        if (input)
        {

            input.machine.removeMachineLine(this);
        }
        if (output)
        {

            output.machine.removeMachineLine(this);
        }

        lr = GetComponent<LineRenderer>();


        if(_put.isInput)
        {
            input = _put;
        }
        else
        {
            output = _put;
        }
        currentStartPut = _put;
        currentDraggingAttach = null;

        Utils.destroyAllChildren(transform);

        lr.SetPosition(0, currentStartPut.transform.position);
        var mousePosition = Utils.getMousePosition;
        lr.SetPosition(1, mousePosition);
    }

    // Start is called before the first frame update
   

    public void updatePosition()
    {

        Utils.destroyAllChildren(transform);

        lr.SetPosition(0, input.transform.position);
        lr.SetPosition(1, output.transform.position);
    }


    public void OnEndDrag()
    {

        Debug.Log("OnEndDrag line");

        if (currentDraggingAttach)
        {
            currentStartPut.attach(currentDraggingAttach, this);
            currentDraggingAttach.attach(currentStartPut, this);
            lr.SetPosition(1, currentDraggingAttach.transform.position);
            if (currentDraggingAttach.isInput)
            {
                input = currentDraggingAttach;
            }
            else
            {
                output = currentDraggingAttach;
            }

            input.machine.addMachineLine(this);
            output.machine.addMachineLine(this);
            releaseTryAttach();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void OnDrag()
    {
        var mousePosition = Utils.getMousePosition;

        if (currentDraggingAttach)
        {
            if ((currentDraggingAttach.transform.position - mousePosition).magnitude >= collideRadius)
            {
                releaseTryAttach();
            }
        }

        foreach (var item in Physics2D.OverlapCircleAll(mousePosition, collideRadius))
        {
            var newInOut = item.GetComponent<inoutPut>();
            //todo find the closest inoutput to attach
            if (newInOut && newInOut.machine != currentStartPut.machine && newInOut.isInput != currentStartPut.isInput)
            {
                selectToTryAttach(newInOut);
                break;
            }
        }

        lr.SetPosition(1, mousePosition);
    }


    public void selectToTryAttach(inoutPut newInOut)
    {
        currentDraggingAttach = newInOut;
        currentDraggingAttach.renderer.transform.localScale = Vector3.one * 1.5f;
    }

    public void releaseTryAttach()
    {
        currentDraggingAttach.renderer.transform.localScale = Vector3.one;
        currentDraggingAttach = null;
    }

    private void OnDestroy()
    {
        if (input)
        {
            input.machine.removeMachineLine(this);
        }
        if (output)
        {

            output.machine.removeMachineLine(this);
        }
    }
}

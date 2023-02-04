using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonPressOnNode : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{

    bool isInside = true;
    bool isPressed = false;
    // Start is called before the first frame update
    void Update()
    {
        if (isPressed && !isInside)
        {
            RootManager.Instance.DrawPreview();
        }
    }


    public void OnPointerDown(PointerEventData pointerEventData)
    {
        RootManager.Instance.selectedNode = GetComponentInParent<Node>();
        isPressed = true;
        //Output the name of the GameObject that is being clicked
        //Debug.Log(name + "Game Object Click in Progress");
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        isPressed = false;
        //Debug.Log(name + "No longer being clicked");
        if (isInside)
            GetComponentInParent<Node>().DisplayUI();
        else
            RootManager.Instance.SpawnRoot();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isInside = false;
        //Debug.Log("The cursor exited the selectable UI element.");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isInside = true;
        RootManager.Instance.ClearPreview();
        //Debug.Log("The cursor exited the selectable UI element.");
    }
}

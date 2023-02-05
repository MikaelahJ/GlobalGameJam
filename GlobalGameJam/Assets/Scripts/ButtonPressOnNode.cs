using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonPressOnNode : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{

    bool isInside = true;
    bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    void Update()
    {
        if (isPressed && !isInside)
        {
            RootManager.Instance.DrawPreview();
        }
    }


    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Node previousNode = RootManager.Instance.selectedNode;
        Node currentNode = GetComponentInParent<Node>();

        if(previousNode != null && previousNode != currentNode)
        {
            previousNode.DisplayUI(false);
        }

        RootManager.Instance.selectedNode = currentNode;
        isPressed = true;

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

        if (isPressed)
        {
            GetComponentInParent<Node>().DisplayUI(false);
        }
        //Debug.Log("The cursor exited the selectable UI element.");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isInside = true;
        RootManager.Instance.ClearPreview();
        //Debug.Log("The cursor exited the selectable UI element.");
    }
}

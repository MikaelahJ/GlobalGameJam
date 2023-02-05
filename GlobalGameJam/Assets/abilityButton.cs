using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class abilityButton : MonoBehaviour
{
    public Abilities ability;
    private Node node;

    private Button button;
    
    // Start is called before the first frame update
    void Start()
    {
        node = transform.parent.GetComponentInParent<Node>();
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { OnbuttonClick(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnbuttonClick()
    {
        string response = node.AddAbility(ability);
        GetComponentInChildren<TextMeshProUGUI>().text = response;
    }
}

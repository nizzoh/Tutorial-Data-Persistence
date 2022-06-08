using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var input = gameObject.GetComponent<InputField>();
        var submitEvent = new InputField.SubmitEvent();
        submitEvent.AddListener(SubmitName);
        input.onEndEdit = submitEvent;

        input.text = NameManagerSingelton.Instance.playerName;
    }

    private void SubmitName(string arg0)
    {        
        NameManagerSingelton.Instance.playerName = arg0;
        Debug.Log(arg0);
    }
}

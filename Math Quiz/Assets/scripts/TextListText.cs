using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextListText : MonoBehaviour
{
    [SerializeField]
    private Text myText;

    //[SerializeField]
    //private ButtonListControl buttoncontrol;

    private string myTextstring;

    public void SetText(string textString)
    {
        myTextstring = textString;
        myText.text = textString;
    }

    public void OnClick()
    {
        //buttoncontrol.ButtonClicked(myTextstring);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject textTemplete;

    [SerializeField]
    private int[] intArray;

    private List<GameObject> buttons;

    void Start()
    {
        Debug.Log("TextList Start");

        //buttons = new List<GameObject>();

        //if (buttons.Count > 0)
        //{
        //    foreach (GameObject button in buttons)
        //    {
        //        Destroy(button.gameObject);
        //    }

        //    buttons.Clear();
        //}

        //foreach (int i in intArray)
        //{
        //    GameObject button = Instantiate(buttonTemplete) as GameObject;
        //    button.SetActive(true);

        //    button.GetComponent<ButtonListButton>().SetText("Button #" + i);

        //    button.transform.SetParent(buttonTemplete.transform.parent, false);
        //}

        //for (int i = 1; i <= 30; i++)
        //{
        //    GameObject text = Instantiate(textTemplete) as GameObject;
        //    text.SetActive(true);

        //    text.GetComponent<TextListText>().SetText("Text #" + i);

        //    text.transform.SetParent(textTemplete.transform.parent, false);
        //}

    }

    public void AddTextList()
    {
        Debug.Log("TextList add");

        GameObject text = Instantiate(textTemplete) as GameObject;
        text.SetActive(true);

        text.GetComponent<TextListText>().SetText("Text #" + 1);

        text.transform.SetParent(textTemplete.transform.parent, false);
    }

    public void AddTextList(string myTextString)
    {
        //Debug.Log("TextList add");

        GameObject text = Instantiate(textTemplete) as GameObject;
        text.SetActive(true);

        text.GetComponent<TextListText>().SetText(myTextString);

        text.transform.SetParent(textTemplete.transform.parent, false);
    }

    public void Generate()
    {
        Debug.Log("TextList Generate");

        for (int i = 1; i <= 30; i++)
        {
            GameObject text = Instantiate(textTemplete) as GameObject;
            text.SetActive(true);

            text.GetComponent<TextListText>().SetText("Text #" + i);

            text.transform.SetParent(textTemplete.transform.parent, false);
        }
    }


    public void ButtonClicked(string myTextString)
    {
        Debug.Log(myTextString);
    }


}

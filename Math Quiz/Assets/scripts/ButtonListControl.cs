using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplete;

    [SerializeField]
    private GameObject textTemplete;

    [SerializeField]
    private int[] intArray;

    private List<GameObject> buttons;

    void Start()
    //void GenerateList()
    {
        Debug.Log("ButtonList Start");


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

        for (int i = 1; i <= 30; i++)
        {
            //GameObject button = Instantiate(buttonTemplete) as GameObject;
            //button.SetActive(true);

            //button.GetComponent<ButtonListButton>().SetText("Button #" + i);

            //button.transform.SetParent(buttonTemplete.transform.parent, false);


            GameObject text = Instantiate(textTemplete) as GameObject;
            text.SetActive(true);

            text.GetComponent<ButtonListButton>().SetText("Text #" + i);

            text.transform.SetParent(textTemplete.transform.parent, false);
        }

    }

    public void ButtonClicked(string myTextString)
    {
        Debug.Log(myTextString);
    }


}

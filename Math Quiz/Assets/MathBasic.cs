using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class MathBasic : MonoBehaviour
{
    public Sprite sprite1;
    public SpriteRenderer Level_Image;
    private Image KanjiImg = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Math basic start!");

        Level_Image = GameObject.Find("KanjiImage").GetComponent<SpriteRenderer>();
        //KanjiImg = GameObject.Find("NextImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PreviousButtonClicked()
    {
        Debug.Log("PreviousButtonClicked");
    }

    public void NextButtonClicked()
    {
        Debug.Log("NextButtonClicked");

        //KanjiImg.sprite = sprite1;
        Level_Image.sprite = sprite1;

    }

}

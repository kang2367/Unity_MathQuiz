using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

using System.IO;

public class GameManager : MonoBehaviour
{
    public GameObject questionText;
    public GameObject example1Text;
    public GameObject example2Text;
    public GameObject example3Text;
    public GameObject example4Text;

    Dictionary<string, string>[] problems = {
        new Dictionary<string,string>(){
            {"question", "1 + 2 = ?"},
            {"answer", "3"},
            {"example1", "1"},
            {"example2", "3"},
            {"example3", "2"},
            {"example4", "4"}},
        new Dictionary<string,string>(){
            {"question", "3 + 2 = ?"},
            {"answer", "5"},
            {"example1", "4"},
            {"example2", "6"},
            {"example3", "5"},
            {"example4", "2"}},
        new Dictionary<string,string>(){
            {"question", "3 + 3 = ?"},
            {"answer", "6"},
            {"example1", "6"},
            {"example2", "1"},
            {"example3", "5"},
            {"example4", "4"}},
        new Dictionary<string,string>(){
            {"question", "0 + 3 = ?"},
            {"answer", "3"},
            {"example1", "1"},
            {"example2", "2"},
            {"example3", "4"},
            {"example4", "3"}},
        new Dictionary<string,string>(){
            {"question", "4 + 2 = ?"},
            {"answer", "6"},
            {"example1", "6"},
            {"example2", "4"},
            {"example3", "2"},
            {"example4", "5"}},
        new Dictionary<string,string>(){
            {"question", "5 + 4 = ?"},
            {"answer", "9"},
            {"example1", "8"},
            {"example2", "6"},
            {"example3", "7"},
            {"example4", "9"}},
        new Dictionary<string,string>(){
            {"question", "4 + 4 = ?"},
            {"answer", "8"},
            {"example1", "7"},
            {"example2", "1"},
            {"example3", "8"},
            {"example4", "3"}},
        new Dictionary<string,string>(){
            {"question", "2 + 5 = ?"},
            {"answer", "7"},
            {"example1", "7"},
            {"example2", "1"},
            {"example3", "5"},
            {"example4", "4"}},
        new Dictionary<string,string>(){
            {"question", "1 + 4 = ?"},
            {"answer", "5"},
            {"example1", "4"},
            {"example2", "5"},
            {"example3", "0"},
            {"example4", "6"}},
        new Dictionary<string,string>(){
            {"question", "3 + 1 = ?"},
            {"answer", "4"},
            {"example1", "8"},
            {"example2", "3" },
            {"example3", "4"},
            {"example4", "0"}}
    };
    int problemNumber = 1;
    string question = "";
    string answer = "";
    string example1 = "";
    string example2 = "";
    string example3 = "";
    string example4 = "";

    int totalCorrect = 0;
    public GameObject totalCorrectText;
    public GameObject correctIncorrectText;

    public GameObject gameOverPanel;

    string m_strPath = "Assets/";

    // Start is called before the first frame update
    void Start()
    {
        ShowProblem();

        totalCorrectText.GetComponent<Text>().text = "Total Correct: 0";
        correctIncorrectText.GetComponent<Text>().text = "Correct/Incorrect";
    }

    public void SaveButtonClicked()
    {
        Debug.Log("SaveButtonClicked");
        WriteData("Test\r\n");
    }

    public void LoadButtonClicked()
    {
        Debug.Log("LoadButtonClicked");
        Parse();
    }

    public void WriteData(string strData)
    {
        FileStream f = new FileStream(m_strPath + "Data.txt", FileMode.Append, FileAccess.Write);

        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);

        writer.WriteLine(strData);

        writer.Close();
    }

    public void Parse()
    {
        TextAsset data = Resources.Load("Data", typeof(TextAsset)) as TextAsset;

        StringReader sr = new StringReader(data.text);

        // 먼저 한줄을 읽는다. 
        string source = sr.ReadLine();
        string[] values;                // 쉼표로 구분된 데이터들을 저장할 배열 (values[0]이면 첫번째 데이터 )

        while (source != null)
        {
            values = source.Split(',');  // 쉼표로 구분한다. 저장시에 쉼표로 구분하여 저장하였다.

            if (values.Length == 0)
            {
                sr.Close();
                return;
            }
            source = sr.ReadLine();    // 한줄 읽는다.

            Debug.Log(source);
        }
    }

    void ShowProblem()
    {
        question = problems[problemNumber - 1]["question"];
        answer = problems[problemNumber - 1]["answer"];
        example1 = problems[problemNumber - 1]["example1"];
        example2 = problems[problemNumber - 1]["example2"];
        example3 = problems[problemNumber - 1]["example3"];
        example4 = problems[problemNumber - 1]["example4"];
        questionText.GetComponent<Text>().text = question;
        example1Text.GetComponent<Text>().text = example1;
        example2Text.GetComponent<Text>().text = example2;
        example3Text.GetComponent<Text>().text = example3;
        example4Text.GetComponent<Text>().text = example4;
    }


    public void Example1ButtonClicked()
    {
        Debug.Log("Example1ButtonClicked");
        SelectExample(example1);

        //Debug.Log(example1);
        //problemNumber += 1;
        //ShowProblem();
    }

    void SelectExample(string example)
    {
        Debug.Log(example);

        if (answer.Equals(example))
        {
            totalCorrect += 1;
            totalCorrectText.GetComponent<Text>().text = totalCorrect.ToString();
            correctIncorrectText.GetComponent<Text>().text = "Correct";
        }
        else
        {
            correctIncorrectText.GetComponent<Text>().text = "Incorrect";
        }

        if (problemNumber < problems.Length)
        {
            problemNumber += 1;
            ShowProblem();
        }
        else
        {
            Debug.Log("ShowGameOverBox");
            ShowGameOverBox();
        }

        //problemNumber += 1;
        //ShowProblem();
    }

    void ShowGameOverBox()
    {
        gameOverPanel.SetActive(true);
    }

    public void Example2ButtonClicked()
    {
        Debug.Log("Example2ButtonClicked");
        SelectExample(example2);

        //Debug.Log(example2);
        //problemNumber += 1;
        //ShowProblem();
    }

    public void Example3ButtonClicked()
    {
        Debug.Log("Example3ButtonClicked");
        SelectExample(example3);

        //Debug.Log(example3);
        //problemNumber += 1;
        //ShowProblem();
    }

    public void Example4ButtonClicked()
    {
        Debug.Log("Example4ButtonClicked");
        SelectExample(example4);

        //Debug.Log(example4);
        //problemNumber += 1;
        //ShowProblem();
    }

    public void Replay()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

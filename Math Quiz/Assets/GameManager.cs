using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

using System.IO;
using System;
//using System.Linq;

public class GameManager : MonoBehaviour
{
    public GameObject questionText;
    public GameObject example1Text;
    public GameObject example2Text;
    public GameObject example3Text;
    public GameObject example4Text;

    //Dictionary<string, string>[] problems = {
    //    new Dictionary<string,string>(){
    //        {"question", "1 + 2 = ?"},
    //        {"answer", "3"},
    //        {"example1", "1"},
    //        {"example2", "3"},
    //        {"example3", "Clear"},
    //        {"example4", "Enter"}},
    //    new Dictionary<string,string>(){
    //        {"question", "3 + 2 = ?"},
    //        {"answer", "5"},
    //        {"example1", "4"},
    //        {"example2", "6"},
    //        {"example3", "5"},
    //        {"example4", "2"}},
    //    new Dictionary<string,string>(){
    //        {"question", "3 + 3 = ?"},
    //        {"answer", "6"},
    //        {"example1", "6"},
    //        {"example2", "1"},
    //        {"example3", "5"},
    //        {"example4", "4"}},
    //    new Dictionary<string,string>(){
    //        {"question", "0 + 3 = ?"},
    //        {"answer", "3"},
    //        {"example1", "1"},
    //        {"example2", "2"},
    //        {"example3", "4"},
    //        {"example4", "3"}},
    //    new Dictionary<string,string>(){
    //        {"question", "4 + 2 = ?"},
    //        {"answer", "6"},
    //        {"example1", "6"},
    //        {"example2", "4"},
    //        {"example3", "2"},
    //        {"example4", "5"}},
    //    new Dictionary<string,string>(){
    //        {"question", "5 + 4 = ?"},
    //        {"answer", "9"},
    //        {"example1", "8"},
    //        {"example2", "6"},
    //        {"example3", "7"},
    //        {"example4", "9"}},
    //    new Dictionary<string,string>(){
    //        {"question", "4 + 4 = ?"},
    //        {"answer", "8"},
    //        {"example1", "7"},
    //        {"example2", "1"},
    //        {"example3", "8"},
    //        {"example4", "3"}},
    //    new Dictionary<string,string>(){
    //        {"question", "2 + 5 = ?"},
    //        {"answer", "7"},
    //        {"example1", "7"},
    //        {"example2", "1"},
    //        {"example3", "5"},
    //        {"example4", "4"}},
    //    new Dictionary<string,string>(){
    //        {"question", "1 + 4 = ?"},
    //        {"answer", "5"},
    //        {"example1", "4"},
    //        {"example2", "5"},
    //        {"example3", "0"},
    //        {"example4", "6"}},
    //    new Dictionary<string,string>(){
    //        {"question", "3 + 1 = ?"},
    //        {"answer", "4"},
    //        {"example1", "8"},
    //        {"example2", "3" },
    //        {"example3", "4"},
    //        {"example4", "0"}}
    //};

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
    public GameObject statusText;

    public GameObject gameOverPanel;

    string m_strPath = "Assets/Resources/";

    private Text logText = null;
    private ScrollRect scroll_rect = null;
    private InputField inputAnswer = null;
    private Text textAnswer = null;
    private Text textQNumber = null;
    //private Text textResult = null;

    //int firstNumber =  UnityEngine.Random.Range(1, 10);
    //int secondNumber = UnityEngine.Random.Range(1, 10);
    int firstNumber = 0;
    int secondNumber = 0;
    int problemsLength = 10;

    private Toggle toggleUI = null;

    float timer;
    float waitingTime = 0.0f;

    string strResult = "";
    bool bResultDisplay = false;

    // Start is called before the first frame update
    void Start()
    {
        //ShowProblem();

        totalCorrectText.GetComponent<Text>().text = "Total Correct: 0";
        correctIncorrectText.GetComponent<Text>().text = "Correct/Incorrect";
        statusText.GetComponent<Text>().text = "V0.01"; // Version 

        logText = GameObject.Find("log_Text").GetComponent<Text>();
        scroll_rect = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        inputAnswer = GameObject.Find("InputField").GetComponent<InputField>();
        inputAnswer.enabled = false;
        textAnswer = GameObject.Find("TextAnswer").GetComponent<Text>();
        textQNumber = GameObject.Find("QNumberText").GetComponent<Text>();

        toggleUI = GameObject.Find("ToggleUI").GetComponent<Toggle>();

        //problemsLength = 30;    // 10;

        //textResult.text = "";

        timer = 0.0f;
        waitingTime = 2.0f;

        ShowProblem();

        if (logText != null)
        {
            //logText.text += "" + "\n";
            //logText.text += "Hello Log Window!" + "\n";
            //logText.text += "Hello Log Window again and again!" + "\n";
        }


    }

    private void Awake()
    {
        //value = Random.Range(-1, 1);

    }


    void Timer()
    {
        if(bResultDisplay == true)
        {
            timer += Time.deltaTime;

            if (timer > waitingTime)    // Debugging need (항상 실행되고 있음)
            {
                //Action
                timer = 0;

                if (strResult == "Correct")
                {
                    if (problemNumber < problemsLength)
                    {
                        problemNumber += 1;
                        ShowProblem();
                    }
                    else
                    {
                        Debug.Log("ShowGameOverBox");

                        // Save result log
                        string strDateTime = "";
                        strDateTime = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                        Debug.Log(strDateTime);

                        WriteData("Game end:" + strDateTime);

                        ShowGameOverBox();
                    }
                }
                else
                {
                    questionText.GetComponent<Text>().text = question;
                }

                strResult = "";
                bResultDisplay = false;
                textAnswer.text = "";
            }
        }
        else
        {
            timer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    //logText.text += "Mouse down position (" + "X:" + Input.mousePosition.x + " Y:" + Input.mousePosition.y + ")\n";
        //    logText.text += "X:" + Input.mousePosition.x + " Y:" + Input.mousePosition.y + "\n";
        //    scroll_rect.verticalNormalizedPosition = 0.0f;      // Scroll bottom (0.0f), Scroll top (1.0f)
        //}

        PanelControl(toggleUI);

        Timer();
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
        //string path = Application.dataPath;
        string path = Application.persistentDataPath;   // for Android
        path = path.Substring(0, path.LastIndexOf('/'));
        Debug.Log(path);

        FileStream f = new FileStream(path + "/Data.txt", FileMode.Append, FileAccess.Write);
        //FileStream f = new FileStream(m_strPath + "Data.txt", FileMode.Append, FileAccess.Write);

        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);

        writer.WriteLine(strData);

        writer.Close();

        f.Close();

        statusText.GetComponent<Text>().text = "File write";

        // Another example

        /*
        string path = Application.dataPath;
        path = path.Substring(0, path.LastIndexOf('/'));
        Debug.Log(path);

        string strFileName = "/Log";

        path += strFileName;
        //Debug.Log(System.DateTime.Now.ToString("yy/MM/dd/hh/mm"));
        //path += System.DateTime.Now.ToString("_yyMMdd_hhmm");
        //path += System.DateTime.Now.ToString("_yyMMdd_HHmm");
        path += ".txt";

        StreamWriter sw;
        sw = new StreamWriter(path);
        //StreamWriter sw = new StreamWriter(path);
        //StreamWriter sw = new StreamWriter(strFileName);
        sw.WriteLine("Program start");
        //sw.WriteLine("Another Line");
        //sw.Flush();
        //sw.Close();
        Debug.Log("File make");

        if (File.Exists(strFileName))
        {
            //Debug.Log(strFileName + " already exists.");
        }
        else
        {

        }
        */
    }

    public void Parse()
    {
        //string path = Application.dataPath;
        //path = path.Substring(0, path.LastIndexOf('/'));
        //path += "/Baseball.INI";
        //Debug.Log(path);

        //string path = Application.dataPath;
        string path = Application.persistentDataPath;   // for Android
        path = path.Substring(0, path.LastIndexOf('/'));
        Debug.Log(path);

        StreamReader sr = new StreamReader(path + "/Data.txt");
        //StreamReader sr = new StreamReader(m_strPath + "Data.txt");
        string line = "";

        if (sr == null)
        {
            print("Error : " + m_strPath);  // Log message
            statusText.GetComponent<Text>().text = "Read error";
        }
        else
        {
            statusText.GetComponent<Text>().text = "File read";
            line = sr.ReadLine();
            Debug.Log(line);
            statusText.GetComponent<Text>().text = line;
            while (line != null)
            {
                line = sr.ReadLine();
                Debug.Log(line);
                //statusText.GetComponent<Text>().text = line;
            }
        }

        sr.Close();


        // File must exist in resource folder 
        /*

        TextAsset data = Resources.Load("Data", typeof(TextAsset)) as TextAsset;

        if (data == null)
        {
            Debug.Log("No Textasset! data");
            return;
        }

        StringReader sr = new StringReader(data.text);

        // 먼저 한줄을 읽는다. 
        string source = sr.ReadLine();
        string[] values;                // 쉼표로 구분된 데이터들을 저장할 배열 (values[0]이면 첫번째 데이터 )

        //Debug.Log("ReadLine");

        while (source != null)
        {
            values = source.Split(',');  // 쉼표로 구분한다. 저장시에 쉼표로 구분하여 저장하였다.
            //Debug.Log("values");

            if (values.Length == 0)
            {
                sr.Close();
                return;
            }
            source = sr.ReadLine();    // 한줄 읽는다.

            Debug.Log(source);
        }

        */

    }

    void ShowProblem()
    {
        // { "question", "1 + 2 = ?"},

        //question = problems[problemNumber - 1]["question"];
        //answer = problems[problemNumber - 1]["answer"];
        //example1 = problems[problemNumber - 1]["example1"];
        //example2 = problems[problemNumber - 1]["example2"];
        //example3 = problems[problemNumber - 1]["example3"];
        //example4 = problems[problemNumber - 1]["example4"];

        System.Random randomDirection = new System.Random();
        int directionChoice = randomDirection.Next(1, 10);

        //firstNumber = UnityEngine.Random.Range(1, 10);
        //secondNumber = UnityEngine.Random.Range(1, 10);

        //firstNumber = randomDirection.Next(1, 10);
        //secondNumber = randomDirection.Next(1, 10);

        // Basic lavel 1 (10 이하의 덧셈)
        do
        {
            firstNumber = randomDirection.Next(1, 8);
            secondNumber = randomDirection.Next(1, 8);
            //Debug.Log(firstNumber.ToString() + " + " + secondNumber.ToString() + " = ?");
            //Debug.Log((firstNumber + secondNumber).ToString());
        } while ((firstNumber + secondNumber) > 9);

        //if((firstNumber + secondNumber) > 10)
        //{
        //    firstNumber = randomDirection.Next(1, 8);
        //    secondNumber = randomDirection.Next(1, 8);
        //    Debug.Log("Over 10");
        //}

        question = firstNumber.ToString() + " + " + secondNumber.ToString() + " = ?";

        questionText.GetComponent<Text>().text = question;

        textQNumber.text = problemNumber.ToString() + " / " + problemsLength.ToString(); 

        //example1Text.GetComponent<Text>().text = example1;
        //example2Text.GetComponent<Text>().text = example2;
        //example3Text.GetComponent<Text>().text = example3;
        //example4Text.GetComponent<Text>().text = example4;
    }


    public void Example0ButtonClicked()
    {
        //Debug.Log("Example1ButtonClicked");
        //SelectExample(example1);

        textAnswer.text += "0";
        //logText.text += "0" + "\n";
        //scroll_rect.verticalNormalizedPosition = 0.0f;      // Scroll bottom (0.0f), Scroll top (1.0f)

        //Debug.Log(example1);
        //problemNumber += 1;
        //ShowProblem();
    }

    public void Example1ButtonClicked()
    {
        textAnswer.text += "1";
        //logText.text += "1" + "\n";
        //scroll_rect.verticalNormalizedPosition = 0.0f;      // Scroll bottom (0.0f), Scroll top (1.0f)
    }

    public void Example2ButtonClicked()
    {
        textAnswer.text += "2";
        //logText.text += "2" + "\n";
        //scroll_rect.verticalNormalizedPosition = 0.0f;      // Scroll bottom (0.0f), Scroll top (1.0f)
    }

    public void Example3ButtonClicked()
    {
        textAnswer.text += "3";
        //logText.text += "3" + "\n";
        //scroll_rect.verticalNormalizedPosition = 0.0f;      // Scroll bottom (0.0f), Scroll top (1.0f)
    }

    public void Example4ButtonClicked()
    {
        textAnswer.text += "4";
        //logText.text += "4" + "\n";
        //scroll_rect.verticalNormalizedPosition = 0.0f;      // Scroll bottom (0.0f), Scroll top (1.0f)
    }


    public void Example5ButtonClicked()
    {
        textAnswer.text += "5";
        //logText.text += "5" + "\n";
        //scroll_rect.verticalNormalizedPosition = 0.0f;      // Scroll bottom (0.0f), Scroll top (1.0f)
    }

    public void Example6ButtonClicked()
    {
        textAnswer.text += "6";
        //logText.text += "6" + "\n";
        //scroll_rect.verticalNormalizedPosition = 0.0f;      // Scroll bottom (0.0f), Scroll top (1.0f)
    }

    public void Example7ButtonClicked()
    {
        textAnswer.text += "7";
        //logText.text += "7" + "\n";
        //scroll_rect.verticalNormalizedPosition = 0.0f;      // Scroll bottom (0.0f), Scroll top (1.0f)
    }

    public void Example8ButtonClicked()
    {
        textAnswer.text += "8";
        //logText.text += "8" + "\n";
        //scroll_rect.verticalNormalizedPosition = 0.0f;      // Scroll bottom (0.0f), Scroll top (1.0f)
    }

    public void Example9ButtonClicked()
    {
        textAnswer.text += "9";
        //logText.text += "9" + "\n";
        //scroll_rect.verticalNormalizedPosition = 0.0f;      // Scroll bottom (0.0f), Scroll top (1.0f)
    }

    public void ExampleClearButtonClicked()
    {
        textAnswer.text = "";
        logText.text += "Clear" + "\n";
        scroll_rect.verticalNormalizedPosition = 0.0f;      // Scroll bottom (0.0f), Scroll top (1.0f)
    }

    public void ExampleEnterButtonClicked()
    {
        //logText.text += "Enter" + "\n";

        scroll_rect.verticalNormalizedPosition = 0.0f;

        if ((firstNumber + secondNumber) == int.Parse(textAnswer.text))
        {
            logText.text += "Correct" + "\n";
            // Display result 1 sec

            strResult = "Correct";
            questionText.GetComponent<Text>().text = strResult;
            waitingTime = 0.5f;
            bResultDisplay = true;

            //StartCoroutine(WaitForIt());

            /*
            if (problemNumber < problemsLength)
            {
                problemNumber += 1;
                ShowProblem();
                Debug.Log("ShowProblem");
            }
            else
            {
                Debug.Log("ShowGameOverBox");

                // Save result log
                ShowGameOverBox();
            }
            */
        }
        else
        {
            logText.text += "Incorrect" + "\n";
            // Display result 1 sec

            strResult = "Incorrect";
            questionText.GetComponent<Text>().text = strResult;
            waitingTime = 0.5f;
            bResultDisplay = true;

            //StartCoroutine(WaitForIt());
        }

        scroll_rect.verticalNormalizedPosition = 0.0f;

        //if ((firstNumber + secondNumber) == int.Parse(textAnswer.text))
        //    logText.text += "Correct" + "\n";
        //else
        //    logText.text += "Incorrect" + "\n";

        //scroll_rect.verticalNormalizedPosition = 0.0f;

        //textAnswer.text = "";

        //if (problemNumber < problemsLength)
        //{
        //    problemNumber += 1;
        //    ShowProblem();
        //}
        //else
        //{
        //    Debug.Log("ShowGameOverBox");
        //    ShowGameOverBox();
        //}

    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(2.0f);
        Debug.Log("WiatForSeconds");
        //check = true;
    }

    void SelectExample(string example)
    {
        //Debug.Log(example);

        //if (answer.Equals(example))
        //{
        //    totalCorrect += 1;
        //    totalCorrectText.GetComponent<Text>().text = totalCorrect.ToString();
        //    correctIncorrectText.GetComponent<Text>().text = "Correct";
        //}
        //else
        //{
        //    correctIncorrectText.GetComponent<Text>().text = "Incorrect";
        //}

        //if (problemNumber < problems.Length)
        //{
        //    problemNumber += 1;
        //    ShowProblem();
        //}
        //else
        //{
        //    Debug.Log("ShowGameOverBox");
        //    ShowGameOverBox();
        //}

        //problemNumber += 1;
        //ShowProblem();
    }

    void ShowGameOverBox()
    {
        gameOverPanel.SetActive(true);
    }


    public void Replay()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitApp()
    {
        Application.Quit();
    }


    public void PanelControl(Toggle toggletest)
    {
        if(toggletest.isOn)
        {
            scroll_rect.gameObject.SetActive(true);
        }
        else
        {
            scroll_rect.gameObject.SetActive(false);
        }
    }
}

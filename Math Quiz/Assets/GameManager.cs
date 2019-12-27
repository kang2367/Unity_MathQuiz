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
    [SerializeField]
    private TextListControl textListControl;

    public GameObject questionText;
    public GameObject example1Text;
    public GameObject example2Text;
    public GameObject example3Text;
    public GameObject example4Text;

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


    public GameObject mathMain;
    private Text logText = null;
    private ScrollRect scroll_rect = null;
    private InputField inputAnswer = null;
    private Text textAnswer = null;
    private Text textQNumber = null;

    public GameObject logMain;

    //int firstNumber =  UnityEngine.Random.Range(1, 10);
    //int secondNumber = UnityEngine.Random.Range(1, 10);
    int firstNumber = 0;
    int secondNumber = 0;
    int problemsLength = 30; //3;//10;

    private Toggle toggleUI = null;

    float timer;
    float waitingTime = 0.0f;

    string strResult = "";
    bool bResultDisplay = false;

    // Start is called before the first frame update
    void Start()
    {
        if(mathMain.activeSelf == true)
        {
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

            scroll_rect.gameObject.SetActive(false);
            toggleUI.gameObject.SetActive(false);

        }

    }

    private void Awake()
    {

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

        if(toggleUI != null)
            if(toggleUI.IsActive() == true)
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

    // ------------------
    // Menu move function 
    // ------------------

    public void LogButtonClicked()
    {
        Debug.Log("LogButtonClicked");

        mathMain.SetActive(false);
        logMain.SetActive(true);

        string path = Application.persistentDataPath;   // for Android
        path = path.Substring(0, path.LastIndexOf('/'));
        Debug.Log(path);

        StreamReader sr = new StreamReader(path + "/Data.txt");
        string line = "";

        if (sr == null)
        {
            print("Error : " + m_strPath);  // Log message
        }
        else
        {
            do
            {
                line = sr.ReadLine();
                if (line == null)
                {
                    sr.Close();
                    return;
                }
                Debug.Log(line);
                textListControl.AddTextList(line);
            } while (line != null);
        }
        sr.Close();
    }

    public void MathButtonClicked()
    {
        logMain.SetActive(false);
        mathMain.SetActive(true);
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

    int iOldFirstNumber = 0;
    int iOldSecondNumber = 0;

    void ShowProblem()
    {
        System.Random randomDirection = new System.Random();
        int directionChoice = randomDirection.Next(1, 10);

        // Basic lavel 1 (10 이하의 덧셈) => 자릿수 올림이 없는 한 자릿수 덧셈
        do
        {
            firstNumber = randomDirection.Next(1, 8);
            secondNumber = randomDirection.Next(1, 8);
            //Debug.Log(firstNumber.ToString() + " + " + secondNumber.ToString() + " = ?");
            //Debug.Log((firstNumber + secondNumber).ToString());

        //} while ((firstNumber + secondNumber) > 9);
        } while (((firstNumber + secondNumber) > 9) || ((iOldFirstNumber == firstNumber) && (iOldSecondNumber == secondNumber)));

        iOldFirstNumber = firstNumber;
        iOldSecondNumber = secondNumber;

        question = firstNumber.ToString() + " + " + secondNumber.ToString() + " = ?";

        // Basic level 2 (10 이상의 덧셈) => 자릿수 올림이 있는 한 자릿수 덧셈
        

        // Basic level 3 (10 이하의 뺄셈) => 한 자릿수 뺄셈


        // Basic level 4 (한 자릿수 곱셈)

    
        // Basic level 5 (한 자릿수 나눗셈)


        // 연속된 한 자릿수 덧셈

        questionText.GetComponent<Text>().text = question;

        textQNumber.text = problemNumber.ToString() + " / " + problemsLength.ToString(); 
    }


    public void Example0ButtonClicked()
    {
        //Debug.Log("Example1ButtonClicked");
        textAnswer.text += "0";
    }

    public void Example1ButtonClicked()
    {
        textAnswer.text += "1";
    }

    public void Example2ButtonClicked()
    {
        textAnswer.text += "2";
    }

    public void Example3ButtonClicked()
    {
        textAnswer.text += "3";
    }

    public void Example4ButtonClicked()
    {
        textAnswer.text += "4";
    }


    public void Example5ButtonClicked()
    {
        textAnswer.text += "5";
    }

    public void Example6ButtonClicked()
    {
        textAnswer.text += "6";
    }

    public void Example7ButtonClicked()
    {
        textAnswer.text += "7";
    }

    public void Example8ButtonClicked()
    {
        textAnswer.text += "8";
    }

    public void Example9ButtonClicked()
    {
        textAnswer.text += "9";
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

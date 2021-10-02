using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{

    [HideInInspector] public readonly int pointsPerQuestion = 10;
    [HideInInspector] public static int totalQuestions;
    [HideInInspector] public static int currentQuestionIndex=0;
    [HideInInspector] public static int attemptedQuestions = 0;

    public GameObject QuizCardPrefab;
    public GameObject QuestionPanel;
    public GameObject OptionsPanel;
    public GameObject OptionButtonPrefab;
    public GameObject NextButton;
    public TMP_Text score;

    public ToggleGroup toggleGroup;
    public List<GameObject> options;

    private int answerIndex=-1;

    private void OnEnable()
    {
        GameManager.OnGameStart += Init;
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.FloorToInt(GameManager.Instance.timer)==0)
        {
            NextButton.GetComponent<Button>().interactable = false;
        }
    }

    void PopulateQuizCard(int currentIndex)
    {
        answerIndex = -1;
        QuestionPanel.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.networkTest.quizDataArray.quizDataList[currentIndex].question;

        if (options.Count==0)
        {
            for (int i = 0; i < GameManager.Instance.networkTest.quizDataArray.quizDataList[currentIndex].options.Length; i++)
            {
                GameObject go = Instantiate(OptionButtonPrefab, OptionsPanel.transform) as GameObject;
                go.transform.name = (i).ToString();
                go.GetComponent<Toggle>().group = go.GetComponentInParent<ToggleGroup>();
                go.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                go.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.networkTest.quizDataArray.quizDataList[currentIndex].options[i];
                options.Add(go);

            }
        }
        else
        {
            for (int i = 0; i < options.Count; i++)
            {
                options[i].GetComponentInChildren<TMP_Text>().text = GameManager.Instance.networkTest.quizDataArray.quizDataList[currentIndex].options[i];
                options[i].GetComponent<Toggle>().isOn = false;
            }
        }
    }

    void Init()
    {
        totalQuestions = GameManager.Instance.networkTest.quizDataArray.quizDataList.Length;
        currentQuestionIndex = 0;
        attemptedQuestions = 0;
        GameManager.score = 0;
        score.text = "Points: "+ GameManager.score;
        PopulateQuizCard(currentQuestionIndex);
        NextButton.GetComponent<Button>().interactable = true;
    }

    public void EvaluateAnswer()
    {
        
        foreach(Toggle toggle in toggleGroup.transform.GetComponentsInChildren<Toggle>())
        {
            if (toggle.isOn)
            {
                if(int.TryParse(toggle.transform.name,out answerIndex))
                {
                    if(answerIndex== GameManager.Instance.networkTest.quizDataArray.quizDataList[currentQuestionIndex].correctOptionIndex)
                    {
                        Debug.Log("<color=green>" + "Right Answer" + "</color>");

                        GameManager.score += pointsPerQuestion;
                        score.text = "Points: "+GameManager.score.ToString();
                        attemptedQuestions += 1;
                    }
                    else
                    {
                        Debug.Log("<color=red>" + "Wrong Answer" + "</color>");
                        attemptedQuestions += 1;
                    }
                }

                break;
            }

        }

        if (answerIndex==-1)
            Debug.Log("<color=blue>" + "Not Attempted" + "</color>");

        if (currentQuestionIndex < totalQuestions - 1)
            PopulateQuizCard(++currentQuestionIndex);
        else
            GameManager.OnGameOver.Invoke();

        
    }
    private void OnDisable()
    {
        GameManager.OnGameStart += Init;
    }


}

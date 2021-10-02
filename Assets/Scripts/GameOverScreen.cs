using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public TMPro.TMP_Text questionsAttempted;
    public TMPro.TMP_Text timeLeftText;
    public TMPro.TMP_Text playerScoreText;

    public UnityEngine.UI.Button retryButton;

    private float timeLeft;
    private float playerScore;
    // Start is called before the first frame update

    private void OnEnable()
    {
        GameManager.OnGameOver += UpdateValues;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateValues()
    {
        timeLeft = Mathf.FloorToInt(GameManager.Instance.timer);
        playerScore = GameManager.score;

        questionsAttempted.text = "Questiones Attempted: " + QuizController.attemptedQuestions.ToString() +"/" +QuizController.totalQuestions.ToString();
        timeLeftText.text = "Time Left: "+Mathf.FloorToInt(GameManager.Instance.timer).ToString();
        playerScoreText.text = "Score: "+GameManager.score.ToString();

        if(QuizController.attemptedQuestions== QuizController.totalQuestions)
        {
            retryButton.interactable = false;
            StartCoroutine(TimetoScore());
        }
        else
            retryButton.interactable = true;

    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= UpdateValues;
    }

    IEnumerator TimetoScore()
    {
        yield return new WaitForSeconds(2f);
            while (timeLeft >0)
            {
                timeLeft -= 1;
                playerScore += 1;
                timeLeftText.text = "Time Left: " + timeLeft.ToString();
                playerScoreText.text = "Score: " + playerScore.ToString();
                yield return new WaitForSeconds(0.1f);

            }
        retryButton.interactable = true;
        StopCoroutine(TimetoScore());
    }
}

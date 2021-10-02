using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TweenFunctions : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.OnGameOver += AnimateGameOverScreen;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AnimateGameOverScreen()
    {
        LeanTween.moveY(GameManager.Instance.gameOverScreen.GetComponent<RectTransform>(), 0f, 1f).setEaseOutBounce();
    }
    public void OnRetryAnimateGameOverScreen()
    {
        LeanTween.moveY(GameManager.Instance.gameOverScreen.GetComponent<RectTransform>(), -2000f, 2f).setEaseInElastic();
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= AnimateGameOverScreen;
    }
}

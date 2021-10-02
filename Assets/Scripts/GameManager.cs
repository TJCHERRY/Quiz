using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private  static GameManager instance=null;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if(instance == null)
                {
                    GameObject gm = new GameObject();
                    instance = gm.AddComponent<GameManager>();
                    DontDestroyOnLoad(gm);
                }
            }
            return instance;
        }
    }

    public static int score;
    public TMP_Text countDownText;
    public readonly float maxTimeLimit=61f;
    [HideInInspector] public float timer;
    [HideInInspector]public NetworkTest networkTest;
    [HideInInspector] public static UnityAction OnGameStart;
    [HideInInspector] public static UnityAction OnGameOver;
    [HideInInspector] public static UnityAction OnRestart;

    public GameObject gameOverScreen;
    public GameObject gameScreen;
    public GameObject startScreen;

    private bool startTimer;

    private void OnEnable()
    {
        OnGameStart += StartTimer;
        OnGameOver += DisplayGameOverScreen;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (networkTest == null)
        {
            networkTest = transform.GetComponent<NetworkTest>();
        }

 
    }

    // Start is called before the first frame update
    void Start()
    {
        if(networkTest!=null)
            StartCoroutine(networkTest.GetNetworkConnection("https://my-json-server.typicode.com/strshri/json-server/questionsAndAnswers"));

        timer = maxTimeLimit;

    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;
            countDownText.text = Mathf.FloorToInt(timer).ToString();

            if (Mathf.FloorToInt(timer) == 0)
            {
                startTimer = false;
                GameManager.OnGameOver.Invoke();
            }
        }

    }

    public void StartTimer()
    {
        startTimer = true;
    }

    public void DisplayGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        timer = maxTimeLimit;
        OnGameStart.Invoke();
    }


    private void OnDisable()
    {
        OnGameStart -= StartTimer;
        OnGameOver -= DisplayGameOverScreen;
    }
}

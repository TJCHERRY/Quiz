using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
public class NetworkTest : MonoBehaviour
{
    private string jsonData=null;
    public static UnityEvent OnGetRquestSuccess;
    [HideInInspector]public DataArray quizDataArray;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(GetNetworkConnection("https://my-json-server.typicode.com/strshri/json-server/questionsAndAnswers"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    //stores the json data in an object format
    public void  ProcessJsonData(DownloadHandler downloadHandler)
    {
        quizDataArray = JsonUtility.FromJson<DataArray>("{\"quizDataList\":" + downloadHandler.text + "}");
    }

    //Sends a connection request to the remote server and returns the data if connection has succeeded else display the nature of error
    public IEnumerator GetNetworkConnection(string url)
    {
        UnityWebRequest getRequest = UnityWebRequest.Get(url);

        yield return getRequest.SendWebRequest();

        string[] pages = url.Split('/');
        int page = pages.Length - 1;

        switch (getRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.LogError(pages[page] + ": Error: " + getRequest.error);
                break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(pages[page] + ": Error: " + getRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(pages[page] + ": HTTP Error: " + getRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(pages[page] + ": \nReceived:\n" + getRequest.downloadHandler.text);
                ProcessJsonData(getRequest.downloadHandler);
                GameManager.OnGameStart.Invoke();
                break;
        }

        StopAllCoroutines();

    }
}

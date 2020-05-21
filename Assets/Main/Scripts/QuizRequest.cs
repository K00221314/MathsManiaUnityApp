using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Quiz;

public class QuizRequest : MonoBehaviour
{

    [SerializeField]
    
    private readonly string baseUrl = "http://localhost:8080/MathsManiaJspCrudServer/webresources/restful.results";

    [SerializeField]
   
    private readonly int questionAmount = 10;

    
    private Action<Question[]> successCallback = null;

   
    //private Action<Error> errorCallback = null;

    #region Private Methods
    
    private IEnumerator RequestQuiz()
    {
        string url = baseUrl + "?amount=" + questionAmount.ToString();
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        //if (request.isNetworkError || request.isHttpError)
        //{
        //    //OnError(request.responseCode, request.error);
        //}
        //else
        {
            byte[] byteResponse = request.downloadHandler.data;
            Success(ToText(byteResponse));
        }
    }

  
    //private void OnError(long statusCode, string errorMessage)
    //{
    //    Error error = new Error
    //    {
    //        statusCode = statusCode,
    //        errorMessage = errorMessage
    //    };
    //    errorCallback(error);
    //}

    
    private void Success(string jsonArrayText)
    {
        string jsonObject = InsertArrayIntoObject("Items", jsonArrayText);
        //Debug.Log("jsonObject");
        Debug.Log(jsonObject);
        Question[] questions = JsonHelper.FromJson<Question>(jsonObject);
        successCallback(questions);
    }

    private string InsertArrayIntoObject(string arrayName, string jsonArrayText)
    {
        return "{\"" + arrayName + "\":" + jsonArrayText + "}";
    }
    #endregion

    #region Public Interface
    
    public static string ToText(byte[] bytes)
    {
        return System.Text.Encoding.Default.GetString(bytes);
    }

  
    public void Request(Action<Question[]> successCallback, Action<Error> errorCallback)
    {
        this.successCallback = successCallback;
        //this.errorCallback = errorCallback;
        StartCoroutine(RequestQuiz());
    }
    #endregion
}
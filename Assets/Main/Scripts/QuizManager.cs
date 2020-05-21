using System;
using System.Collections.Generic;
using UnityEngine;
using Quiz;

public class QuizManager : MonoBehaviour
{

    #region Static Field
    private static QuizManager _instance = null;
    #endregion

    #region Serialized Fields
    
    [SerializeField]
    private QuizRequest api = null;

    [SerializeField]
    private QuestionCanvas canvas = null;
    #endregion

    #region Private Fields
    private List<Question> questions;
    private int currentQuestion = 0;
    private int score = 0;
    #endregion

    #region Public Properties
   
    public static QuizManager Instance
    {
        get
        {
            return _instance;
        }
    }

    
    public bool IsLastQuestion
    {
        get
        {
            Debug.Log(questions.Count + ", " + currentQuestion);
            return questions.Count - 1 == currentQuestion;
        }
    }
    #endregion

    void Start()
    {
        if (api) 
        {
            api.Request(
                new Action<Question[]>((Question[] response) =>
                { 

                    questions = new List<Question>(response);

                    RenderQuestion();

                    if (canvas)
                        canvas.HideFeedback();

                }),
                new Action<Error>((Error error) =>
                { // Error callback.

                    Debug.Log(error.statusCode + ", " + error.errorMessage);

                })
            );
        }

        _instance = this;
    }

   
    public void RenderQuestion()
    {
        Question q = questions[currentQuestion]; 

        if (canvas)
        {
            canvas.SetQuestion(q, currentQuestion); 
        }
    }

    
    public void NextQuestion()
    {
        if (currentQuestion < questions.Count)
        {
            currentQuestion++;
            RenderQuestion();
        }
    }

    
    public void FinishedQuiz()
    {
        if (canvas)
            canvas.FinishedQuiz(score);
    }

    
    public void HideFeedback()
    {
        if (canvas)
            canvas.HideFeedback();
    }

   
    public void Respond(ButtonOption selectedOption)
    {
        if (selectedOption.isCorrect)
        {
            Success();
        }
        else
        {
            Failure();
        }
    }

    
    public void Success()
    {
        Debug.Log("Correct!");
        canvas.Success();

        ScoreUp();
        canvas.SetScore(score);
    }

   
    public void Failure()
    {
        Debug.Log("Wrong!");
        canvas.Failure();

    }

    
    public void ScoreUp()
    {
        score += 10;
    }

   
    public void Restart()
    {
        score = 0;
        canvas.SetScore(score);

        currentQuestion = 0;

        HideFeedback();
        RenderQuestion();
    }
}

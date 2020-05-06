using UnityEngine;
using UnityEditor;
using Quiz;
using System.Collections.Generic;

public class JSONTest
{
    internal static void DoIt()
    {
        var question1 = new Question
        {
            category = "Maths",
            correctAnswer = "5",
            difficulty = "Easy",
            incorrectAnswers1 = "4",
            question = "how many lights do you see",
            type = "Toture",
        };

        var question2 = new Question
        {
            category = "Strategy",
            correctAnswer = "5 ships",
            difficulty = "Hard",
            incorrectAnswers1 = "I don't know",
            question = "What are the federation defense plans for menus corva",
            type = "Defense",
        };

        var question1Json = JsonUtility.ToJson(question1);
        Debug.Log(question1Json);
        var question1Reborn = JsonUtility.FromJson<Question>(question1Json);
        Debug.Log(question1Reborn.question);

        Question[] questions = { question1, question2 };

        List<Question> questionList = new List<Question>();


        Debug.Log("Print JSON ARRAY");
        Debug.Log(JsonUtility.ToJson(questions));
        Debug.Log(JsonUtility.ToJson(questionList));
        Debug.Log(JsonHelper.ToJson<Question>(questions));

    }
}

using UnityEngine;
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
            question = "how many apples do you see",
            type = "3rd class",
        };

        var question2 = new Question
        {
            category = "Maths",
            correctAnswer = "4",
            difficulty = "Hard",
            incorrectAnswers1 = "6 5 7",
            question = "What is the blah blah blah",
            type = "3rd class",
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

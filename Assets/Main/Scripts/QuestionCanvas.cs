using Quiz;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class QuestionCanvas : MonoBehaviour
{

    #region Serielized Fields
    [SerializeField]
    Text textScore = null;

    [SerializeField]
    Text textTitle = null;

    [SerializeField]
    Text textQuestion = null;

    [SerializeField]
    ButtonOption alternative1 = null;

    [SerializeField]
    ButtonOption alternative2 = null;

    [SerializeField]
    ButtonOption alternative3 = null;

    [SerializeField]
    ButtonOption alternative4 = null;

    [SerializeField]
    GameObject successFeedback = null;

    [SerializeField]
    GameObject failureFeedback = null;

    [SerializeField]
    GameObject panelLoading = null;

    [SerializeField]
    GameObject panelFinished = null;
    #endregion

    /**
     * Hides all feedback panels.
     */
    public void HideFeedback()
    {
        if (successFeedback)
            successFeedback.SetActive(false);

        if (failureFeedback)
            failureFeedback.SetActive(false);

        if (panelLoading)
            panelLoading.SetActive(false);

        if (panelFinished)
            panelFinished.SetActive(false);
    }

    /**
     * Displays Success feedback panel.
     */
    public void Success()
    {
        if (successFeedback)
            successFeedback.SetActive(true);
    }

    /**
     * Displays Failure feedback panel.
     */
    public void Failure()
    {
        if (failureFeedback)
            failureFeedback.SetActive(true);
    }

    /**
     * Sets score display value.
     */
    public void SetScore(int score)
    {
        if (textScore)
            textScore.text = score.ToString("D4"); // Format score value to display with four leading zeroes.
    }

    /**
     * Displays Finished feedback panel.
     */
    public void FinishedQuiz()
    {
        if (panelFinished)
            panelFinished.SetActive(true);
    }

    public void FinishedQuiz(int score)
    {
        if (panelFinished)
        {
            panelFinished.SetActive(true);
            FinishedPanel fp = panelFinished.GetComponent<FinishedPanel>();
            fp.SetFinalScore(score);
        }

    }

    /**
     * Displays a new question on the screen.
     */
    public void SetQuestion(Question question, int questionNumber)
    {
        ButtonOption[] buttonOptions =
        {
            alternative1,
            alternative2,
            alternative3,
            alternative4,
        };

        foreach(var buttonOption in buttonOptions)
        {
            buttonOption.SetText(string.Empty);
            buttonOption.isCorrect = false;
        }




        Debug.Log($"--------Question:{questionNumber}--------");
        Debug.Log($"Number of Possible Answers:{question.possible_answers.Count}");

        int[] order = new int[question.possible_answers.Count];

        for(int index = 0; index< order.Length; index++)
        {
            order[index] = index;
        }
        
        RandominiseArrayContents(order, 10);


        if (textQuestion)
            textQuestion.text = question.question; // Set question text.

        if (textTitle)
            textTitle.text = "Question " + (questionNumber + 1); // Set question number.

        for (int answerNumber = 0; answerNumber < question.possible_answers.Count; answerNumber++)
        {
            initializeButton(buttonOptions[answerNumber], answerNumber, question, order);
            //initializeButton(alternative2, 1, question, order);
            //initializeButton(alternative3, 2, question, order);
            //initializeButton(alternative4, 3, question, order);
        }

        //if (alternative2)
        //{
        //    alternative2.SetText(question.incorrect_answers[0]); // Set alternative 2.
        //    alternative2.isCorrect = false;
        //}


        //if (alternative3)
        //{
        //    if (question.incorrect_answers.Count > 2)
        //    {
        //        alternative3.SetText(question.incorrect_answers[1]); // Set alternative 3.
        //        alternative3.isCorrect = false;
        //    }

        //}

        //if (alternative4)
        //{
        //    if (question.incorrect_answers.Count > 2)
        //    {
        //        alternative4.SetText(question.incorrect_answers[2]); // Set alternative 4.
        //        alternative4.isCorrect = false;
        //    }
        //}

        //        for ( int i = 0; i < question.correctAnswer.count; i++)
        //        {
        //            ButtonList[i].transform.SetSiblingIndex(i)
        //}


    }

    private void initializeButton(ButtonOption buttonOption, int buttonNumber, Question question, int[] order)
    {
        const int correctAnswerIndex = 0;
        if (buttonOption)
        {
            Debug.Log($"buttonNumber:{buttonNumber}");
            Debug.Log($"order[buttonNumber]:{order[buttonNumber]}");
            Debug.Log($"question.possible_answers[order[buttonNumber]]:{question.possible_answers[order[buttonNumber]]}");

            buttonOption.SetText(question.possible_answers[order[buttonNumber]]);
            buttonOption.isCorrect = order[buttonNumber] == correctAnswerIndex;
        }
    }

    public void RandominiseArrayContents(int[] arrray, int swapTimes)
    {
        System.Random random = new System.Random(Guid.NewGuid().GetHashCode());//https://stackoverflow.com/questions/1785744/how-do-i-seed-a-random-class-to-avoid-getting-duplicate-random-values

        for (int swapCount = 0; swapCount < swapTimes; swapCount++)
        {
            int randomIndex1 = random.Next(0, arrray.Length);
            int randomIndex2 = random.Next(0, arrray.Length);

            if (randomIndex1 != randomIndex2)
            {
                swapArrayElements(arrray, randomIndex1, randomIndex2);
            }
        }
    }
    public void swapArrayElements(int[] arrray, int lhsIndex, int rhsIndex)
    {
        int swapSapce = arrray[lhsIndex];
        arrray[lhsIndex] = arrray[rhsIndex];
        arrray[rhsIndex] = swapSapce;
    }
}

using Quiz;
using System;
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


    /// <summary>
    /// Sets score display value.
    /// </summary>
    /// <param name="score"></param>
    public void SetScore(int score)
    {
        if (textScore)
            textScore.text = score.ToString("D4"); // Format score value to display with four leading zeroes.
    }


    /// <summary>
    /// Displays Finished feedback panel.
    /// </summary>
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
            FinishedPanel finishedPanel = panelFinished.GetComponent<FinishedPanel>();
            finishedPanel.SetFinalScore(score);
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
        ClearButtonsData(buttonOptions);

        int[] order = GenerateRandomAnswerOrder(question);

        InitialiseQuestionDisplay(question, questionNumber);


        for (int answerNumber = 0; answerNumber < question.possible_answers.Count; answerNumber++)
        {
            InitializeButton(buttonOptions[answerNumber], answerNumber, question, order);

        }
    }

    private void InitialiseQuestionDisplay(Question question, int questionNumber)
    {
        if (textQuestion)
            textQuestion.text = question.question; // Set question text.

        if (textTitle)
            textTitle.text = "Question " + (questionNumber + 1); // Set question number.
    }

    private int[] GenerateRandomAnswerOrder(Question question)
    {
        int[] order = new int[question.possible_answers.Count];

        for (int index = 0; index < order.Length; index++)
        {
            order[index] = index;
        }

        RandominiseArrayContents(order, 10);
        return order;
    }

    private static void ClearButtonsData(ButtonOption[] buttonOptions)
    {
        foreach (var buttonOption in buttonOptions)
        {
            buttonOption.SetText(string.Empty);
            buttonOption.isCorrect = false;
        }
    }

    private void InitializeButton(ButtonOption buttonOption, int buttonNumber, Question question, int[] order)
    {
        const int correctAnswerIndex = 0;
        if (buttonOption)
        {
            buttonOption.SetText(question.possible_answers[order[buttonNumber]]);
            buttonOption.isCorrect = order[buttonNumber] == correctAnswerIndex;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="arrray"></param>
    /// <param name="swapTimes">The number of swaps to preporm on the arry</param>
    public void RandominiseArrayContents(int[] arrray, int swapTimes)
    {
        System.Random random = new System.Random(Guid.NewGuid().GetHashCode());//https://stackoverflow.com/questions/1785744/how-do-i-seed-a-random-class-to-avoid-getting-duplicate-random-values

        for (int swapCount = 0; swapCount < swapTimes; swapCount++)
        {
            int randomIndex1 = random.Next(0, arrray.Length);
            int randomIndex2 = random.Next(0, arrray.Length);

            if (randomIndex1 != randomIndex2)
            {
                SwapArrayElements(arrray, randomIndex1, randomIndex2);
            }
        }
    }
    /// <summary>
    /// Swaps the contents of two locations in an array
    /// </summary>
    /// <param name="arrray"></param>
    /// <param name="lhsIndex"></param>
    /// <param name="rhsIndex"></param>
    public void SwapArrayElements(int[] arrray, int lhsIndex, int rhsIndex)
    {
        int swapSapce = arrray[lhsIndex];
        arrray[lhsIndex] = arrray[rhsIndex];
        arrray[rhsIndex] = swapSapce;
    }
}

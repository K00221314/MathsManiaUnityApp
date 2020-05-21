using UnityEngine;
using UnityEngine.UI;

public class OnNextClicked : MonoBehaviour {

    
    public void OnNextButtonClicked()
    {
        if (!QuizManager.Instance.IsLastQuestion)
        {
            QuizManager.Instance.NextQuestion();
            QuizManager.Instance.HideFeedback();
        }
        else
        {
            QuizManager.Instance.FinishedQuiz();
        }
    }
}

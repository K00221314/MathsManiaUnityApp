using UnityEngine;
using UnityEngine.UI;

public class FinishedPanel : MonoBehaviour {

    [SerializeField]
    private Text finalScore = null;

    
    public void SetFinalScore(int score)
    {
        finalScore.text = score.ToString("D4"); 

    }
}

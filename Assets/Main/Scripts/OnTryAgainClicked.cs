using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTryAgainClicked : MonoBehaviour {

 
	public void OnTryAgainButtonClicked()
    {
        QuizManager.Instance.Restart();
    }
}

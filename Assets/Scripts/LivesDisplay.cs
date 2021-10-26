using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] SceneController sceneController;

    private void Start()
    {
        Player.OnLifeLost += UpdateLivesDisplay;
    }

    private void OnDisable()
    {
        Player.OnLifeLost -= UpdateLivesDisplay;
    }

    private void UpdateLivesDisplay()
    {

        if(transform.childCount > 0 && transform.GetChild(0).gameObject != null)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        else
        {
            sceneController.loadLoseScreen();
        }
       
    }
}

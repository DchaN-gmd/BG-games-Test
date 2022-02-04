using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;

    [Header("Animation")]
    [SerializeField] private Animator _panelController;
    

    public void PauseGame()
    {
        Time.timeScale = 0;
        _resumeButton.gameObject.SetActive(true);
        _panelController.Play("ShowPanel");
        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _resumeButton.gameObject.SetActive(false);
        _panelController.Play("HidePanel");
    }
}

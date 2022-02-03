using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;

    public void PauseGame()
    {
        Time.timeScale = 0;
        _resumeButton.gameObject.SetActive(true);
    }
}

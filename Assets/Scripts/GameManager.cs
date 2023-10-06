using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] QuizController questionController;
    public static bool IsGameOver { get; set; }
    public static bool IsGameActive { get; set; }
    void Start()
    {
        IsGameOver = false;
        IsGameActive = false;
    }
    public void OnGameOver()
    {
        GameManager.IsGameOver = true;
        GameManager.IsGameActive = false;
        questionController.OnGameOver();
    }   
    public void AppQuit()
    {
        Application.Quit();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void GameToMenu() {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void ShowGameOver() {
        Debug.Log("Game Over!");
        gameObject.SetActive(true);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameController gameController;
    public GameObject finalScoreMenu;
    public GameObject inGameMenu;
    private TextMeshProUGUI finalScoreText;
    void Start() {  
        GameObject gameControllerObject = 
            GameObject.FindWithTag("GameController");
        finalScoreMenu.SetActive(false);
        if(gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else Debug.Log("Unable to find game contoller");
        GameObject messageObject = finalScoreMenu.transform.Find("Message").gameObject;
        if(messageObject != null)
            finalScoreText = 
                messageObject.GetComponent<TextMeshProUGUI>();
        else Debug.Log("Unable to find message text");
    }
    public void quit() {
        Debug.Log("Exiting the game");
        Application.Quit();
    }
    public void playGame() {
        Debug.Log("Loading Game");
        gameObject.SetActive(false);
        inGameMenu.SetActive(true);
        gameController.startGame();
    }
    public void toggleMusic() {
        Debug.Log("Toggle Music");
        gameController.toggleMusic();
    }
    public void endGame(int time) {
        Debug.Log("Game Ended in "+ time.ToString());
        inGameMenu.SetActive(false);
    }

    public void showMessage() {
        finalScoreMenu.SetActive(true);
    }
}

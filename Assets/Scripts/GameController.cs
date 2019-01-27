using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public enum GameState {
        NOT_STARTED,
        IN_PROGRESS,
        PAUSED,
        FINISHED
    }
    public static GameState currentGameState;
    public const int MAX_REQUIRED = 2; 
    public Text scoreText;
    public Text winText;
    public Text restartText;
    public GameObject consumable;
    public MainMenu mainMenu; 
    public Text timerText;
    private int remainingItems = MAX_REQUIRED;
    private float timer;
    private Player playerScript;
    private AudioSource audioSource;
    void Start()
    {
        currentGameState = GameState.NOT_STARTED;
        Time.timeScale = 1f;
        GameObject menuObject = GameObject.FindWithTag("MainMenu");
        if(menuObject != null)
            mainMenu = menuObject.GetComponent<MainMenu>();
        else Debug.Log("Unable to find main menu controller");
        GameObject snake = GameObject.FindWithTag("Player");
        if(snake != null)
            playerScript = snake.GetComponent<Player>();
        else Debug.Log("Unable to find player script");
        audioSource = GetComponent<AudioSource>();
    }
    public void startGame() {
        Debug.Log("Starting new Game!");
        currentGameState = GameState.IN_PROGRESS;
        timer = 0;
        Time.timeScale = 1f;
        remainingItems = MAX_REQUIRED;
        scoreText.text = "Remaining : " + remainingItems.ToString();
        createConsumable();
    }
    public void endGame() {
        Debug.Log("Game finished!");
        currentGameState = GameState.FINISHED;
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
        
        Time.timeScale = 0;
        //mainMenu.endGame((int)timer);
    }
    public void pauseGame() {
        Debug.Log("Game is paused");
        currentGameState = GameState.PAUSED;
        Time.timeScale = 0f;
    }
    public void resumeGame() {
        Debug.Log("Resuming game!");
        currentGameState = GameState.IN_PROGRESS;
        Time.timeScale = 1f;
    }
    public void toggleMusic() {
        if(audioSource.isPlaying)
            audioSource.Pause();
        else audioSource.Play();
    }
    public void updateScoreBy(int delta) {
        mainMenu.showMessage();    
        Invoke("createConsumable",1f);     
    }

    //TODO : adds time penalty on every invalid collision
    public void addTimePenalty() {
        timer += 2f;
    }
    public void createConsumable() {
        Instantiate(
                consumable, 
                new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10,10), 0), 
                Quaternion.identity);
    }
    public int getScore() {
        return remainingItems;
    }
    void Update() {
        if(currentGameState == GameState.IN_PROGRESS) {
            timer += Time.deltaTime;
            timerText.text =  "Time " + ((int)timer).ToString();
        }
    }
}

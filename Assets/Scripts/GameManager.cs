using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelCompleted;
    public static bool isGameStarted;
    public static bool mute = false;


    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    public GameObject gamePlayPanel;
    public GameObject startMenuPanel;

    public static int currentLevelIndex;
    public Slider gameProgressSlider;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;

    public static int numberOfPassedRings;
    public static int score = 0;

    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);

    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        numberOfPassedRings = 0;
        gameOver = false;
        levelCompleted = false;
        isGameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Update our UI
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex+1).ToString();

        //update slider value
        int progress = numberOfPassedRings * 100/FindObjectOfType<HelixManager>().numberOfRings;
        gameProgressSlider.value = progress;

        Debug.Log(score);

        if(Input.GetMouseButtonDown(0) && !isGameStarted)
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            isGameStarted = true;
            gamePlayPanel.SetActive(true);
            startMenuPanel.SetActive(false);

        }

        //Game Over
        if(gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);

            if(Input.GetButtonDown("Fire1"))
            {
                score = 0;
                SceneManager.LoadScene("Level");
            }

        }
        if(levelCompleted)
        {
            
            levelCompletedPanel.SetActive(true);

            if(Input.GetButtonDown("Fire1"))
            {
                PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex + 1);
                SceneManager.LoadScene("Level");
            }

        }
    }
}

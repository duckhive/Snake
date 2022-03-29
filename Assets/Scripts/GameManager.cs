using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Duck duck;
    [SerializeField] private float easy;
    [SerializeField] private float medium;
    [SerializeField] private float hard;
    [SerializeField] private Difficulty difficulty;
    [SerializeField] private bool paused;

        public enum Difficulty
    {
        easy,
        medium,
        hard
    }
    
    public Transform segmentPrefab;
    
    public int score;
    public TMP_Text UIScoreText;

    public GameObject UIGameOverPanel;
    public GameObject uiInstructionsPanel;
    public GameObject uiPausePanel;

    public bool gameActive;
    public bool gameOver;
    public bool readyToContinue;

    public MMFeedbacks eatFoodFeedbacks;
    public MMFeedbacks dieFeedbacks;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        UIGameOverPanel.SetActive(false);
    }

    private void Start()
    {
        ResetState();
    }


    private void Update()
    {
        UIScoreText.text = $"Score: {score}";
        
        if (!gameActive && !gameOver && !readyToContinue && !paused)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Right") || Input.GetButtonDown("Left"))
            {
                StartGame();
                uiInstructionsPanel.SetActive(false);
            }
        }
        
        if(readyToContinue && (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Right") || Input.GetButtonDown("Left")))
            ResumeGame();
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void IncreaseScore()
    {
        score++;
    }

    public void GameOver()
    {
        UIGameOverPanel.SetActive(true);
        gameActive = false;
        gameOver = true;
    }
    
    public void ResetState()
    {
        for (int i = 1; i < duck.segments.Count; i++)
        {
            Destroy(duck.segments[i].gameObject);
        }
        
        duck.segments.Clear();
        duck.segments.Add(duck.transform);

        for (int i = 1; i < duck.initialSize; i++)
        {
            duck.segments.Add(Instantiate(segmentPrefab));
        }
        
        duck.transform.position = Vector3.zero + new Vector3(0, 0.5f, 0);
        UIGameOverPanel.SetActive(false);
        gameOver = false;
        uiInstructionsPanel.SetActive(true);
        PlaylistManager.Instance.PlayRandomTrack();
        UnPauseGame();
    }

    public void StartGame()
    {
        gameActive = true;
        readyToContinue = false;
        ResetScore();
    }
    
    public void ResumeGame()
    {
        gameActive = true;
        readyToContinue = false;
    }

    public void PauseGame()
    {
        uiPausePanel.SetActive(true);
        gameActive = false;
        paused = true;
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        uiPausePanel.SetActive(false);
        paused = false;
        
        if (difficulty == Difficulty.easy)
            Time.timeScale = easy;
        if (difficulty == Difficulty.medium)
            Time.timeScale = medium;
        if (difficulty == Difficulty.hard)
            Time.timeScale = hard;
    }

    public void SetToEasy()
    {
        difficulty = Difficulty.easy;
    }
    
    public void SetToMedium()
    {
        difficulty = Difficulty.medium;
    }
    
    public void SetToHard()
    {
        difficulty = Difficulty.hard;
    }

    public void ContinueGame()
    {
        duck.transform.position = Vector3.zero + new Vector3(0, 0.5f, 0);
        UIGameOverPanel.SetActive(false);
        gameOver = false;
        readyToContinue = true;
        AdsInitializer.Instance.InitializeAds();
    }
    
}

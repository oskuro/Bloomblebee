using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public float ratioForThreeStars = .9f;
    public float ratioForTwoStars = .5f;

    public Image[] ratingImages;

    public TextMeshProUGUI nectarCountText;
    public TextMeshProUGUI secretNectarCountText;
    public Image secretNectarImage;

    public Image nectarCountImage;
    public GameObject goalPanel;
    public TextMeshProUGUI totalCollectedText;
    bool reachedGoal = false;
    Goal goal = null;
    public string nextLevel = "";
    public float nectarBonusTimeDivision = 5f;
    int totalNectarsInScene = 0;
    int totalSecretNectars = 0;

    public GameObject pausMenu = null;

    public static bool usesGamepad = false;

    public Button doneButton = null;

    private int secretNectarCount = 0;
    public int SecretNectarCount { 
        get { return secretNectarCount; }
        set { 
            secretNectarCount = value;
            if(secretNectarCount == 1)
                secretNectarImage.gameObject.SetActive(true);

            UpdateSecretNectarCount();
        } 
    }

    private int nectarCount = 0;
    public int NectarCount
    {
        get
        {
            return nectarCount;
        }
        set
        {
            nectarCount = value;
            UpdateNectarCount();
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
        goal = FindObjectOfType<Goal>();

        if (goal)
            goal.levelFinished += OnLevelFinished;


        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Pickup[] pickups = FindObjectsOfType<Pickup>();
        totalNectarsInScene = pickups.Where(pickup => pickup.secret == false).Count();
        totalSecretNectars = pickups.Where(pickup => pickup.secret == true).Count();

        UpdateNectarCount();

        if (pausMenu)
            pausMenu.GetComponent<MainMenu>().FadeInScene();
    }

    public void OnLevelFinished()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        doneButton.Select();
        reachedGoal = true;
        goalPanel.SetActive(true);
        totalCollectedText.text = $"{nectarCount} out of {totalNectarsInScene} nectars found. \n{secretNectarCount} out of {totalSecretNectars} secret nectars found!";
        CalculateScore();
    }

    private void CalculateScore()
    {
        float score = nectarCount / totalNectarsInScene;
        if (score >= ratioForThreeStars)
        {
            foreach(Image i in ratingImages)
            {
                i.color = Color.white;
            }
        }
        else if (score >= ratioForTwoStars)
        {
            ratingImages[1].color = Color.white;
        }
    
    }

    public void OnDrink()
    {
        //LoadNextLevel();
    }

    private void UpdateNectarCount()
    {
        StartCoroutine(UiUtility.PulseRect(nectarCountImage.rectTransform, new Vector3(1.3f, 1.3f, 1.3f), 6f));
        nectarCountText.text = $"{nectarCount}/{totalNectarsInScene}";
    }

    public void UpdateSecretNectarCount()
    {
        
        StartCoroutine(UiUtility.PulseRect(secretNectarImage.rectTransform, new Vector3(1.3f, 1.3f, 1.3f), 6f));
        secretNectarCountText.text = $"{secretNectarCount}/{totalSecretNectars}";
    }

    public void LoadNextLevel()
    {
        if (reachedGoal)
        {
            if (nextLevel.Length != 0)
                SceneManager.LoadScene(nextLevel);
            else
                SceneManager.LoadScene(0);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        if(goal)
            goal.levelFinished -= OnLevelFinished;
    }
}

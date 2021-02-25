using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] float timeScale = 2f;
    [SerializeField] Image fadeImage = null;
    [SerializeField] GameObject pausMenu = null;
    [SerializeField] Button resumeButton = null;

    [SerializeField] Button tutorialButton = null;

    int loadLevelIndex = 1;
    public void LoadTutorial()
    {
        loadLevelIndex = 2;
        FadeOutScene();
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (fadeImage)
            fadeImage.enabled = false;
        if (pausMenu)
            pausMenu.SetActive(false);

        if (tutorialButton)
            tutorialButton.Select();
    }

    public void OnMenu()
    {
        TogglePausMenu();
    }

    public void LoadLevel(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void Play() {
        loadLevelIndex = 1;
        FadeOutScene();
    }

    public void Exit() {
        Application.Quit();
    }

    public void Resume()
    {

    }

    public void OnPointerEnter(RectTransform rect) {
        StartCoroutine(UiUtility.ScaleRect(rect, new Vector3(1.1f, 1.1f, 1.1f), timeScale));
    }

    public void OnPointerExit(RectTransform rect) {
        StartCoroutine(UiUtility.ScaleRect(rect, new Vector3(1, 1, 1), timeScale));
    }

    public void FadeOutScene()
    {
        fadeImage.enabled = true;
        StartCoroutine(UiUtility.FadeImage(fadeImage, 1f));
    }

    public void FadeInScene()
    {
        fadeImage.enabled = true;
        fadeImage.color = Color.black;
        StartCoroutine(UiUtility.FadeImage(fadeImage, 0f));
    }

    public void TogglePausMenu()
    {
        if (pausMenu == null)
            return;

        pausMenu.SetActive(!pausMenu.activeSelf);

        if (pausMenu.activeSelf)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            resumeButton.Select();
        }
        else
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        if(fadeImage.color.a == 1)
            SceneManager.LoadScene(loadLevelIndex);
    }
}

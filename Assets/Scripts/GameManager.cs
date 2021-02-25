using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private float volume = 0;
    public float Volume
    {
        get { return volume; }
        set { volume = value; }
    }

    [SerializeField]
    private bool invertHorizontal = true;
    public bool InvertHorizontal { get => invertHorizontal; set => invertHorizontal = value; }
    [SerializeField]
    private bool invertVertical = true;
    public bool InvertVertical { get => invertVertical; set => invertVertical = value; }
    [SerializeField]
    private float horizontalSensitivity = 105f;
    public float HorizontalSensitivity { get => horizontalSensitivity; set => horizontalSensitivity = value; }
    [SerializeField]
    private float verticalSensitivity = 75f;
    public float VerticalSensitivity { get => verticalSensitivity; set => verticalSensitivity = value; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        
    }

    //public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    MainMenu mainMenu = FindObjectOfType<MainMenu>();
    //    if (mainMenu)
    //        mainMenu.FadeInScene();
    //}

    //private void OnDestroy()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}
}

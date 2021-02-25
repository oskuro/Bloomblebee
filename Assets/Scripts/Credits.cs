using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public Button returnButton;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (returnButton)
            returnButton.Select();
    }

    public void LoadMain()
    {
        SceneManager.LoadScene(0);
    }
   
}

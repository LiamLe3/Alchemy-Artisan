using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene(1);
    }
}

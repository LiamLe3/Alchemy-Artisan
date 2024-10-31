using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpButtons : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

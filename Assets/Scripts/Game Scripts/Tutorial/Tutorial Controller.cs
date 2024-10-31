using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    const int MainMenu = 0;
    [SerializeField] TutorialText[] tutorialText;
    [SerializeField] TMP_SpriteAsset spriteAsset;

    [SerializeField] TextMeshProUGUI tmpText;
    
    [SerializeField] int tutorialTextIndex = 0;
    [SerializeField] float delay = 2f;

    void Start()
    {
        tmpText.text = tutorialText[tutorialTextIndex].text;
        tmpText.spriteAsset = spriteAsset;
    }

    void NextInstruction()
    {
        tutorialTextIndex++;
        tmpText.text = tutorialText[tutorialTextIndex].text;
    }

    IEnumerator ChangeSceneAfterDelay(int sceneIndex)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneIndex);
    }

    void GoToNextTutorial()
    {
        Scene scene = SceneManager.GetActiveScene();
        StartCoroutine(ChangeSceneAfterDelay(scene.buildIndex + 1));
    }

    public void CheckTutorialObjective(Item item)
    {
        if(tutorialTextIndex == 0 && item is Gem && item.GetId() == 0)
            NextInstruction();
        else if(tutorialTextIndex == 1 && item is Gem && item.GetId() > 0)
            NextInstruction();
        else if(tutorialTextIndex == 2 && item is Gem gem && gem.IsPolished())
            GoToNextTutorial();
        else if(tutorialTextIndex == 3 && item is Potion && item.GetId() < 6)
            NextInstruction();
        else if(tutorialTextIndex == 4 && item is Potion && item.GetId() > 5)
            NextInstruction();
        else if(tutorialTextIndex == 5 && item is NullItem)
            GoToNextTutorial();
        else if(tutorialTextIndex == 6 && item is NullItem)
        {
            NextInstruction();
            StartCoroutine(ChangeSceneAfterDelay(MainMenu));
        }

    }
}

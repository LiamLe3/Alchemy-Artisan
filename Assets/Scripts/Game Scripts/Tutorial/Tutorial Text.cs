using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial Text", menuName = "Tut Text")]
public class TutorialText : ScriptableObject
{
    [TextArea(3, 10)]
    public string text;
}

using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Slider bar;
    [SerializeField] Image fillImage;
    [SerializeField] Image iconImage;

    public void UpdateProgressBar(float value)
    {
        bar.value = value;
    }

    public void SetMaxValue(float value)
    {
        bar.maxValue = value;
        UpdateProgressBar(value);
    }

    public void SetIcon(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }

    public void SetColor(Color color)
    {
        fillImage.color = color;
    }

    public void SetVisible(bool setActive)
    {
        bar.gameObject.SetActive(setActive);
    }
}
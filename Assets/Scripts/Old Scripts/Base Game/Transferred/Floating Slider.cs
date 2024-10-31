using UnityEngine;
using UnityEngine.UI;

public class FloatingSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image fillImage;
    [SerializeField] Image iconImage;

    VisualsManager visualsManager;
    void Awake()
    {
        visualsManager = FindObjectOfType<VisualsManager>();
    }

    public void UpdateSlider(float value)
    {
        slider.value = value;
    }

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetNewUI(int index)
    {
        fillImage.color = visualsManager.GetColor(index);
        iconImage.sprite = visualsManager.GetSprite(index);
    }

    public void SetSliderDisplay(bool setActive)
    {
        slider.gameObject.SetActive(setActive);
    }
}
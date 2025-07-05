using UnityEngine;

public class UISFXController : MonoBehaviour
{
    [SerializeField] private AudioSource clickSFX;
    [SerializeField] private AudioSource primaryButtonSFX;
    [SerializeField] private AudioSource secondaryButtonSFX;

    private void OnEnable()
    {
        SelectableButton.OnButtonClicked += ClickSFX;
        SelectableButton.OnButtonSelected += PrimaryButtonSFX;
        SelectableListItem.OnListChanged += SecondaryButtonSFX;
        SelectableSliderItem.OnSliderModified += SecondaryButtonSFX;
    }

    private void OnDisable()
    {
        SelectableButton.OnButtonClicked -= ClickSFX;
        SelectableButton.OnButtonSelected -= PrimaryButtonSFX;
        SelectableListItem.OnListChanged -= SecondaryButtonSFX;
        SelectableSliderItem.OnSliderModified -= SecondaryButtonSFX;
    }

    private void ClickSFX() => clickSFX.Play();
    private void PrimaryButtonSFX() => primaryButtonSFX.Play();
    private void SecondaryButtonSFX() => secondaryButtonSFX.Play();
}

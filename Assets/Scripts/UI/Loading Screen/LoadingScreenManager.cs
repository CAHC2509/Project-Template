using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;

    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Image loadingBar;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        loadingPanel.SetActive(false);
    }

    public void Show() => loadingPanel.SetActive(true);
    public void Hide() => loadingPanel.SetActive(false);
    public void SetProgress(float value) => loadingBar.fillAmount = value;
}

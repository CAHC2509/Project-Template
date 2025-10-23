using UnityEngine;

public class TimeScaleSetter : MonoBehaviour
{
    [SerializeField] private float desiredTimeScale = 1f;

    [ContextMenu("Change Time Scale")]
    public void ChangeTimeScale()
    {
        Time.timeScale = desiredTimeScale;
    }
}

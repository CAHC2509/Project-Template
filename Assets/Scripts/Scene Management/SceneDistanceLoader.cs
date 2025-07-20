using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneLoaderEntry
{
    public Transform triggerTransform;
    public SceneField sceneToLoad;
}

public class SceneDistanceLoader : MonoBehaviour
{
    [SerializeField] private List<SceneLoaderEntry> sceneLoaderEntries;
    [SerializeField] private Transform player;
    [SerializeField] private float activationDistance;

    private void Update()
    {
        foreach (SceneLoaderEntry entry in sceneLoaderEntries)
        {
            float distance = Vector2.Distance(entry.triggerTransform.position, player.position);
            if (distance <= activationDistance)
                SceneSwapManager.Instance.LoadAdditiveScene(entry.sceneToLoad);
            else
                SceneSwapManager.Instance.UnloadScene(entry.sceneToLoad);
        }
    }
}
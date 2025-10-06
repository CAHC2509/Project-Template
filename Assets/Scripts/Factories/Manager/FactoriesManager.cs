using UnityEngine;

public class FactoriesManager : MonoBehaviour
{
    [SerializeField] private Transform factoriesParent;

    private FactoryBase[] factories;

    private void Awake()
    {
        factories = factoriesParent.GetComponentsInChildren<FactoryBase>();
    }

    public void Initialize()
    {
        foreach (FactoryBase factory in factories)
            factory.Initialize();
    }

    public void Conclude()
    {
        foreach (FactoryBase factory in factories)
            factory.Conclude();
    }
}

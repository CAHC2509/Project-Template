using UnityEngine;

public class SaveSystemFactory
{
    private SaveSystemType saveSystemType;

    public SaveSystemFactory(SaveSystemType saveSystemType)
    {
        this.saveSystemType = saveSystemType;
    }

    public ISaveSystem GetProduct()
    {
        switch (saveSystemType)
        {
            case SaveSystemType.PlayerPrefs:
                return new PlayerPrefsSaveSystem();
            case SaveSystemType.JSON:
                return new JSONSaveSystem();
            default:
                return new JSONSaveSystem();
        }
    }
}

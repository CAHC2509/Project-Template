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
            case SaveSystemType.Local:
                return new LocalSaveSystem();
            default:
                return new LocalSaveSystem();
        }
    }
}

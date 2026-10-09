using System;

[System.Serializable]
public class PassiveItemInstance
{
    public PassiveItemData data;
    public int currentLevel;

    public PassiveItemInstance(PassiveItemData data)
    {
        this.data = data;
        this.currentLevel = 1;
    }

    public bool CanLevelUp()
    {
        return currentLevel < data.maxLevel;
    }

    public void LevelUp()
    {
        if (CanLevelUp()) currentLevel++;
    }
}
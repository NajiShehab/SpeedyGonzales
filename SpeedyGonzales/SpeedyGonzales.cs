using UnityEngine;

public class SpeedyGonzales : Mod
{
    public void Start()
    {
        Debug.Log("Mod SpeedyGonzales has been loaded!");
    }

    public void OnModUnload()
    {
        Debug.Log("Mod SpeedyGonzales has been unloaded!");
    }
}
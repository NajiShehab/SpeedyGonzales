using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpeedyGonzales : Mod
{
    #region Variables
    public static SpeedyGonzales instance;

    public float speed = 2f;
    public static Network_Player player = RAPI.GetLocalPlayer();

    // Console stuff
    public static string modColor = "#4CB0FE";
    public static string modPrefix = "[" + Utils.Colorize("SpeedyGonzales", modColor) + "] ";

    // Misc
    private Semih_Network network = ComponentManager<Semih_Network>.Value;
    private Settings gameSettings = ComponentManager<Settings>.Value;
    #endregion

    public void Start()
    {
        if (instance != null) { throw new Exception("SpeedyGonzales singleton was already set"); }
        instance = this;

        RConsole.Log(modPrefix + "  loaded!");
    }

    public void OnModUnload()
    {
        RConsole.Log(modPrefix + "unloaded!");
        Destroy(gameObject);
    }


    [ConsoleCommand(name: "sprintSpeed", docs: "This make you go zoom.")]
    public static string SprintSpeed(string[] args)
    {
        player.PersonController.sprintSpeed = float.Parse(args[0]);
        return "Sprint speed set to : " + player.PersonController.sprintSpeed;
    }

    [ConsoleCommand(name: "moveSpeed", docs: "This make you go zoom.")]
    public static string MoveSpeed(string[] args)
    {
        player.PersonController.normalSpeed = float.Parse(args[0]);
        return "Move speed set to : " + player.PersonController.normalSpeed;
    }
}

#region Child Classes
public class Utils
{
    private static List<string> positiveBools = new List<string>() { "true", "1", "yes", "y" };
    private static List<string> negativeBools = new List<string>() { "false", "0", "no", "n" };

    #region TypeChecks
    public static bool IsBool(string text)
    {
        text = text.ToLowerInvariant().Replace(" ", "");
        if (!positiveBools.Contains(text) && !negativeBools.Contains(text))
            return false;
        return true;
    }
    public static bool Bool(string text, bool original)
    {
        text = text.ToLowerInvariant().Replace(" ", "");
        if (positiveBools.Contains(text))
            return true;
        if (negativeBools.Contains(text))
            return false;
        return original;
    }
    #endregion

    #region Colorize
    public static string Colorize(string text, string col)
    {
        string s = string.Concat(new string[]
        {
            "<color=",
            col,
            ">",
            text,
            "</color>"
        });
        return s;
    }
    #endregion
}
#endregion

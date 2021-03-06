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

    // Console stuff
    public static string modColor = "#4CB0FE";
    public static string modPrefix = "[" + Utils.Colorize("SpeedyGonzales", modColor) + "] ";

    public static float defaultNormalSpeed = 3f;
    public static float defaultSprintSpeed = 5f;
    #endregion

    public void Start()
    {
        if (instance != null) { throw new Exception("SpeedyGonzales singleton was already set"); }
        instance = this;

        RConsole.Log(modPrefix + "loaded!");
    }

    public void OnModUnload()
    {
        Network_Player player = RAPI.GetLocalPlayer();
        if (player != null) {
            player.PersonController.sprintSpeed = defaultSprintSpeed;
            player.PersonController.normalSpeed = defaultNormalSpeed;
            RConsole.Log("Reverted move speeds back to default.");
        }
        RConsole.Log(modPrefix + "unloaded!");
        Destroy(instance);
    }


    [ConsoleCommand(name: "sprintSpeed", docs: "Alter the sprint speed of character. Default: 5.")]
    public static string SprintSpeed(string[] args)
    {
        Network_Player player = RAPI.GetLocalPlayer();
        if (args.Length == 1) {
            float speed = float.Parse(args[0]);
            if (speed > 0 && speed < 1000) {
                player.PersonController.sprintSpeed = speed;
                return "Sprint speed set to: " + player.PersonController.sprintSpeed;
            }
            return "Enter valid speed (0 < speed < 1000); example: sprintSpeed 50";
        }
        return "Enter speed; example: sprintSpeed 50. Current sprint speed: " + player.PersonController.sprintSpeed.ToString();
    }

    [ConsoleCommand(name: "moveSpeed", docs: "Alter the sprint speed of character. Default: 3.")]
    public static string MoveSpeed(string[] args)
    {
        Network_Player player = RAPI.GetLocalPlayer();
        if (args.Length == 1) {
            float speed = float.Parse(args[0]);
            if (speed > 0 && speed < 1000) {
                player.PersonController.normalSpeed = speed;
                return "Move speed set to: " + player.PersonController.normalSpeed;
            }
            return "Enter valid speed (0 < speed < 1000); example: moveSpeed 50";
        }
        return "Enter speed; example: movespeed 50. Current move speed: " + player.PersonController.normalSpeed.ToString();
    }
}

#region Child Classes
public class Utils
{
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

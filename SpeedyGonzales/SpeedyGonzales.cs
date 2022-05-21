using System;
using UnityEngine;

public class SpeedyGonzales : Mod
{
    #region Variables
    public static SpeedyGonzales instance;

    public static Network_Player player = RAPI.GetLocalPlayer();

    public static float defaultMoveSpeed;
    public static float defautlSprintSpeed;

    // Console stuff
    public static string modColor = "#4CB0FE";
    public static string modPrefix = "[" + Utils.Colorize("SpeedyGonzales", modColor) + "] ";

    #endregion

    public void Start()
    {
        if (instance != null) { throw new Exception("SpeedyGonzales singleton was already set"); }
        instance = this;

        RConsole.Log(modPrefix + "Loaded!");
    }

    public void OnModUnload()
    {
        // Reset default move and sprint speeds
        if (player != null) {
            player.PersonController.normalSpeed = defaultMoveSpeed;
            player.PersonController.sprintSpeed = defautlSprintSpeed;
        }

        RConsole.Log(modPrefix + "Unloaded!");
        Destroy(instance);
    }


    [ConsoleCommand(name: "sprintSpeed", docs: "Alter the sprint speed of character. Default: 5.")]
    public static string SprintSpeed(string[] args)
    {
        if (args.Length == 1) {
            float speed = float.Parse(args[0]);
            
            if (speed > 0 && speed < 1000) {
                if (defautlSprintSpeed == 0.0f) {
                    defautlSprintSpeed = player.PersonController.sprintSpeed;
                }
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
        if (args.Length == 1) {
            float speed = float.Parse(args[0]);
            
            if (speed > 0 && speed < 1000) {
                if (defaultMoveSpeed == 0.0f) {
                    defaultMoveSpeed = player.PersonController.normalSpeed;
                }
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

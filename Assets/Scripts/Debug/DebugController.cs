using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    bool showHelp;

    string input;

    public static DebugCommand HELP;
    public static DebugCommand START_TURN;
    public static DebugCommand END_TURN;
    public static DebugCommand CLEAR;
    public static DebugCommand<int> SPAWN_ALLY;
    public static DebugCommand<int> SPAWN_ENEMY;

    public List<object> commandList;

    Vector2 scroll;

    private void Awake()
    {
        Input.eatKeyPressOnTextFieldFocus = false;

        HELP = new DebugCommand("help", "Show help hint", "help", () =>
        {
            showHelp = true;
        });

        START_TURN = new DebugCommand("start", "starts player turn", "start", () =>
        {
            Gameplay.Singleton.StartTurn();
        });

        END_TURN = new DebugCommand("end", "ends player turn", "end", () =>
        {
            Gameplay.Singleton.EndTurn();
        });

        CLEAR = new DebugCommand("clear", "clears field from units", "clear", () =>
        {
            Gameplay.Singleton.playerUnits.Clear();
            Gameplay.Singleton.enemyUnits.Clear();

            for (int i = 0; i < Gameplay.Singleton.squares.Count; i++)
            {
                Gameplay.Singleton.squares[i].unitOn = null;
            }

            foreach (GameObject child in Gameplay.Singleton.field)
            {
                Destroy(child.gameObject);
            }
        });

        SPAWN_ALLY = new DebugCommand<int>("ally", "spawns an ally", "spawnally <speed>", (x) => 
        {
            Unit unit = new Unit(speed: x);
            PlayerController.Singleton.Summon(unit);
        });

        SPAWN_ENEMY = new DebugCommand<int>("enemy", "spawns an enemy", "spawnenemy <speed>", (x) =>
        {
            Unit unit = new Unit(speed: x);
            PlayerController.Singleton.SummonEnemy(unit);
        });


        //add all commands to list
        commandList = new List<object>
        { 
            HELP,
            SPAWN_ALLY,
            SPAWN_ENEMY,
            START_TURN,
            END_TURN,
            CLEAR,
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        { 
            showConsole = !showConsole;
            input = "";
        }

        if (Input.GetKeyDown("return"))
        {
            if (showConsole)
            {
                HandleInput();
                input = "";
            }
        }
    }

    private void OnGUI()
    {
        if (!showConsole) return;

        float y = 0f;

        //showing help
        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y+5f, Screen.width, 90), scroll, viewport);

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;

                string label = $"{command.commandFormat} - {command.commandDescription}";
                Rect labelRect = new Rect(5, 20*i, viewport.width - 100, 20);

                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();

            y += 100;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        GUI.SetNextControlName("Console");
        GUI.FocusControl("Console");

        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }

    public void HandleInput()
    {
        string[] properties = input.Split(' ');
        try
        {
            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

                if (input.Contains(commandBase.commandId))
                {
                    if (commandList[i] as DebugCommand != null)
                    {
                        (commandList[i] as DebugCommand).Invoke();
                    }
                    else if (commandList[i] as DebugCommand<int> != null)
                    {
                        (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                    }
                    else if (commandList[i] as DebugCommand<int, int> != null)
                    {
                        (commandList[i] as DebugCommand<int, int>).Invoke(int.Parse(properties[1]), int.Parse(properties[2]));
                    }
                }
            }
        }
        catch (Exception e) { }
    }
}

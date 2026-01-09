using UnityEditor;
using UnityEngine;

public class PlayerDevToolsWindow : EditorWindow
{
    PlayerData player;

    [MenuItem("Tools/Player Dev Tools")]
    public static void Open()
    {
        GetWindow<PlayerDevToolsWindow>("Player Dev Tools");
    }

    void OnGUI()
    {
        GUILayout.Label("PLAYER DEV TOOLS", EditorStyles.boldLabel);

        if (GUILayout.Button("Find Player"))
        {
            player = FindObjectOfType<PlayerData>();
        }

        if (player == null)
        {
            EditorGUILayout.HelpBox("PlayerData not found", MessageType.Warning);
            return;
        }

        DrawSaveLoadTools();
        DrawInventoryTools();
        DrawWeaponTools();
    }

    // Save and load !!NOT FINALIZED!!
    void DrawSaveLoadTools()
    {
        EditorGUILayout.Space();
        GUILayout.Label("Save / Load (Single Slot)", EditorStyles.boldLabel);

        if (GUILayout.Button("SAVE PLAYER"))
            PlayerSaveSystem.Save(player);

        if (GUILayout.Button("LOAD PLAYER (OVERWRITE)"))
            PlayerSaveSystem.Load(player);
    }

    // Inventory
    void DrawInventoryTools()
    {
        EditorGUILayout.Space();
        GUILayout.Label("Inventory", EditorStyles.boldLabel);

        DrawCounter("Coins", player.coins,
            () => player.AddCoin(1),
            () => player.RemoveCoin(1));

        DrawCounter("Keys", player.keys,
            () => player.AddKey(1),
            () => player.RemoveKey(1));

        DrawCounter("Crosses", player.crosses,
            () => player.AddCross(1),
            () => player.RemoveCross(1));

        DrawCounter("Batteries", player.batteries,
            () => player.AddBattery(1),
            () => player.RemoveBattery(1));
    }

    void DrawCounter(string label, int value, System.Action add, System.Action remove)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label($"{label}: {value}", GUILayout.Width(140));

        if (GUILayout.Button("+", GUILayout.Width(30))) add();
        if (GUILayout.Button("-", GUILayout.Width(30))) remove();

        EditorGUILayout.EndHorizontal();
    }

    // Tools
    void DrawWeaponTools()
    {
        EditorGUILayout.Space();
        GUILayout.Label("Weapons / Items", EditorStyles.boldLabel);

        DrawToggle("Flashlight",
            player.hasFlashLight,
            player.AddFlashLight,
            player.RemoveFlashLight);

        DrawToggle("Hammer",
            player.hasHammer,
            player.AddHammer,
            player.RemoveHammer);

        DrawToggle("RayGun",
            player.hasrayGun,
            player.AddrayGun,
            player.RemoverayGun);

        DrawToggle("Pistol",
            player.hasPistol,
            player.AddPistol,
            player.RemovePistol);
    }

    void DrawToggle(string label, bool state, System.Action enable, System.Action disable)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(label, GUILayout.Width(140));

        if (!state)
        {
            if (GUILayout.Button("ADD", GUILayout.Width(60)))
                enable();
        }
        else
        {
            if (GUILayout.Button("REMOVE", GUILayout.Width(60)))
                disable();
        }

        EditorGUILayout.EndHorizontal();
    }
}

using System.IO;
using UnityEngine;

// Temporary save logic !!NOT FINISHED!!
public static class PlayerSaveSystem
{
    static string SavePath =>
        Path.Combine(Application.persistentDataPath, "player_dev_save.json");

    // Saving Data (the rest will build upon this)
    public static void Save(PlayerData player)
    {
        PlayerSaveData data = new PlayerSaveData();

        Transform t = player.transform;
        data.position = t.position;
        data.rotation = t.rotation;

        data.keys = player.keys;
        data.coins = player.coins;
        data.crosses = player.crosses;
        data.batteries = player.batteries;

        data.hasFlashLight = player.hasFlashLight;
        data.hasHammer = player.hasHammer;
        data.hasrayGun = player.hasrayGun;
        data.hasPistol = player.hasPistol;

        data.hasVhs1 = player.hasVhs1;
        data.hasVhs2 = player.hasVhs2;
        data.hasVhs3 = player.hasVhs3;

        data.hasMilk = player.hasMilk;
        data.hasHorse = player.hasHorse;
        data.hasMini1Key = player.hasMini1Key;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);

        Debug.Log($"[DEV SAVE] Saved to {SavePath}");
    }

    // Load method for Load button (Will be called in menu too)
    public static void Load(PlayerData player)
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("[DEV SAVE] No save file found");
            return;
        }

        string json = File.ReadAllText(SavePath);
        PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json);

        Transform t = player.transform;
        t.position = data.position;
        t.rotation = data.rotation;

        player.keys = data.keys;
        player.coins = data.coins;
        player.crosses = data.crosses;
        player.batteries = data.batteries;

        player.hasFlashLight = data.hasFlashLight;
        player.hasHammer = data.hasHammer;
        player.hasrayGun = data.hasrayGun;
        player.hasPistol = data.hasPistol;

        player.hasVhs1 = data.hasVhs1;
        player.hasVhs2 = data.hasVhs2;
        player.hasVhs3 = data.hasVhs3;

        player.hasMilk = data.hasMilk;
        player.hasHorse = data.hasHorse;
        player.hasMini1Key = data.hasMini1Key;

        Debug.Log("[DEV SAVE] Loaded & overwritten");
    }
}

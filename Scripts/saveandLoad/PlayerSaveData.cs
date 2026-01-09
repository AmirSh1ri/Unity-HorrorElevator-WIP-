using UnityEngine;

// Temporary save logic !!NOT FINISHED!!
[System.Serializable]
public class PlayerSaveData
{
    // Transform
    public Vector3 position;
    public Quaternion rotation;

    // Inventory
    public int keys;
    public int coins;
    public int crosses;
    public int batteries;

    // Items
    public bool hasFlashLight;
    public bool hasHammer;
    public bool hasrayGun;
    public bool hasPistol;

    // VHS
    public bool hasVhs1;
    public bool hasVhs2;
    public bool hasVhs3;

    // Misc
    public bool hasMilk;
    public bool hasHorse;
    public bool hasMini1Key;
}

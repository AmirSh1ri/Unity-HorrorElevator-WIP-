using UnityEngine;

// Spawning Items for Locker Room
public class ItemSpawnLocker : MonoBehaviour
{
    [Header("Item Array")]
    public GameObject[] items;

    [Header("Spawn Array")]
    public Transform[] spawnPointsLocker;

    [Range(0f, 1f)]
    public float skipChance = 0.25f; // skip chance

    private void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {
        for (int i = 0; i < spawnPointsLocker.Length; i++)
        {
            if (Random.value < skipChance)
                continue; // skip this spawn point

            int randNum = Random.Range(0, items.Length);

            // Instantiate as a child of the spawn point
            GameObject spawned = Instantiate(
                items[randNum],
                spawnPointsLocker[i].position,
                items[randNum].transform.rotation,
                spawnPointsLocker[i]
            );

            spawned.name = items[randNum].name; // Removing Clone from the name (so the logic of the item pickup works)
        }
    }
}

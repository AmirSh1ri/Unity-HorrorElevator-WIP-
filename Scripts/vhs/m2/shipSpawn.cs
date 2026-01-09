using UnityEngine;
using System.Collections;

// VHS MINIGAME 2 ship spawn logic
public class shipSpawn : MonoBehaviour
{
    public GameObject[] shipPrefabs1 = new GameObject[2];
    public GameObject[] shipPrefabs2 = new GameObject[2];

    public Transform posUPship;
    public Transform posDOWNship;

    private bool spawning = true;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (spawning)
        {
            GameObject chosenShip1 = Random.value <= 0.65f ? shipPrefabs1[0] : shipPrefabs1[1];
            GameObject ship1 = Instantiate(chosenShip1, posUPship.position, posUPship.rotation);

            if (chosenShip1 == shipPrefabs1[1])
                ship1.name = "badship";
            else
                ship1.name = chosenShip1.name;

            Animator anim1 = ship1.GetComponent<Animator>();
            if (anim1 != null) anim1.speed = Random.Range(0.4f, 2f);
            Destroy(ship1, 5f);

            yield return new WaitForSeconds(0.3f);

            GameObject chosenShip2 = Random.value <= 0.65f ? shipPrefabs2[0] : shipPrefabs2[1];
            GameObject ship2 = Instantiate(chosenShip2, posDOWNship.position, posDOWNship.rotation);

            if (chosenShip2 == shipPrefabs2[1])
                ship2.name = "badship";
            else
                ship2.name = chosenShip2.name;

            Animator anim2 = ship2.GetComponent<Animator>();
            if (anim2 != null) anim2.speed = Random.Range(0.4f, 2f);
            Destroy(ship2, 5f);

            yield return new WaitForSeconds(0.7f);

            if (Input.GetKeyDown(KeyCode.O))
                spawning = false;
        }
    }
}

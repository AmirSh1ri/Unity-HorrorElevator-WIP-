using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

// Logic for TV room
public class vhsPlayer : MonoBehaviour
{
    private PlayerData playerData;
    private PhoneAction phoneAction;
    private Vector3 originalPosition;
    [SerializeField] private GameObject vhs1;
    [SerializeField] private GameObject vhs2;
    [SerializeField] private GameObject vhs3;
    [SerializeField] private GameObject vhs1Space;
    [SerializeField] private GameObject vhs2Space;
    [SerializeField] private GameObject vhs3Space;
    [SerializeField] private GameObject blueScreen;
    [SerializeField] private Image blackScreen;
    [SerializeField] private Transform player;
    [SerializeField] private Transform vhs1SpawnPoint;
    [SerializeField] private Transform vhs2SpawnPoint;
    [SerializeField] private Transform vhs3SpawnPoint;
    [SerializeField] private GameObject FL;
    [SerializeField] private GameObject Cross;
    public float duration = 1f;
    public PlayerLook playerLook;
    public PlayerMovement playerMovement;
    bool pressed = false;


    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        vhs1.SetActive(false);
        vhs2.SetActive(false);
        vhs2.SetActive(false);
        blueScreen.SetActive(true);
        originalPosition = player.position;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(ReturnToOriginal());
        }
    }

    // Interact with vhs player 
    // Checking which vhs active and calling playvhs Coroutine
    public void Press()
    {
        if (pressed)
        {
            return;
        }
        Animator animator = gameObject.GetComponent<Animator>();
        if (playerData.hasVhs1)
        {
            animator.Play("vhs1in");
            StartCoroutine(playvhs("vhs1"));
        }
        else if (playerData.hasVhs2)
        {
            animator.Play("vhs2in");
            StartCoroutine(playvhs("vhs2"));
        }
        else if (playerData.hasVhs3)
        {
            animator.Play("vhs3in");
            StartCoroutine(playvhs("vhs3"));
        }
        else
        {
            Debug.Log("no vhs have sori");

        }

    }

    // Coroutine to go back to original position
    public IEnumerator ReturnToOriginal()
    {
        pressed = false;
        yield return StartCoroutine(BlackScreen(originalPosition, true));
    }

    // Coroutine to fade the screen
    public IEnumerator BlackScreen(Vector3 targetPosition, bool on)
    {
        playerMovement.enabled = false;
        playerLook.enabled = false;
        if (blackScreen == null) yield break;

        blackScreen.gameObject.SetActive(true);

        float t = 0f;
        Color start = new Color(0, 0, 0, 0);
        Color end = Color.black;
        while (t < duration)
        {
            t += Time.deltaTime;
            blackScreen.color = Color.Lerp(start, end, t / duration);
            yield return null;
        }
        blackScreen.color = end;

        FL.SetActive(on);
        if (playerData.crosses > 0)
        {
            Cross.SetActive(on);
        }

        yield return new WaitForSeconds(0.5f);

        if (player != null) 
        { 
            player.position = targetPosition;
            player.rotation = Quaternion.identity; 
        }

        t = 0f;
    while (t < duration)
    {
        t += Time.deltaTime;
        blackScreen.color = Color.Lerp(end, start, t / duration);
        yield return null;
    }
    blackScreen.color = start;
    blackScreen.gameObject.SetActive(false);
        playerMovement.enabled = true;
        playerLook.enabled = true;
    }

    // Coroutine to change the vhs spawnpoint and area for different minigames
    public IEnumerator playvhs(string vhs)
    {
        pressed = true;
        yield return new WaitForSeconds(2.5f);

        Transform targetSpawn = null;
        GameObject vhsScreen = null;

        switch (vhs)
        {
            case "vhs1":
                vhs1Space.SetActive(true);
                playerData.RemoveVHS("vhs1");
                vhsScreen = vhs1;
                targetSpawn = vhs1SpawnPoint;
                break;
            case "vhs2":
                vhs2Space.SetActive(true);
                playerData.RemoveVHS("vhs2");
                playerData.hasVhs2 = false;
                vhsScreen = vhs2;
                targetSpawn = vhs2SpawnPoint;
                break;
            case "vhs3":
                vhs3Space.SetActive(true);
                playerData.RemoveVHS("vhs3");
                playerData.hasVhs3 = false;
                vhsScreen = vhs3;
                targetSpawn = vhs3SpawnPoint;
                break;
            default:
                blueScreen.SetActive(true);
                yield break;
        }

        vhs1.SetActive(false);
        vhs2.SetActive(false);
        vhs3.SetActive(false);
        blueScreen.SetActive(false);
        vhsScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(BlackScreen(targetSpawn.position, false));


    }
}


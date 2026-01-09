using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Codes for Toilet Room
public class toiletCode : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform toiletPos;
    [SerializeField] private Vector3 rotationOffset;
    [SerializeField] private Transform player;
    [SerializeField] private Image blackScreen;
    [SerializeField] private Animator DoorAnimator;
    [SerializeField] private Animator DoorAnimatorMirror;
    [SerializeField] private GameObject Door;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private Animator EnemyAnim;
    [SerializeField] private ButtonInteract buttonInteract;
    [SerializeField] private Image toiletBar;
    [SerializeField] private GameObject toiletBarTotal;


    [Header("Settings")]
    public float duration = 1f;

    private PlayerMovement playerMovement;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        EnemyAnim = Enemy.GetComponent<Animator>();
    }

    void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    public void ToiletScareCouroutine()
    {
        StartCoroutine(ToiletScare());
    }

    private IEnumerator ToiletScare()
    {
        // Save player state
        if (player != null)
        {
            originalPosition = player.position;
            originalRotation = player.rotation;
            originalScale = player.localScale;
        }

        // Close door if open
        if (DoorAnimator != null &&
            DoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            DoorAnimator.Play("Close");
            DoorAnimatorMirror.Play("Close");
        }

        yield return StartCoroutine(BlackScreen(toiletPos.position, true));

        // Run minigame
        yield return StartCoroutine(ToiletMini());
        yield return StartCoroutine(BlackScreen(originalPosition, false));
    }
    private IEnumerator BlackScreen(Vector3 targetPosition, bool seating)
    {
        if (blackScreen == null) yield break;

        blackScreen.gameObject.SetActive(true);

        float t = 0f;
        Color start = new Color(0, 0, 0, 0);
        Color end = Color.black;

        // Fade IN
        while (t < duration)
        {
            t += Time.deltaTime;
            blackScreen.color = Color.Lerp(start, end, t / duration);
            yield return null;
        }

        blackScreen.color = end;
        yield return new WaitForSeconds(0.5f);

        // Move player
        if (player != null)
        {
            buttonInteract.enabled = !seating;
            playerMovement.enabled = !seating;

            player.localScale = seating
                ? new Vector3(0.1f, 0.1f, 0.1f)
                : originalScale;

            player.position = targetPosition;
            player.rotation = seating
                ? Quaternion.Euler(rotationOffset)
                : originalRotation;
        }

        // Fade OUT
        t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            blackScreen.color = Color.Lerp(end, start, t / duration);
            yield return null;
        }

        blackScreen.color = start;
        blackScreen.gameObject.SetActive(false);
    }

    // Minigame Logic
    private IEnumerator ToiletMini()
    {
        Enemy.SetActive(true);
        yield return new WaitForSeconds(2);
        if (toiletBar == null) yield break;

        toiletBarTotal.SetActive(true);
        toiletBar.fillAmount = 1f;

        float timer = 0f;
        float duration = 5f;

        float drainSpeed = 0.7f;
        float fillSpeed = 1.7f;

        while (timer < duration)
        {
            // Draining the bar
            toiletBar.fillAmount -= drainSpeed * Time.deltaTime;

            if (Input.GetButton("Fire1"))
            {
                toiletBar.fillAmount += fillSpeed * Time.deltaTime;
            }

            toiletBar.fillAmount = Mathf.Clamp01(toiletBar.fillAmount);

            // Failing
            if (toiletBar.fillAmount <= 0f)
            {

                EnemyAnim.Play("Lower");
                yield return new WaitForSeconds(2f);
                Enemy.SetActive(false);
                if (DoorAnimator != null)
                    // Horror animation for Opening the Door
                    DoorAnimator.Play("OpenScare");
                    DoorAnimatorMirror.Play("OpenScare");
                Door.name = "DoorNoDoor";
                break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        toiletBarTotal.SetActive(false);
        yield return new WaitForSeconds(2f);
    }

}

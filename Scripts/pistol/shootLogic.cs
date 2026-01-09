using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

// Logic for Shooting Pistol
public class shootLogic : MonoBehaviour
{
    private Animator animator;
    private int ammo = 7;
    public Transform cameraTransform;
    private int totalAmmo = 21;
    private int magSize = 7;
    public bool isShooting = false;
    private bool isReloading = false;
    private bool hasNoAmmo = false;
    [SerializeField] private bool isCoolDown = false;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private GameObject muzzleFlash;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool triggerPressed = Input.GetMouseButtonDown(0)
                              || Input.GetButtonDown("Trigger")
                              || Input.GetAxis("Trigger") > 0.5f;

        if (triggerPressed && !isShooting && ammo > 0 && !isCoolDown)
        {
            StartCoroutine(Shoot());
            StartCoroutine(CameraShake());
        }
        else if (ammo == 0 && hasNoAmmo)
        {
            StartCoroutine(BrokenGun());
        }

        if ((Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Reload")) &&
            !isReloading && !isShooting && !isCoolDown && totalAmmo > 0 && ammo < magSize)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    // Camera Shake
    private IEnumerator CameraShake(float duration = 0.1f, float magnitude = 0.05f)
    {
        Vector3 originalPos = cameraTransform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cameraTransform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPos;
    }

    private IEnumerator Shoot()
    {
        muzzle.Play();
        StartCoroutine(flash());
        animator.Play("shoot");
        isShooting = true;
        isCoolDown = true;
        yield return new WaitForSeconds(0.1f);
        isShooting = false;
        yield return new WaitForSeconds(0.3f);

        



        ammo--;

        if (ammo > 0)
        {
            animator.Play("defaultafterWakeup");
        }
        else
        {
            hasNoAmmo = true;
            StartCoroutine(BrokenGun());
        }

        isCoolDown = false;
    }

    // Muzzle Flash
    private IEnumerator flash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        muzzleFlash.SetActive(false);
    }

    // When No ammo ( different reload animation )
    private IEnumerator BrokenGun()
    {
        hasNoAmmo = false;
        animator.Play("broken");
        yield return null;
    }

    private IEnumerator Reload()
    {
        isCoolDown = true;
        animator.Play(ammo == 0 ? "reloadNoAmmo" : "reload");

        yield return new WaitForSeconds(ammo == 0 ? 2.5f : 2f);

        int needed = magSize - ammo;
        int toLoad = Mathf.Min(needed, totalAmmo);

        ammo += toLoad;
        totalAmmo -= toLoad;

        animator.Play("defaultafterWakeup");
        isCoolDown = false;
        isReloading = false;
    }
}

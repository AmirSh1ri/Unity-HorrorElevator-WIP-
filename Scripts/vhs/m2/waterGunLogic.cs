using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

// WaterGun Logic for VHS MINIGAME 2
public class WaterGun : MonoBehaviour
{
    private Animator animator;

    public Transform cameraTransform;
    public Material matBody;
    public Material matBody2;
    [SerializeField] private ParticleSystem bullet;
    public bool isShooting = false;
    [SerializeField] private LayerMask shipLayer;
    [SerializeField] private float range = 100f;


    void Start()
    {
        // Data for charging the weapon up
        animator = GetComponent<Animator>();
        matBody.EnableKeyword("_EMISSION");
        matBody2.EnableKeyword("_EMISSION");
        matBody.SetColor("_EmissionColor", Color.red * -0.25f);
        matBody2.SetColor("_EmissionColor", Color.blue * -0.25f);
    }

    void Update()
    {
        bool triggerPressed =
            Input.GetMouseButtonDown(0) ||
            Input.GetButtonDown("Trigger") ||
            Input.GetAxis("Trigger") > 0.5f;

        if (triggerPressed && !isShooting)
        {
            StartCoroutine(Shoot());
            StartCoroutine(CameraShake());
        }
    }
    // Camera shake
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
        isShooting = true;
        // Changing emission
        yield return StartCoroutine(LerpEmission(-0.25f, 1f, 0.7f));

        animator.Play("shoot");
        bullet.Play();
        StartCoroutine(DetectHit());

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(LerpEmission(1f, -0.25f, 0.3f));
        isShooting = false;
    }

    // Coroutine for Lerping emission
    private IEnumerator LerpEmission(float from, float to, float duration)
    {
        float elapsed = 0f;
        Color baseColor = Color.red;
        Color baseColor2 = Color.blue;
        while (elapsed < duration)
        {
            float intensity = Mathf.Lerp(from, to, elapsed / duration);
            matBody.SetColor("_EmissionColor", baseColor * intensity);
            matBody2.SetColor("_EmissionColor", baseColor2 * intensity);
            elapsed += Time.deltaTime;
            yield return null;
        }

        matBody.SetColor("_EmissionColor", baseColor * to);
        matBody2.SetColor("_EmissionColor", baseColor2 * to);
    }

    // Raycast Hit detection
    private IEnumerator DetectHit()
    {
        yield return null;
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, shipLayer))
        {
            Transform hitTransform = hit.collider.transform;
            Transform root = hitTransform.root;

            if (root.name.Contains("badship"))
            {
                Debug.Log("badship");
            }
            else
            {
                Debug.Log("goodship");
            }


            yield return StartCoroutine(rotate(hitTransform));
        }
    }

    // Rotation ships 180 degrees to show being hit
    private IEnumerator rotate(Transform hitTransform)
    {
        for (int i = 0; i < 10; i++)
        {
            hitTransform.Rotate(0f, 180f / 10f, 0f, Space.Self);
            yield return new WaitForSeconds(0.025f);
        }



    }
}

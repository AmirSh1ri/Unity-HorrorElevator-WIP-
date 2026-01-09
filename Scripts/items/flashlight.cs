using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

// Logic used for FlashLight / Camera
public class Flashlight : MonoBehaviour
{


    [SerializeField] private GameObject Flash;
    [SerializeField] private GameObject parent;

    private bool isOn = true;
    private bool canToggle = true;
    private bool isZooming = false;
    private float cooldown = 1.4f;
    private bool isWaking = true;
    private bool isTurnedOn = true;
    void OnEnable()
    {
        Flash.SetActive(false);
        StartCoroutine(isWakingup());
    }

    void Update()
    {
        Debug.Log("LT Axis Value: " + Input.GetAxis("Aim_LT"));
        if (isWaking)
        {
            return;
        }
        if ((Input.GetButton("Aim") || Input.GetAxis("Aim_LT") > 0.1f) && isOn)
        {
            StartCoroutine(ToggleZoom());
        }
        if (Input.GetButtonDown("Tbtn") && canToggle && !isZooming)
        {
            if (!canToggle || isZooming)
                return;
            canToggle = false;
            StartCoroutine (ToggleOfforOn());
        }
    }
    private IEnumerator ToggleZoom()
    {
        Animator animator = parent.GetComponent<Animator>();
        if (isTurnedOn)
        {
            if (isZooming)
            {
                animator.Play("unzoom");
                yield return new WaitForSeconds(1.5f);
                isZooming = false;
                canToggle = true;
                isOn = true;
            }
            else
            {
                animator.Play("zoom");
                canToggle = false;
                yield return new WaitForSeconds(1.3f);
                isZooming = true;
            }
        }

    }
    private IEnumerator ToggleCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canToggle = true;
    }
    private IEnumerator isWakingup()
    {
        yield return new WaitForSeconds(6f);
        isWaking = false;
    }
    private IEnumerator ToggleOfforOn()
    {

        Animator animator = parent.GetComponent<Animator>();
        if (!isZooming)
        {
            isOn = !isOn;
            Flash.SetActive(isOn);
            if (isOn)
            {
                animator.Play("enable");
                yield return new WaitForSeconds(1.3f);
                isTurnedOn = true;
            }
            else
            {
                animator.Play("disabled");
                isTurnedOn = false;

            }

            StartCoroutine(ToggleCooldown());
        }
    }
}

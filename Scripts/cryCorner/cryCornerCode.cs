using System.Collections;
using UnityEngine;
// Room CryCorner
public class cryCornerCode : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject Cross;
    [SerializeField] private GameObject ScarySound;
    [SerializeField] private GameObject cryingLad;
    [SerializeField] private float checkDistance = 2f;
    [SerializeField] private float lookThreshold = 0.7f;

    private bool hasTriggered = false;  

    void Update()
    {
        if (player == null) return;
 
        Vector3 directionToObject = (transform.position - player.position).normalized;
        float distance = Vector3.Distance(player.position, transform.position);
        float dot = Vector3.Dot(player.forward, directionToObject);
 
        if (!hasTriggered && distance < checkDistance && dot > lookThreshold)
        {
            // Look towards enemy -> sound activates behind
            StartCoroutine(disableSound());
            hasTriggered = true;  
        }
 
        if (hasTriggered && dot < lookThreshold)
        {
            // If not looking at enemy enemy gone
            if (cryingLad != null) cryingLad.SetActive(false);
            if (Cross != null) Cross.SetActive(true);
        }
    }
    private IEnumerator disableSound()
    {
        // Couroutine enable -> disable
        ScarySound.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        ScarySound.SetActive(false);
    }
}

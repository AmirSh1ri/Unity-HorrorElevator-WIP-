using UnityEngine;


// Logic for RAYCAST
// for Interacting + raycast Pistol

public class ButtonInteract : MonoBehaviour
{
    public Camera Camera;
    public float maxDistance = 3f;
    private PlayerData playerData;
    public shootLogic shootlogic;
    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
    }

    private void Update()
    {
        Ray raycast = new Ray(Camera.transform.position, Camera.transform.forward);
        RaycastHit hit;

        if (playerData.hasPistol)
        {
            maxDistance = 100f;

            if (Physics.Raycast(raycast, out hit, maxDistance))
            {
                if (shootlogic.isShooting)
                {
                    enemySubway enemy = hit.collider.GetComponent<enemySubway>();
                    if (enemy != null)
                    {
                        enemy.destroy();
                    }
                }
            }
        }
        else if (Physics.Raycast(raycast, out hit, maxDistance))
        {
            if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
            {
                var buttonAction = hit.collider.GetComponent<ButtonAction>();
                if (buttonAction != null)
                {
                    buttonAction.Press();
                    return;
                }

                var phoneAction = hit.collider.GetComponent<PhoneAction>();
                if (phoneAction != null)
                {
                    phoneAction.Press();
                    return;
                }

                var vhsAction = hit.collider.GetComponent<vhsPlayer>();
                if (vhsAction != null)
                {
                    vhsAction.Press();
                    return;
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

// Logic for using the hammer VHS MINIGAME 1
public class HammerLogic : MonoBehaviour
{
    private Animator animator;
    private int attackIndex;
    private bool isAttacking = false;
    public bool coolingDown = false;

    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float rayDistance = 3f;
    [SerializeField] private LayerMask hitLayers = ~0;
    [SerializeField] private GameObject[] Items;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Player;
    private static bool specialItemDropped = false;
    private static int chanceToDropKey = 0;
    


    void Start()
    {
        animator = GetComponent<Animator>();

        if (rayOrigin == null)
            rayOrigin = transform;
    }

    void Update()
    {
        bool lmbPressed = Input.GetButtonDown("LMB");
        float rtValue = Input.GetAxis("RT");
        bool rtPressed = rtValue > 0.5f;
        // Check for Cooldowns before attacking again
        if ((lmbPressed || rtPressed) && !coolingDown && !isAttacking)
        {
            StartCoroutine(Slash());
        }
    }

    // Attack Logic
    private IEnumerator Slash()
    {
        isAttacking = true;
        coolingDown = true;

        attackIndex = Random.Range(0, 2);
        string animName = attackIndex == 0 ? "attack" : "attack2";

        animator.CrossFadeInFixedTime(animName, 0.05f);

        yield return null;

        yield return new WaitForSeconds(1.35f);

        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, hitLayers))
        {
            GameObject hitObject = hit.collider.gameObject;
            Animator objAnimator = hitObject.GetComponent<Animator>();
            if (hitObject.name == "breakableTV" && !objAnimator.GetCurrentAnimatorStateInfo(0).IsName("break"))
            {

                if (objAnimator != null)
                {
                    
                    objAnimator.Play("break");
                    yield return new WaitForSeconds(0.3f);
                    breakTV();
                }   
            }
        }

        yield return new WaitForSeconds(1.35f);

        isAttacking = false;
        coolingDown = false;
    }

    // If TV hit -> breakTV
    public void breakTV()
    {
        Vector3 spawn = Player.transform.position + transform.forward * 6;
        Vector3 spawnEnemy = Player.transform.position - transform.forward * Random.Range(40, 71);
        spawnEnemy.y = -0.602f;


        int j = Random.Range(15, 101);
        int i;
        if (chanceToDropKey >= j && !specialItemDropped)
        {
            i = 2;
            specialItemDropped = true;
        }
        else { i = Random.Range(0, 2); }
        if (!specialItemDropped)
        {
            chanceToDropKey += 20;
        }

        Instantiate(Enemy, spawnEnemy, Quaternion.identity);
        GameObject spawnedItem = Instantiate(Items[i], spawn, Quaternion.identity );
        spawnedItem.name = Items[i].name;
    }
}

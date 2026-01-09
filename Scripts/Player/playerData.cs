using System.Collections;
using TMPro;
using UnityEngine;

// All the Payer Data of Items 
public class PlayerData : MonoBehaviour
{
    public int keys = 0;
    public int coins = 0;
    public int crosses = 0;
    public int batteries = 0;
    private bool CrossShowing = false;
    public bool hasVhs1 = false;
    public bool hasVhs2 = false;
    public bool hasVhs3 = false;
    public bool hasMilk = false;
    public bool hasHorse = false;
    public bool hasMini1Key = false;
    [Header("UI Texts")]
    [SerializeField] private TextMeshProUGUI Cross;
    [SerializeField] private TextMeshProUGUI Batteries;
    [SerializeField] private TextMeshProUGUI Coins;
    [SerializeField] private TextMeshProUGUI Keys;

    [Header("UI Icons with Animators")]
    [SerializeField] private GameObject CrossUI;
    [SerializeField] private GameObject BatteriesUI;
    [SerializeField] private GameObject CoinsUI;
    [SerializeField] private GameObject KeysUI;
    public GameObject vhs1UI;
    public GameObject vhs2UI;
    public GameObject vhs3UI;

    [Header("Flashlight")]
    [SerializeField] private GameObject flashLight;
    public bool hasFlashLight = false;
    private Animator flashLightAnimator;
    [SerializeField] private Flashlight flashLogic;
    private bool FLturnedOnce = false;

    [Header("Hammer")]
    [SerializeField] private GameObject Hammer;
    public bool hasHammer = false;
    private Animator HammerAnimator;
    private bool HMturnedOnce = false;

    [Header("rayGun")]
    [SerializeField] private GameObject rayGun;
    public bool hasrayGun = false;
    private Animator rayGunAnimator;
    private bool rayGunturnedOnce = false;

    [Header("Cross")]
    [SerializeField] private GameObject CrossOBJ;

    [Header("pistol")]
    public bool hasPistol = false;
    [SerializeField] private GameObject pistol;
    [SerializeField] private shootLogic shootLogic;
    private Animator pistolAnimator;


    void Start()
    {
        // Start with no tools
        if (flashLight != null)
        {
            flashLight.SetActive(false);
            flashLightAnimator = flashLight.GetComponent<Animator>();
        }
        if (Hammer != null)
        {
            Hammer.SetActive(false);
            HammerAnimator = Hammer.GetComponent<Animator>();
        }
        if (pistol != null)
        {
            pistol.SetActive(false);
            pistolAnimator = pistol.GetComponent<Animator>();
        }
        if (rayGun != null)
        {
            rayGun.SetActive(false);
            rayGunAnimator = rayGun.GetComponent<Animator>();
        }
        shootLogic.enabled = false;
    }

    void Update()
    {
        if (hasFlashLight && flashLight != null && !flashLight.activeSelf && !FLturnedOnce)
        {
            FLturnedOnce = true;
            flashLight.SetActive(true);
            if (flashLightAnimator != null)
                flashLightAnimator.Play("wakeup");
        }
        if (hasHammer && Hammer != null && !Hammer.activeSelf && !HMturnedOnce)
        {
            HMturnedOnce = true;
            Hammer.SetActive(true);
        }
        if (hasrayGun && rayGun != null && !rayGun.activeSelf && !rayGunturnedOnce)
        {
            rayGunturnedOnce = true;
            rayGun.SetActive(true);
        }
        if (hasPistol && pistol != null && !pistol.activeSelf)
        {
            StartCoroutine(pistolBeingAdded());
        }
        // Debug
        if (Input.GetKeyDown(KeyCode.K))
        {
            RemoveCross(); RemoveBattery();RemoveCoin();RemoveKey();
        }
    }

    // Coroutine to Add pistol while removing flash light
    public IEnumerator pistolBeingAdded()
    {

        if (flashLight.activeSelf)
        {
            if (flashLightAnimator != null)
                flashLightAnimator.Play("disabled");
            yield return new WaitForSeconds(1.3f);
            flashLogic.enabled = false;
        }



        pistol.SetActive(true);
        if (pistolAnimator != null)
            pistolAnimator.Play("wakeup");
        yield return new WaitForSeconds(3.2f);
        shootLogic.enabled = true;
    }

    // Methods to Add Tools and remove tools
    public void AddFlashLight()
    {
        hasFlashLight = true;
    }
    public void RemoveFlashLight()
    {
        hasFlashLight = false;
        flashLight.SetActive(false);
    }
    public void AddHammer()
    {
        hasHammer = true;
    }

    public void RemoveHammer()
    {
        hasHammer = false;
        Hammer.SetActive(false);
    }
    public void AddPistol()
    {
        hasPistol = true;
    }
    public void RemovePistol()
    {
        hasPistol = false;
        pistol.SetActive(false);
    }
    public void AddrayGun()
    {
        hasrayGun = true;
    }

    public void RemoverayGun()
    {
        hasrayGun = false;
        rayGun.SetActive(false);
    }
    public void EnableCross()
    {
        if(crosses > 0 && !CrossShowing)
        {
            CrossShowing = true;
            CrossOBJ.SetActive(true);
        }
    }

    public void AddKey(int amount = 1)
    {
        bool wasZero = (keys == 0);
        keys += amount;
        Keys.text = keys.ToString();

        if (wasZero && keys > 0 && KeysUI != null)
            KeysUI.GetComponent<Animator>()?.Play("wakeup");
    }

    public void RemoveKey(int amount = 1)
    {
        keys -= amount;
        if (keys <= 0)
        {
            keys = 0;
            KeysUI.GetComponent<Animator>()?.Play("remove");
        }
        Keys.text = keys.ToString();
    }

    public void AddCoin(int amount = 1)
    {
        bool wasZero = (coins == 0);
        coins += amount;
        Coins.text = coins.ToString();

        if (wasZero && coins > 0 && CoinsUI != null)
            CoinsUI.GetComponent<Animator>()?.Play("wakeup");
    }

    public void RemoveCoin(int amount = 1)
    {
        coins -= amount;
        if (coins <= 0)
        {
            coins = 0;
            CoinsUI.GetComponent<Animator>()?.Play("remove");
        }
        Coins.text = coins.ToString();
    }

    // Add Cross / HP
    public void AddCross(int amount = 1)
    {
        bool wasZero = (crosses == 0);
        crosses += amount;
        Cross.text = crosses.ToString();

        if (wasZero && crosses > 0 && CrossUI != null)
            CrossUI.GetComponent<Animator>()?.Play("wakeup");
    }

    public void RemoveCross(int amount = 1)
    {
        crosses -= amount;

        if (CrossOBJ != null)
        {
            CrossOBJ.GetComponent<Animator>()?.Play("BREAK");
        }

        if (crosses < 0)
        {
            crosses = 0;
            // death logic here
        }else if(crosses == 0)
        {
            CrossUI.GetComponent<Animator>()?.Play("remove");
            Cross.text = crosses.ToString();
        }
        else
        {
            Cross.text = crosses.ToString();
            if (CrossOBJ != null)
            {
                StartCoroutine(WaitThenWake());
            }
        }
    }
    public void RemoveVHS(string name)
    {
        StartCoroutine(RemoveVHSCoroutine(name));
    }

    private IEnumerator RemoveVHSCoroutine(string name)
    {
        GameObject vhsUI = null;

        switch (name.ToLower())
        {
            case "vhs1":
                vhsUI = vhs1UI;
                break;
            case "vhs2":
                vhsUI = vhs2UI;
                break;
            case "vhs3":
                vhsUI = vhs3UI;
                break;
            default:
                yield break;
        }

        if (vhsUI != null)
        {
            Animator anim = vhsUI.GetComponent<Animator>();
            if (anim != null)
            {
                anim.Play("remove");
            }
        }
        yield return new WaitForSeconds(0.5f);

        switch (name.ToLower())
        {
            case "vhs1":
                hasVhs1 = false;
                break;
            case "vhs2":
                hasVhs2 = false;
                break;
            case "vhs3":
                hasVhs3 = false;
                break;
        }
    }

    private IEnumerator WaitThenWake()
    {
        yield return new WaitForSeconds(1f); // wait 1 second
        CrossOBJ.GetComponent<Animator>()?.Play("wakeup");
    }


    public void AddBattery(int amount = 1)
    {
        bool wasZero = (batteries == 0);
        batteries += amount;
        Batteries.text = batteries.ToString();

        if (wasZero && batteries > 0 && BatteriesUI != null)
            BatteriesUI.GetComponent<Animator>()?.Play("wakeup");
    }

    public void RemoveBattery(int amount = 1)
    {

        batteries -= amount;
        if (batteries <= 0) 
        {
            batteries = 0;
            BatteriesUI.GetComponent<Animator>()?.Play("remove");
        }
        Batteries.text = batteries.ToString();
    }
}

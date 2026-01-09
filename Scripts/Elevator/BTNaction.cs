using UnityEngine;
using System.Collections;
using TMPro;
using System.Linq;

// Code for Interacting Logic for All the Items

public class ButtonAction : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI FloorText;
    [SerializeField] private TextMeshProUGUI FloorNumberText;
    [SerializeField] private Animator animationDoor;
    private PlayerData playerData;
    private HammerLogic hammerLogic;
    private PhoneAction phoneAction;
    private toiletCode toiletCode;
    private hallwayLogic hallwayLogic;
    private vhsPlayer vhsPlayer;
    private int num;
    private bool inProgress;
    private bool doorOpen = false;
    private bool isOpen = false;
    private bool isPickingUp = false;
    private bool phoneButtonCooldown = false;
    private bool drawerIsOpen = false;
    private bool isUnlocking = false;
    private bool fridgeOpen = false;
    private bool hadMilk = false;
    private bool hadHorse = false;

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        phoneAction = FindObjectOfType<PhoneAction>();
        toiletCode = FindAnyObjectByType<toiletCode>();
        hallwayLogic = FindAnyObjectByType<hallwayLogic>();
        hammerLogic = FindAnyObjectByType<HammerLogic>();


    }
    public void Press()
    {
        // Pickable Items
        string[] validNames = { "pistol", "vhs1", "vhs2", "vhs3", "Key", "Batteries", "Cross", "flash", "Coin", "Hammer", "M1Key", "rayGun" };

        // Buttons
        if (gameObject.name == "UP")
        {
            Animator animator = gameObject.GetComponent<Animator>();
            animator.Play(0);

            int FloorNumber = int.Parse(FloorText.text);
            FloorNumber += 1;
            FloorText.text = FloorNumber.ToString();

        }
        else if (gameObject.name == "DOWN")
        {
            Animator animator = gameObject.GetComponent<Animator>();
            animator.Play(0);

            int FloorNumber = int.Parse(FloorText.text);
            FloorNumber -= 1;
            FloorText.text = FloorNumber.ToString();

        }


        // Milk and Horse are parts of a puzzle
        else if (gameObject.name == "hasmilkandhorse")
        {
            Animator animator = gameObject.GetComponent<Animator>();
            animator.Play("click");
            if (hadHorse && hadHorse)
            {
                // Logic for prize
            }


        }
        else if (gameObject.name == "checkHorse" || gameObject.name == "checkMilk")
        {
            Animator animator = gameObject.GetComponent<Animator>();
            switch (gameObject.name)
            {
                case ("checkMilk"):
                {
                        if (playerData.hasMilk)
                        {
                            hadHorse = true;
                            animator.Play("hasMilk");
                            playerData.hasMilk = false;
                        }

                        break;
                }
                case ("checkHorse"):
                {
                        if (playerData.hasHorse)
                        {
                            hadMilk = true;
                            animator.Play("hasHorse");
                            playerData.hasHorse = false;
                        }

                        break;
                }

            }

        }

        // More Buttons with different logics being applied
        else if (gameObject.name == "ACCEPT")
        {
            Animator animator = gameObject.GetComponent<Animator>();
            animator.Play(0);
            if (int.TryParse(FloorNumberText.text, out num))
            {

                StartCoroutine(AcceptLogic());

            }

        }
        else if (gameObject.name == "door")
        {
            StartCoroutine(OpenDoor());
        }
        else if (gameObject.name == "fridge")
        {
            StartCoroutine(OpenDoorFridge());

        }
        else if (gameObject.name == "PhoneButton" && !phoneButtonCooldown)
        {
            StartCoroutine(PhoneButtonPress());
        }
        else if (gameObject.name == "PhoneButtonL" && !phoneButtonCooldown)
        {
            StartCoroutine(PhoneButtonPressL());
        }

        else if (gameObject.name == "Lock")
        {
            Animator animator = gameObject.transform.parent.GetComponent<Animator>();
            if (!isUnlocking)
            {
                isUnlocking = true;
                if (playerData.keys > 0)
                {
                    StartCoroutine(waitThenDefault(true));

                }
                else
                {

                    StartCoroutine(waitThenDefault(false));
                    Debug.Log("not Unlocked");
                }
            }




        }

        // Pickable Loots

        else if (validNames.Contains(gameObject.name) && !isPickingUp)
        {
            isPickingUp = true;

            Animator animator = gameObject.GetComponent<Animator>();



            switch (gameObject.name)
            {
                case "vhs1":
                    if (!(playerData.hasVhs2 || playerData.hasVhs3))
                    {
                        Debug.Log("Picked up vhs1!");
                        animator.Play("pickup");
                        StartCoroutine(PickupDestroy());
                        playerData.hasVhs1 = true;
                        playerData.vhs1UI.SetActive(true);
                    }
                    Debug.Log("Has other vhs");
                    break;
                case "vhs2":
                    if (!(playerData.hasVhs1 || playerData.hasVhs3))
                    {
                        Debug.Log("Picked up vhs2!");
                        animator.Play("pickup");
                        StartCoroutine(PickupDestroy());
                        playerData.hasVhs2 = true;
                        playerData.vhs2UI.SetActive(true);
                    }
                    Debug.Log("Has other vhs");
                    break;
                case "vhs3":
                    if (!(playerData.hasVhs2 || playerData.hasVhs1))
                    {
                        Debug.Log("Picked up vhs1!");
                        animator.Play("pickup");
                        StartCoroutine(PickupDestroy());
                        playerData.hasVhs3 = true;
                        playerData.vhs3UI.SetActive(true);
                    }
                    Debug.Log("Has other vhs");
                    break;
                case "Key":
                    Debug.Log("Picked up a Key!");
                    animator.Play("pickup");
                    StartCoroutine(PickupDestroy());
                    playerData.AddKey();

                    break;

                case "Batteries":
                    Debug.Log("Picked up Batteries!");
                    animator.Play("pickup");
                    StartCoroutine(PickupDestroy());
                    playerData.AddBattery();
                    break;

                case "Cross":
                    Debug.Log("Picked up Cross!");
                    animator.Play("pickup");
                    StartCoroutine(PickupDestroy());
                    playerData.AddCross();
                    break;

                case "Coin":
                    Debug.Log("Picked up Coin!");
                    animator.Play("pickup");
                    StartCoroutine(PickupDestroy());
                    playerData.AddCoin();
                    break;

                case "flash":
                    animator.Play("pickup");
                    StartCoroutine(PickupDestroy());
                    break;

                case "pistol":
                    animator.Play("pickup");
                    StartCoroutine(PickupDestroy());
                    break;

                case "Hammer":
                    animator.Play("pickup");
                    StartCoroutine(PickupDestroy());
                    break;

                case "M1Key":
                    animator.Play("pickup");
                    StartCoroutine(PickupDestroy());
                    break;

                case "rayGun":
                    animator.Play("pickup");
                    StartCoroutine(PickupDestroy());
                    break;

                default:
                    Debug.Log("Unknown item");
                    break;
            }

        }
        else if (gameObject.name == "phone")
        {
            Animator animator = gameObject.GetComponent<Animator>();

            animator.Play("Pickup");
        }
        else if (gameObject.name == "toiletInteract")
        {
            toiletCode.ToiletScareCouroutine();

        }
        else if (gameObject.name == "cabinet")
        {
            StartCoroutine(OpenDrawer());
        }
        else if (gameObject.name == "EXITDOORMINIGAME1")
        {
            if (playerData.hasMini1Key)
            {
                vhsPlayer.ReturnToOriginal();
                playerData.RemoveHammer();
            }
        }


    }

    // Logics for Doors, Pickup destroys, and etc

    public IEnumerator OpenDoorFridge()
    {
        if (fridgeOpen)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            animator.Play("close");
            yield return new WaitForSeconds(2.5f);
            fridgeOpen = false;
        }else if (!fridgeOpen)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            animator.Play("open");
            yield return new WaitForSeconds(2.5f);
            fridgeOpen = true;
        }

    }
    public IEnumerator waitThenDefault(bool hasKey)
    {
        Animator animator = gameObject.transform.parent.GetComponent<Animator>();

        if (animator == null) yield break;
        
        if (hasKey)
        {
            animator.Play("Open");
            yield return new WaitForSeconds(0.3f);
            playerData.RemoveKey();
            yield return new WaitForSeconds(1.7f);
            isUnlocking = false;
            if(gameObject.transform.parent.name == "hallway")
            {
                hallwayLogic.ChangeSize();
            }
        }
        else
        {
            animator.Play("NoKeys");
            yield return new WaitForSeconds(1.7f);
            animator.Play("Default");
            yield return new WaitForSeconds(0.3f);
            isUnlocking = false;
        }
    }

    public IEnumerator OpenDrawer()
    {
        if (drawerIsOpen)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            animator.Play("Close");
            yield return new WaitForSeconds(1.5f);
            drawerIsOpen = false;
        }
        else if (!drawerIsOpen)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            animator.Play("Open");
            yield return new WaitForSeconds(1.5f);
            drawerIsOpen = true;
        }


        yield return new WaitForEndOfFrame();
    }

    private IEnumerator PhoneButtonPress()
    {
        phoneButtonCooldown = true;

        Animator animator = gameObject.GetComponent<Animator>();
        animator.Play("Press");
        phoneAction.answer = 1;
        yield return new WaitForSeconds(1f);
        phoneButtonCooldown = false;
    }
    private IEnumerator PhoneButtonPressL()
    {
        phoneButtonCooldown = true;

        Animator animator = gameObject.GetComponent<Animator>();
        animator.Play("Press");
        phoneAction.answer = 0;
        yield return new WaitForSeconds(1f);
        phoneButtonCooldown = false;
    }

    public IEnumerator PickupDestroy()
    {
        yield return new WaitForSeconds(1f);
        if (gameObject.name == "flash")
        {
            playerData.AddFlashLight();
        } else if (gameObject.name == "Cross")
        {
            playerData.EnableCross();
        } else if (gameObject.name == "pistol")
        {
            playerData.AddPistol();
        } else if (gameObject.name == "Hammer")
        {
            playerData.AddHammer();
        } else if (gameObject.name == "M1Key")
        {
            playerData.hasMini1Key = true;
        }
        else if (gameObject.name == "rayGun")
        {
            playerData.AddrayGun();
        }
        isPickingUp = false;
        Destroy(gameObject);
    }
    public IEnumerator OpenDoor()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        LinkedDoorSimple linked = GetComponent<LinkedDoorSimple>();

        if (!isOpen)
        {
            animator.Play("Open");
            if (linked != null) linked.OpenLinkedDoor(true);
            yield return new WaitForSeconds(1.5f);
            isOpen = true;
        }
        else
        {
            animator.Play("Close");
            if (linked != null) linked.OpenLinkedDoor(false);
            yield return new WaitForSeconds(1.5f);
            isOpen = false;
        }
    }

    // TEMPORARILY LOGICS FOR ELEVATOR MOVING WILL BE CHANGED BECAUSE A CHANGE IN GAME LOOP

    public IEnumerator AcceptLogic()
    {
        if ((num + int.Parse(FloorText.text)) < 11 && (num + int.Parse(FloorText.text)) > -11 && !inProgress)
        {
            yield return new WaitForEndOfFrame();   
            if (inProgress) yield break;
            inProgress = true;
            Debug.Log("Can do");

            if (doorOpen)
            {
                animationDoor.Play("doorClose");
                doorOpen = false;
            }


            StartCoroutine(MoveElevator());
        }
    }
    public IEnumerator MoveElevator()
    {
        yield return new WaitForSeconds(2f);
        while (FloorText.text != "0"){

            yield return new WaitForSeconds(2f);
            // Audio
            if (int.Parse(FloorText.text) > 0)
            {
                FloorText.text = int.Parse(FloorText.text) - 1 + "";
                FloorNumberText.text = int.Parse(FloorNumberText.text) + 1 + "";

            }
            else
            {
                FloorText.text = int.Parse(FloorText.text) + 1 + "";
                FloorNumberText.text = int.Parse(FloorNumberText.text) - 1 + "";
            }
        }
        yield return new WaitForSeconds(2f);
        animationDoor.Play("doorOpen");
        yield return new WaitForSeconds(6f);
        animationDoor.Play("doorClose");
        yield return new WaitForSeconds(6f);
        inProgress = false;



    }
}

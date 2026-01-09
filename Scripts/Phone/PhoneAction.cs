using UnityEngine;
using TMPro;
using System.Collections;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

// Phone pickup for Phone Room
public class PhoneAction : MonoBehaviour
{
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField, TextArea(2, 5)] private string[] lines;
    [SerializeField] private float typingSpeed = 0.03f;
    public int answer = 2;
    private int index = 0;
    private bool isTyping = false;
    private Coroutine typingRoutine;
    private bool firstPickup = true;
    [SerializeField] private GameObject metal;
    [SerializeField] private Animator metalAnimator;
    [SerializeField] private int metalPhase;
    void Start()
    {
        metalAnimator = metal.GetComponent<Animator>();
        metalPhase = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(False());
        }
    }

    public void Press()
    {
        Animator animator = gameObject.GetComponent<Animator>();

        if (!dialogueCanvas.activeSelf &&  gameObject.name == "phone")
        {
            dialogueCanvas.SetActive(true);
            index = 0;
            StartCoroutine(StartDialogueWithDelay());
            animator?.Play("Pickup");
        } else if(!dialogueCanvas.activeSelf && gameObject.name == "RED")
        {

        }
        else if (!dialogueCanvas.activeSelf && gameObject.name == "GREEN")
        {

        }
    }

    private IEnumerator StartDialogueWithDelay()
    {
        if (firstPickup)
        {
            yield return new WaitForSeconds(3f); // Wait for the pickup animation
            firstPickup = false;
        }

        StartDialogue();
    }

    private void StartDialogue()
    {
        dialogueText.text = "";
        if(index == 9 || index == 12 || index == 15 || index == 18 || index == 23)
        {
            Debug.Log("wait for answer");
            StartCoroutine(WaitForAnswer());

        }
        else
        {
            typingRoutine = StartCoroutine(TypeLine(lines[index]));
        }
        
       
    }
    IEnumerator False()
    {
        yield return null;
        metalPhase++; // 0 -> 1
        metalAnimator.Play(metalPhase.ToString()); // plays 1 then 2 so on

    }
    private IEnumerator WaitForAnswer()
    {
        // wait until player picks red or green
        while (answer == 2)
        {
            yield return null;
        }

        string response = "";

        if (index == 9) //Ready?
        {
            if (answer == 1)
                response = "Great! Let's start then..."; // true
            else
            {
                StartCoroutine(False());                
                response = "Wrong answer! We'll start anyway."; // false
            }


            // call move
        }
        else if (index == 12) // You left him?
        {
            if (answer == 1)
                response = "Good start..."; // true
            else
            {
                response = "WRONG! Don't try to lie to me, I'm.. let's say, aware."; // false
            StartCoroutine(False());
            }

            //call move
        }
        else if (index == 15) // is ur dad looking for you too?
        {
            if (answer == 1)
            {
                response = "That's sad.."; // false
                StartCoroutine(False());
            }

            //call move
            else
                response = "That's sad.."; // true
        }
        else if (index == 18) // been using too many pills?
        {
            if (answer == 1) //yes
                //if(pills>5)
                response = "Truth";
            //else response = Lie
            else {
            response = "Truth111";
            StartCoroutine(False());
            } //no
                //if(pills<5){}

            //else response = Lie
        }
        else if (index == 23) // how u like it so far
        {
            if (answer == 1)
                response = "THANK YOU, I knew it couldn't be that bad, I'm god afterall.";
            else
            {
                response = "Disapproving god's work? daring aren't we.";
                StartCoroutine(False());
            }

        }


        dialogueText.text = "";
        yield return StartCoroutine(TypeLine(response));
        answer = 2;
        yield return new WaitForSeconds(1.5f);
    }



    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        yield return new WaitForSeconds(1f);

        // automatically go to next line
        index++;
        if (index < lines.Length)
        {
            StartDialogue();
        }
        else
        {
            dialogueCanvas.SetActive(false); // end of dialogue
            Animator animator = gameObject.GetComponent<Animator>();
            animator.Play("hangUp");
        }
    }
}

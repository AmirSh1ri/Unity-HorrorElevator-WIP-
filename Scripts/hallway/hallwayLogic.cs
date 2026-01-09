using UnityEngine;
// Room Hallway
public class hallwayLogic : MonoBehaviour
{
    [SerializeField] private GameObject firstpartHallway;
    [SerializeField] private GameObject secondpartHallway;
    [SerializeField] private HallEnemy hallEnemy;

    public void ChangeSize()
    {
        if (firstpartHallway != null)
            firstpartHallway.SetActive(false);

        if (secondpartHallway != null)
        {
            Animator anim = secondpartHallway.GetComponent<Animator>();
            if (anim != null)
                anim.Play("changeSize");
            hallEnemy.Move();
        }

    }
}

using UnityEngine;
using TMPro;

// Matches floor Number with UI for floor number at all times
public class FloorGenerator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI floorText;
    [SerializeField] private int floor;

    void Update()
    {
        int.TryParse(floorText.text, out floor);
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    GameManager gm;  // Reference to the GameManager for managing button interactions

    // OnEnable is called when the script is enabled
    void OnEnable()
    {
        // Pass the button component to the GameManager for further handling
        gm.GetButton(gameObject.GetComponent<Button>());
    }
}
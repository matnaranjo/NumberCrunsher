using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    GameManager gm;

    // Update is called once per frame
    void OnEnable()
    {
        gm.GetButton(gameObject.GetComponent<Button>());
    }
}

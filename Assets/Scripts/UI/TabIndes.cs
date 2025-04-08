using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TabIndes : MonoBehaviour
{
    [SerializeField] Button submit;

    private List<Selectable> tabElements = new List<Selectable>();
    private int inputSelected = 0;

    private void Awake()
    {
        // Get all TMP_InputFields in children
        TMP_InputField[] inputFields = GetComponentsInChildren<TMP_InputField>(includeInactive: true);

        foreach (var field in inputFields)
        {
            tabElements.Add(field);
        }

        tabElements.Add(submit);
    }

    private void Update()
    {
        if (tabElements.Count == 0) return;

        // First-time focus if nothing is selected
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            inputSelected = GetNextValidIndex(-1,1); // Start from 0 going forward
            SelectElement();
            return;
        }

        // Shift + Tab (go backward)
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            inputSelected = GetNextValidIndex(inputSelected, -1);
            SelectElement();
        }
        // Tab (go forward)
        else if (Input.GetKeyDown(KeyCode.Tab) && !Input.GetKey(KeyCode.LeftShift))
        {
            inputSelected = GetNextValidIndex(inputSelected, 1);
            SelectElement();
        }
    }

    private int GetNextValidIndex(int startIndex, int direction)
    {
        int index = startIndex;
        int attempts = 0;

        do
        {
            index += direction;

            if (index >= tabElements.Count) index = 0;
            if (index < 0) index = tabElements.Count - 1;

            attempts++;

            // Break if we checked all elements
            if (attempts > tabElements.Count) break;

        } while (!tabElements[index].IsInteractable() || !tabElements[index].gameObject.activeInHierarchy);

        return index;
    }

    private void SelectElement()
    {
        var element = tabElements[inputSelected];
        element.Select();

        if (element is TMP_InputField inputField)
        {
            inputField.ActivateInputField();
        }
    }
}

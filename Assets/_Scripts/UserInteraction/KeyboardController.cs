using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRKeys;

public class KeyboardController : MonoBehaviour
{

    public Keyboard keyboard;

    private bool kbOn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnableKeyboard()
    {
        keyboard.Enable();
        keyboard.SetPlaceholderMessage("Please enter your email address");

        keyboard.OnUpdate.AddListener(HandleUpdate);
        keyboard.OnSubmit.AddListener(HandleSubmit);
        keyboard.OnCancel.AddListener(HandleCancel);
    }

    private void DisableKeyboard()
    {
        keyboard.OnUpdate.RemoveListener(HandleUpdate);
        keyboard.OnSubmit.RemoveListener(HandleSubmit);
        keyboard.OnCancel.RemoveListener(HandleCancel);

        keyboard.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            kbOn = !kbOn;
        }

        if (kbOn)
            EnableKeyboard();
        else
            DisableKeyboard();
    }

    /// <summary>
    /// Hide the validation message on update. Connect this to OnUpdate.
    /// </summary>
    public void HandleUpdate(string text)
    {
        keyboard.HideValidationMessage();
    }

    /// <summary>
    /// Validate the email and simulate a form submission. Connect this to OnSubmit.
    /// </summary>
    public void HandleSubmit(string text)
    {
        keyboard.DisableInput();

    }

    public void HandleCancel()
    {
        Debug.Log("Cancelled keyboard input!");
    }
}

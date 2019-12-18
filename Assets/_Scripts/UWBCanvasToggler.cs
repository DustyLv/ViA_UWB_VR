using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(CanvasGroup))]
public class UWBCanvasToggler : MonoBehaviour
{
    public bool startHidden = true;
    [SerializeField]
    private CanvasGroup cg;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        cg = GetComponent<CanvasGroup>();
        if (startHidden)
        {
            cg.alpha = 0f;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
        else
        {
            cg.alpha = 1f;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }
    }

    public void ToggleCanvas(bool show)
    {
        if (!show)
        {
            cg.alpha = 0f;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
        else
        {
            cg.alpha = 1f;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }
    }

    public void ToggleCanvas()
    {
        cg.alpha = 1f - cg.alpha; // if alpha=1 => (1-1) alpha=0, if alpha=0 => (1-0) alpha=1
        if(cg.alpha > 0f)
        {
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }
        else
        {
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
    }
}


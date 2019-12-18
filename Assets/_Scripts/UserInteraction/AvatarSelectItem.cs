using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSelectItem : MonoBehaviour {
    private GameObject avatarObject;
    private Button btn;
    // public string avatarLabel;

    // Start is called before the first frame update
    void Start () {
        // SetupAvatarItem();
        
    }

    // Update is called once per frame
    void Update () {

    }

    public void SetupAvatarItem (AvatarItem newAvatarData) {
        btn = transform.GetComponent<Button>();
        RawImage thumbnail = transform.GetChild (0).GetComponent<RawImage> ();
        TextMeshProUGUI label = transform.GetChild (1).GetComponent<TextMeshProUGUI> ();
        label.text = newAvatarData.label;
        avatarObject = newAvatarData.avatar;
        thumbnail.texture = newAvatarData.thumbnail;
        btn.onClick.AddListener(OnSelected);
    }

    public void OnSelected () {
        if (UWBObjectManager.i.activeSelectedUWBObject != null)
            UWBObjectManager.i.activeSelectedUWBObject.ChangeObjectAvatar (avatarObject);
    }
}
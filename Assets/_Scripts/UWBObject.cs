using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UWBObject : MonoBehaviour {
    #region Variables
    [Header ("Sensor Data")]
    public string UWBAddress;
    //public SensorDataRaw data; // there is a thing in ReceiveData to uncomment aswell

    [Header ("Position Override Settings")]
    public bool overridePosX = false;
    [SerializeField]
    private float overridePosXValue = 0f;
    public float OverridePosXValue {
        get { return overridePosXValue; }
        set {
            overridePosXValue = value;
            posX = overridePosXValue;
            UpdateObjectPosition ();
        }
    }

    [Space (10)]
    public bool overridePosY = false;
    [SerializeField]
    private float overridePosYValue = 0f;
    public float OverridePosYValue {
        get { return overridePosYValue; }
        set {
            overridePosYValue = value;
            posY = overridePosYValue;
            UpdateObjectPosition ();
        }
    }

    [Space (10)]
    public bool overridePosZ = false;
    [SerializeField]
    private float overridePosZValue = 0f;
    public float OverridePosZValue {
        get { return overridePosZValue; }
        set {
            overridePosZValue = value;
            posZ = overridePosXValue;
            UpdateObjectPosition ();
        }
    }

    [Header ("Rotation Override Settings")]
    public bool allowRotation = false;

    [Header ("Position Invert Settings")]
    [SerializeField]
    private bool invertPosX = false;
    public bool InvertPosX {
        get { return invertPosX; }
        set {
            invertPosX = value;
            UpdateObjectPosition ();
        }
    }

    [SerializeField]
    private bool invertPosY = false;
    public bool InvertPosY {
        get { return invertPosY; }
        set {
            invertPosY = value;
            UpdateObjectPosition ();
        }
    }

    [SerializeField]
    private bool invertPosZ = true;
    public bool InvertPosZ {
        get { return invertPosZ; }
        set {
            invertPosZ = value;
            UpdateObjectPosition ();
        }
    }

    [Header ("Position Offset Settings")]
    [SerializeField]
    private float offsetPosX = 0f;
    public float OffsetPosX {
        get { return offsetPosX; }
        set {
            offsetPosX = value;
            UpdateObjectPosition ();
        }
    }

    [SerializeField]
    private float offsetPosY = 0f;
    public float OffsetPosY {
        get { return offsetPosY; }
        set {
            offsetPosY = value;
            UpdateObjectPosition ();
        }
    }

    [SerializeField]
    private float offsetPosZ = 0f;
    public float OffsetPosZ {
        get { return offsetPosZ; }
        set {
            offsetPosZ = value;
            UpdateObjectPosition ();
        }
    }

    // These float values are used for storing XYZ position values read from tag, then use the in constructing the position Vec3
    float posX = 0f;
    float posY = 0f;
    float posZ = 0f;

    // Initial rotation of this object
    Quaternion initialObjectRotation;

    // Initial rotation of the tag
    Quaternion initialIMURotation;
    bool imuInitialized = false;

    [Header ("Other Settings")]
    // Used in the tween to control how fast it will tween between values (in position and rotation)
    public float smoothMoveSpeed = 0.05f;

    public enum UWBButtonAction { SwitchTypes, Record };
    [SerializeField]
    private UWBButtonAction buttonAction;
    public UWBButtonAction ButtonAction {
        get { return buttonAction; }
        set {
        buttonAction = value;
        }
    }

    private GameObject CurrentUWBObject;
    private JSONRecorder recorder;

    #endregion

    private void Start () {
        initialObjectRotation = transform.rotation;
        recorder = GameObject.FindObjectOfType<JSONRecorder> ();

        ChangeObjectAvatar(GlobalVariables.i.DEFAULT_AVATAR_PREFAB.avatar);
    }

    public void ReceiveData (SensorDataRaw _data) {
        #region Maybe needed
        //data = _data;
        #endregion

        foreach (Datastream ds in _data.datastreams) {
            // Remove any whitespace, we don't need those
            var val = string.Concat (ds.current_value.Where (c => !char.IsWhiteSpace (c)));

            // If user button is pressed, do something
            // if (ds.id == "user_button") { OnButtonPressed(); break; }

            // this goes on Unity x axis
            if (ds.id == "posX" && !overridePosX) {
                float.TryParse (val, out posX);
            } else if (ds.id == "posX" && overridePosX) {
                posX = overridePosXValue;
            }

            // this goes on Unity z axis 
            if (ds.id == "posY" && !overridePosZ) {
                float.TryParse (val, out posZ);
            } else if (ds.id == "posY" && overridePosZ) {
                posZ = overridePosZValue;
            }

            // this is height, but it's weird now and doesn't really work, so just set it to static value
            if (ds.id == "posZ" && !overridePosY) {
                float.TryParse (val, out posY);
            } else if (ds.id == "posZ" && overridePosY) {
                posY = overridePosYValue;
            }

            UpdateObjectPosition ();

            // Create quaternion from string and update rotation with it
            // Need to figure out if sensor quaternions are the same as unity, because rotations seem weird
            if (ds.id == "quaternion" && allowRotation) {
                Quaternion rot = ExtensionMethods.StringToQuaternion (val);
                Vector3 eulerRot = QuaternionToEuler (rot);
                UpdateObjectRotation (eulerRot);
                break;
            } else if (ds.id == "quaternion" && !allowRotation) {
                Quaternion rot = Quaternion.identity;
                UpdateObjectRotation (rot);
            }
        }
    }

    Vector3 QuaternionToEuler (Quaternion _rot) {
        Vector3 eul = _rot.eulerAngles;
        return eul;
    }

    bool IsZero (float val) {
        if (val > 0f || val < 0f) {
            return false;
        } else {
            return true;
        }
    }

    private void UpdateObjectPosition () {
        Vector3 position = new Vector3 (posX * Invert (invertPosX) + offsetPosX, posY * Invert (invertPosY) + offsetPosY, posZ * Invert (invertPosZ) + offsetPosZ);
        DOTween.Kill (transform);
        transform.DOMove (position, smoothMoveSpeed).SetEase (Ease.Linear);
        // transform.position = position;
    }

    private void UpdateObjectRotation (Quaternion targetRotation) {
        // This doesn't seem to work. The IMU tag quaternions doesn't go on the same axis as in Unity (as far as I understand - tag's forward is Y and up is Z, atleast the colors of axis from the image on their site - https://www.sewio.net/product/li-ion-tag-imu-tdoa/)
        // Use the function that takes in a Vec3

        // Some sources suggest using the inverse of the quaternion, and some other calculations described here - https://forum.unity.com/threads/imu-quaternion-to-cube-rotation.531880/
        targetRotation = Quaternion.Inverse (targetRotation);
        transform.DORotateQuaternion (targetRotation, smoothMoveSpeed).SetEase (Ease.Linear);
    }
    private void UpdateObjectRotation (Vector3 targetRotation) {
        // A little bit of hardcoding, which might mean that something later on might not work as intended, but now the rotations seem correct (if the model is positioned correctly - forward is facing +Z, up is +Y, right is +X)
        Vector3 rot = new Vector3 (-targetRotation.z, targetRotation.x, targetRotation.y);
        // transform.rotation = Quaternion.Euler(rot);
        transform.DORotate (rot, smoothMoveSpeed).SetEase (Ease.Linear);

    }

    private void OnButtonPressed () {
        switch (buttonAction) {
            //case UWBButtonAction.SwitchTypes:
            //    ChangeObjectType ();
            //    break;
            case UWBButtonAction.Record:
                ToggleRecord ();
                break;
        }
    }

    private void ToggleRecord () {
        recorder.Record = !recorder.Record;
    }

    public void ChangeObjectAvatar (GameObject newAvatar) {

        // FIX THE DESTROY!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        foreach (Transform child in transform) {
            GameObject.Destroy (CurrentUWBObject);
        }
        CurrentUWBObject = Instantiate (newAvatar, transform);
    }

    private float Invert (bool axis) {
        if (axis) {
            return -1f;
        } else {
            return 1f;
        }
    }
}
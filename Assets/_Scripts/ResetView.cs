using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR;
using UnityEngine.XR;

public class ResetView : MonoBehaviour {

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown (KeyCode.R)) {
            ResetViewAction ();
        }
    }

    void ResetViewAction () {
        // Valve.VR.OpenVR.System.ResetSeatedZeroPose ();
        // Valve.VR.OpenVR.Compositor.SetTrackingSpace (Valve.VR.ETrackingUniverseOrigin.TrackingUniverseSeated);
        // SteamVR.instance.hmd.ResetSeatedZeroPose ();
        // SteamVR.instance.hmd.ResetSeatedZeroPose

        XRDevice.SetTrackingSpaceType(TrackingSpaceType.Stationary);
    }
}
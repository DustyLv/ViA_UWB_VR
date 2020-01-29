﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UWBServer_Client;
public class ClientAutoConnect : MonoBehaviour
{
    void Start()
    {
        ConnectToServer();
    }

    public void ConnectToServer()
    {
        print("Connecting to server.");
        Client.instance.ConnectToServer();
        
    }
}

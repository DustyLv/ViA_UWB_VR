using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Jobs;

public class UDPReceiver : MonoBehaviour
{
    public int port = 5300;
    static readonly object lockObject = new object();
    public string returnData = "";
    public bool processData = false;
    Thread thread;
    UdpClient udp;

    public JSONRecorder recorder;

    JSONWorker jsonWorker;

    /// <summary>
    /// Gets reference for jsonWorker variable from the gameobject. Creates and starts a new thread for receiving data from the server.
    /// </summary>
    void Start()
    {
        jsonWorker = GetComponent<JSONWorker>();
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
    }

    /// <summary>
    /// Continually check if new JSON data is received and sends it to JSONWorker to be processed. Controls raw JSON data recording state.
    /// </summary>
    void FixedUpdate()
    {
        if (processData)
        {
            /*lock object to make sure there data is 
             *not being accessed from multiple threads at thesame time*/
            lock (lockObject)
            {
                if (recorder.Record)
                {
                    recorder.ReceiveAndProcess(returnData);
                }
                jsonWorker.JsonToObject(returnData);
                processData = false;
                //Reset it for next read(OPTIONAL)
                returnData = "";
                

            }
        }
    }

    /// <summary>
    /// Creates a new UDP client object. Receives raw JSON data from server and stores it in a variable.
    /// </summary>
    private void ThreadMethod()
    {
        udp = new UdpClient(port);
        if(udp != null)
        {
            Debug.Log("UDP connection established");
        }
        while (true)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            byte[] receiveBytes = udp.Receive(ref RemoteIpEndPoint);

            /*lock object to make sure there data is 
            *not being accessed from multiple threads at thesame time*/
            lock (lockObject)
            {
                returnData = Encoding.ASCII.GetString(receiveBytes);

                //Debug.Log(returnData);
                if (returnData != "")
                {
                    //print(returnData);
                    //Done, notify the Update function
                    processData = true;
                }
            }
        }
    }

}
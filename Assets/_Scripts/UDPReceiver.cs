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

    void Start()
    {
        jsonWorker = GetComponent<JSONWorker>();
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
    }

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

    private void ThreadMethod()
    {
        udp = new UdpClient(port);
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
                    print(returnData);
                    //Done, notify the Update function
                    processData = true;
                }
            }
        }
    }

}
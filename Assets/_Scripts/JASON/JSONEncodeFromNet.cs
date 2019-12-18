using UnityEngine;
using LitJson;
using System;
using System.Collections;
using System.Net;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Text;
using System.IO;

public class Data
{
    public ArrayList id = new ArrayList();
    public ArrayList address = new ArrayList();
    public string[,,] datastream = new string[1, 8, 3];
}

[System.Serializable]
public class PosData
{
    public int id;
    public string address;
    public SensData datastream = new SensData();

    public struct SensData
    {
        public string id;
        public string current_value;
        public string at;
    }
}

public class JSONEncodeFromNet : MonoBehaviour
{
    public string gameDataFileName = "extractedJson.json";
    public List<PosData> data;

    Data jsonData = new Data();

    private const int listenPort = 10000;

    private string jsonString;
    private JsonData itemData;

    private string[] acc = new string[3];
    private string[] gyro = new string[3];
    private string[] mag = new string[3];

    //movement movementScript;

    // Assignment of Cube's movement script.
    void Start()
    {
        //movementScript = this.gameObject.GetComponent<movement>();
        LoadData();
    }

    // waiting for the cube to the take its position, after that it can be take new values from new udp packet.
    void Update()
    {
        //if (movement.Status == true)
        //{
            //try
            //{
            //    JsonPacket(GetServerData());
            //}
            //catch (Exception)
            //{
            //    //Debug.Log("Udp Packet Waiting..");
            //}
        //}
    }

    private void LoadData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            //PosData loadedData = JsonUtility.FromJson<PosData>(dataAsJson);
            LitJson.JsonData loadedData = new LitJson.JsonData();
            loadedData = LitJson.JsonMapper.ToObject(dataAsJson);

            // Retrieve the allRoundData property of loadedData
            for(int i = 0; i<loadedData.Count; i++)
            {
                PosData d = new PosData();
                d.id = (int)loadedData[i][0];
                d.address = loadedData[i][1].ToString();
                d.datastream.id = loadedData[i][2][0].ToString();
                d.datastream.current_value = loadedData[i][2][1].ToString();
                d.datastream.at = loadedData[i][2][2].ToString();


                data.Add(d);
            }

            foreach (PosData d in data)
            {
                print(String.Format("{0}, {1}, val id {2}, val {3}", d.id, d.address, d.datastream.id, d.datastream.current_value));
            }
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

    // Getting Data From Udp Packet
    string GetServerData()
    {
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            string received_data;
            byte[] receive_byte_array;

            try
            {
                receive_byte_array = listener.Receive(ref groupEP);
                received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);
                //movement.Connected = true;
                return received_data;
            }
            catch (WebException)
            {
                Debug.Log("<color=red>Connection_Failed</color>");
                //movement.Connected = false;
            }

            return "";
    }

        // Deserializing of udp packet as a json.
    void JsonPacket(string data)
    {
        Debug.Log(data);
        JsonData matris_jsonvale = JsonMapper.ToObject(data);

        Debug.Log("<color=blue> <------ Array ------> </color>");

        for (int i = 0; i < 8; i++)
        {
            Debug.Log("<color=red> <--- Block ---> </color>");
            Debug.Log(matris_jsonvale[2][i][0].ToString());
            Debug.Log(matris_jsonvale[2][i][1].ToString());
            Debug.Log(matris_jsonvale[2][i][2].ToString());

            jsonData.datastream[0, i, 0] = matris_jsonvale[2][i][0].ToString();
            jsonData.datastream[0, i, 1] = matris_jsonvale[2][i][1].ToString();
            jsonData.datastream[0, i, 2] = matris_jsonvale[2][i][2].ToString();

        }


        //if (movement.Status == true)
        //{
        Split();
        Assignment();
        //}

    }

    // Splitting Variables From Converted json Data
    void Split()
    {

        Debug.Log("<color=yellow>" + jsonData.datastream[0, 5, 1].ToString() + "</color>");
        Debug.Log("<color=yellow>" + jsonData.datastream[0, 6, 1].ToString() + "</color>");
        Debug.Log("<color=yellow>" + jsonData.datastream[0, 7, 1].ToString() + "</color>");

        acc = jsonData.datastream[0, 5, 1].Split(';');
        Debug.Log("<color=yellow> Splitted Data :  </color>" + acc[0] + " " + acc[1] + " " + acc[2]);

        gyro = jsonData.datastream[0, 6, 1].Split(';');
        Debug.Log("<color=yellow> Splitted Data :  </color>" + gyro[0] + " " + gyro[1] + " " + gyro[2]);

        mag = jsonData.datastream[0, 7, 1].Split(';');
        Debug.Log("<color=yellow> Splitted Data :  </color>" + mag[0] + " " + mag[1] + " " + mag[2]);
    }

    // Assigning new variables to the cube for new destination.
    void Assignment()
    {

        //movementScript.xPosition_Value = double.Parse(jsonData.datastream[0, 0, 1]);
        //movementScript.yPosition_Value = double.Parse(jsonData.datastream[0, 1, 1]);
        //movementScript.zPosition_Value = double.Parse(jsonData.datastream[0, 2, 1]);

        double xPosition_Value = double.Parse(jsonData.datastream[0, 0, 1]);
        double yPosition_Value = double.Parse(jsonData.datastream[0, 1, 1]);
        double zPosition_Value = double.Parse(jsonData.datastream[0, 2, 1]);

        //movementScript.xPositionLoop = true;
        //movementScript.yPositionLoop = true;
        //movementScript.zPositionLoop = true;

        //movementScript.accData[0] = float.Parse(acc[0]);
        //movementScript.accData[1] = float.Parse(acc[1]);
        //movementScript.accData[2] = float.Parse(acc[2]);

        //movementScript.gyroData[0] = float.Parse(gyro[0]);
        //movementScript.gyroData[1] = float.Parse(gyro[1]);
        //movementScript.gyroData[2] = float.Parse(gyro[2]);

        //movementScript.magData[0] = float.Parse(mag[0]);
        //movementScript.magData[1] = float.Parse(mag[1]);
        //movementScript.magData[2] = float.Parse(mag[2]);

        //movementScript.magLoop[0] = true;
        //movementScript.magLoop[1] = true;
        //movementScript.magLoop[2] = true;

        //movementScript.gyroLoop[0] = true;
        //movementScript.gyroLoop[1] = true;
        //movementScript.gyroLoop[2] = true;

        //movementScript.accLoop[0] = true;
        //movementScript.accLoop[1] = true;
        //movementScript.accLoop[2] = true;
        Debug.Log(String.Format("X: {0}, Y: {1}, Z: {2}", xPosition_Value, yPosition_Value, zPosition_Value));
    }
}


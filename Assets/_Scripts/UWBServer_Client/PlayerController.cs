using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UWBServer_Client
{
    public class PlayerController : MonoBehaviour
    {
        private void FixedUpdate()
        {
            SendInputToServer();
        }

        /// <summary>Sends player input to the server.</summary>
        private void SendInputToServer()
        {
            bool[] _inputs = new bool[]
            {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            };

            ClientSend.PlayerMovement(_inputs);
        }
    }
}

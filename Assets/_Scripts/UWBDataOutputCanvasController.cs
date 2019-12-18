using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UWBDataOutputCanvasController : MonoBehaviour
{

    public TextMeshProUGUI text;
    public Transform uwbObject;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = string.Format("Pos X:{0}\nPos Y:{1}\nPos Z:{2}", uwbObject.position.x, uwbObject.position.y, uwbObject.position.z);
    }
}

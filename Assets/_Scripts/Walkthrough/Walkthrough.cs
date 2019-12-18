using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkthrough : MonoBehaviour
{
    public Node WalkNodes;
    public float LookAtTimer = 3f;

    private Transform walker;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DoWalkthrough()
    {
        yield return null;
    }
}

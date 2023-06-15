using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down : MonoBehaviour
{
    public float down;

    // Start is called before the first frame update
    void Start()
    {
        down = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        down += 1f;
    }
}

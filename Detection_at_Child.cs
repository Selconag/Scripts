using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection_at_Child : MonoBehaviour
{
    public bool detect;

    private void Awake()
    {
        detect = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            detect = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ball")
        {
            detect = false;
        }
    }


}

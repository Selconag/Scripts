using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enumarator : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
    void Start()
    {
        StartCoroutine(FireShot());
    }
    IEnumerator FireShot()
    {
        Debug.Log("Fired First");

        yield return new WaitForSeconds(3);

        Debug.Log("Fired First");

        yield return 0;
    }
}

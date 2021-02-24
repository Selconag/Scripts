using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Fadeout : MonoBehaviour
{
    private bool faded = false;
    public float Duration = 1f;
    public GameObject obj;
    public void Fade()
    {
        var canvGroup = GetComponent<CanvasGroup>();

        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, faded ? 1 : 0));
        faded = !faded;
       
    }
    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end)
    {                       
        float counter = 0f;

        while (counter < Duration)
        {
            counter = Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);
            yield return null;
        }
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket_Detect : MonoBehaviour
{
    public float BasketDetectCooldown;

    private bool control;

    private int shootCounter;

    public List<Detection_at_Child> detPoints;

    private void Awake()
    {
        shootCounter = 0;
        control = true;
    }

    private void Update()
    {
        bool cnt = true;

        foreach (Detection_at_Child item in detPoints)
        {
            if (!item.detect) cnt = false; 
        }

        if (cnt && control) Score(); 
    }

    void Score()
    {
        control = false;
        GameObject currentPlayer = GameObject.FindGameObjectWithTag("Player");
        shootCounter++;


        if (currentPlayer != null)
        {
            Character_Controller datas = currentPlayer.GetComponent<Character_Controller>();
            datas.currentScore += datas.ScoreForPoints[datas.currentPosIndex];

            if (shootCounter % 2 == 0 && shootCounter >= 2) datas.RandPos();
        }

        StartCoroutine("DelayBasket", BasketDetectCooldown);
    }

    IEnumerator DelayBasket(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        control = true;
    }

}

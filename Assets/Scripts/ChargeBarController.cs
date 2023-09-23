using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBarController : MonoBehaviour
{

    private Slider chargeBar;
    public float currentCharge = 0f;

    // Start is called before the first frame update
    void Start()
    {
        chargeBar = GetComponent<Slider>();
    }


    // Update is called once per frame
    void Update()
    {
        chargeBar.value = currentCharge;
    }


    public void changeCharge(float charge)
    {
        currentCharge = charge;
    }
}

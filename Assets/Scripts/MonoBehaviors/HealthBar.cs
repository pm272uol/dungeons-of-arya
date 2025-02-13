using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // using the UI

public class HealthBar : MonoBehaviour
{
    public HitPoints hitPoints;

    [HideInInspector] public Player character;

    public Image meterImage;

    public Text hpText;

    float maxHitPoints;

    // Start is called before the first frame update
    void Start()
    {

        maxHitPoints = character.maxHitPoints; // Set the maxHitPoints to be the maxHitPoints of the character
    }

    // Update is called once per frame
    void Update()
    {
        if(character != null)
        {
            meterImage.fillAmount = hitPoints.value / maxHitPoints; // Change the meter image length to be the value of hitpoints

            hpText.text = "HP:" + (meterImage.fillAmount * 100);
        }
    }
}

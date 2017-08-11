using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TraitsAttibution : MonoBehaviour
{

    Text inputfield;
    Text remainingPointsField;

    // Use this for initialization
    void Start()
    {
        remainingPointsField = GameObject.Find("RemainingPointsValue").GetComponent<Text>();
        inputfield = transform.parent.transform.FindChild("Placeholder").GetComponent<Text>();
    }

    public void IncreaseValue()
    {
        int remainingPoints = Utility.ParseToInt(remainingPointsField.text);

        if (remainingPoints > 0)
        {
            int transfValue = Utility.ParseToInt(inputfield.text);
            transfValue++;
            inputfield.text = transfValue.ToString();
            remainingPoints--;
            remainingPointsField.text = remainingPoints.ToString();
        }
    }

     public void DecreaseValue()
    {
        int transfValue = Utility.ParseToInt(inputfield.text);

        if (transfValue > 0)
        {
            int remainingPoints = Utility.ParseToInt(remainingPointsField.text);
            transfValue--;
            inputfield.text = transfValue.ToString();
            remainingPoints++;
            remainingPointsField.text = remainingPoints.ToString();
        }
    }


}

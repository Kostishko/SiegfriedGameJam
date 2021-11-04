using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    [SerializeField] Image bar;

    public void UpdateBar(int value, int MaxValue)
    {
        bar.fillAmount = (float)value / MaxValue;
    }
}

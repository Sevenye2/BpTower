using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ToggleExtend : MonoBehaviour
{
    public Color labelActiveColor;
    public Color backgroundActivateColor;
    
    public Color labelInactiveColor;
    public Color backgroundInactiveColor;

    public Toggle toggle;
    public Image background;
    public TextMeshProUGUI label;

    public GameObject page;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        toggle.isOn = toggle.isOn;
        OnValueChanged(toggle.isOn); 
    }

    public void OnValueChanged(bool value)
    {
        var labelColor =  value ? labelActiveColor : labelInactiveColor;
        var bg =  value ? backgroundActivateColor : backgroundInactiveColor;
        label.DOColor(labelColor, 0.1f);
        background.DOColor(bg, 0.1f);
        
        page.SetActive(value);
    }
}


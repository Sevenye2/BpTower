using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoticeUI : MonoBehaviour
{

    public TextMeshProUGUI current;
    public Transform split;
    public TextMeshProUGUI next;

    public RectTransform panel;

    void Start()
    {
        
    }
    
    public void ShowCurrent(string c)
    {
        gameObject.SetActive(true);
        current.gameObject.SetActive(true);
        split.gameObject.SetActive(false);
        next.gameObject.SetActive(false);
       
        current.text = "Current:\n" + c;
 
        panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, current.preferredHeight + 50);
    }
    public void ShowNext(string c)
    {
        gameObject.SetActive(true);
        current.gameObject.SetActive(false);
        split.gameObject.SetActive(false);
        next.gameObject.SetActive(true);
        
        next.text = "Next:\n" + c;
        panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, next.preferredHeight + 50);
    }



    public void ShowDouble(string c1, string c2)
    {
        gameObject.SetActive(true);
        current.gameObject.SetActive(true);
        split.gameObject.SetActive(true);
        next.gameObject.SetActive(true);

        current.text = "Current:\n" + c1;
        next.text = "Next:\n" + c2;
        
        panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, current.preferredHeight + next.preferredHeight + 80);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LogUI : MonoBehaviour, IPointerClickHandler
{
    public ConsoleUI battleUI;

    // Start is called before the first frame update
    void Start()
    {
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        battleUI.consoleActivateTime = 0f;
        battleUI.isFocusConsole = true;
    }
}
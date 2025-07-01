using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsoleUI : MonoBehaviour
{
    public bool interactable = true;
    public int max = 60;

    
    public TextMeshProUGUI console;
    public ScrollRect scrollRect;
    public TMP_InputField inputField;
   
    private readonly Queue<string> _messages = new();
    
    public float consoleActivateTime;
    public bool isFocusConsole;

    public bool isFocusInput;
    
    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFocusConsole)
        {
            consoleActivateTime += Time.deltaTime;
            if (consoleActivateTime > 10)
            {
                isFocusConsole = false;
            }
        }
        else
        {
            scrollRect.verticalScrollbar.value = 0;
        }

        if (isFocusInput)
        {
            if (!Input.GetKeyDown(KeyCode.Return)) return;

            var text = inputField.text;
            Log($"<size=10><i><color=#80808020> -- {text}</color></i></size>");
            ProcessController.Instance.PlayerInput(text);

            EventSystem.current.SetSelectedGameObject(null);
            inputField.text = "";
        }
        else
        {
            if (!Input.GetKeyDown(KeyCode.Return) || !interactable) return;

            inputField.Select();
        } 
    }
    
    public void Log(string text)
    {
        var m = $"{text}\n";
        _messages.Enqueue(m);
        if (_messages.Count > max)
            _messages.Dequeue();

        var result = "";
        _messages.ToList().ForEach(s => result += s);
        console.text = result;

        scrollRect.content
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, console.preferredHeight);
    }


    public void OnInputFieldSelect()
    {
        isFocusInput = true;
    }

    public void OnInputFieldDeselect()
    {
        isFocusInput = false;
    }

    public void Clear()
    {
        console.text = "";
        inputField.text = "";
        _messages.Clear(); 
    }
}

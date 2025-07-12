using TMPro;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public TextMeshProUGUI hp;
    public TextMeshProUGUI time;
    public TextMeshProUGUI level;
    public ConsoleUI console;

    public void Start()
    {
    }

    public void Open()
    {
        console.interactable = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        console.interactable = false;
        gameObject.SetActive(false);
    }

    public void LogTime(float timestamp)
    {
        var m = (int)timestamp / 60;
        var s = timestamp % 60;

        time.SetText($"Time = {m:00} : {s:00.0}");
    }

    public void LogLevel(int l)
    {
        level.SetText($"\"Level = {l}\"");
    }

    public void LogHp(int value, int max)
    {
        hp.text = $"\"Hp = {value}/{max}\"";
    }
}
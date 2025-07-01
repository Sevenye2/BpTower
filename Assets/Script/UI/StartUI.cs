using Framework;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void StartBtn()
    {
        ProcessController.Instance.NewGame();
    }
    
    public void ExitBtn()
    {
        Application.Quit();
    }
    
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProcessController : MonoSingleton<ProcessController>
{
    // Start is called before the first frame update
    void Start()
    {
    }
    // ReSharper disable Unity.PerformanceAnalysis

    public void PlayerInput(string text)
    {
        switch (text.ToLower())
        {
            case "new":
                NewGame();
                GlobalUI.Instance.battleUI.console.Log("New Game : Level = 1, Time = 5 minus");
                break;
            case "win":
                GameOver(true);
                break;
            case "lose":
                GameOver(false);
                break;
            default:
                GlobalUI.Instance.battleUI.console.Log("No Command Found, Type 'Help' To Get Help");
                break;
        }
    }


    public void NewGame()
    {
        _isGameOver = false;

        SpawnPlayer();

        UniTask.Void(KeepSpawnEnemy);
        StartCoroutine(BattleStart());
    }


    // battle

    #region Battle

    public PlayerController Player;
    private List<EnemyController> _enemies = new();

    private bool _isGameOver;

    private void SpawnPlayer()
    {
        Player = new PlayerController();
        Player.ReLoad(SaveDataHandler.Temp, ReferenceManager.Instance.player);
    }

    private async UniTaskVoid KeepSpawnEnemy()
    {
        while (true)
        {
            // SpawnEnemy
            var ctl = new EnemyController();
            await ctl.Link();
            _enemies.Add(ctl);
            var gap = Random.Range(0, 500);

            await UniTask.Delay(gap);

            if (_isGameOver)
                break;
        }
    }

    private IEnumerator BattleStart()
    {
        GlobalUI.Instance.battleUI.LogLevel(1);
        var turnTime = 300f;

        while (!_isGameOver)
        {
            GlobalUI.Instance.battleUI.LogHp(SaveDataHandler.Temp.hp);

            // property
            turnTime -= Time.deltaTime;

            // display
            GlobalUI.Instance.battleUI.LogTime(turnTime);

            // logic run
            Player.Run();

            // clean
            _enemies = _enemies.Where(c => c._isDead == false).ToList();
            _enemies.ForEach(e => { e.Run(); });

            yield return new WaitForEndOfFrame();

            if (!(turnTime <= 0)) 
                continue;
            
            GameOver(true);
        }
        
        // Clear
        _enemies.ForEach(e => { e.OnDead(); });
        _enemies.Clear();
        EnemyController.Clear();
    }

    public void GameOver(bool isWin)
    {
        _isGameOver = true;

        GlobalUI.Instance.settlementUI.Open(isWin);

        if (isWin)
        {
            GlobalUI.Instance.battleUI.console.Log("<size=20>Win :)</size>");
            GlobalUI.Instance.settlementUI.OnConfirm
                = () => { _ = GlobalUI.Instance.programmingUI.OpenAsync(); };
        }
        else
        {
            GlobalUI.Instance.battleUI.console.Log("<size=20><color=red>Lose :(<.color></size>");
        }
    }

    public void EnemyDead(EnemyController controller)
    {
        GlobalUI.Instance.battleUI.console.Log("<size=10>enemy killed</size>");
    }

    #endregion
}
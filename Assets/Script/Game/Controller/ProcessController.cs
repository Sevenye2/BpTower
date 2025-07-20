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
            case "test":
                BattleStart();
                break;
            case "win":
                Win();
                break;
            case "lose":
                Lose();
                break;
            default:
                GlobalUI.Instance.battleUI.console.Log("No Command Found, Type 'Help' To Get Help");
                break;
        }
    }


    public void BattleStart()
    {
        StartCoroutine(m_BattleStart());
    }


    // battle

    #region Battle

    public PlayerController Player;
    private bool _isGameOver;
    private BattleData _battleData;

    private void SpawnPlayer()
    {
        Player = new PlayerController();
        Player.ReLoad(SaveDataHandler.Data, ReferenceManager.Instance.player);
    }

    private async UniTaskVoid KeepSpawnEnemy(int level)
    {
        var enemyHp = LevelConfig.GetEnemyHp(level);
        var enemySpawn = LevelConfig.GetSpawnTime(level);

        while (!_isGameOver)
        {
            var hp = Random.Range(enemyHp.x, enemyHp.y);
            // SpawnEnemy
            var ctl = new EnemyController((int)hp);
            await ctl.Link();
            var gap = Random.Range(enemySpawn.x, enemySpawn.y);
            await UniTask.WaitForSeconds(gap);
        }
    }

    private IEnumerator m_BattleStart()
    {
        _isGameOver = false;
        _battleData = new BattleData();

        var level = SaveDataHandler.Data.level;
        var minus = LevelConfig.GetGameTime(level);


        GlobalUI.Instance.battleUI.console.Log($"New Game : Level = {level}, Time = {minus} minus");
        GlobalUI.Instance.battleUI.LogLevel(level);
        SpawnPlayer();
        Player.OnStart();

        _ = KeepSpawnEnemy(level);

        var second = minus * 60;
        while (!_isGameOver)
        {
            // property
            second -= Time.deltaTime;

            // display
            GlobalUI.Instance.battleUI.LogTime(second);

            // logic run
            Player.Run();

            // clean
            EnemyController.RunAll();

            yield return new WaitForEndOfFrame();

            if (!(second <= 0))
                continue;

            Win();
        }
        
        EnemyController.ClearAll();
    }


    public void Win()
    {
        _isGameOver = true;
        var levelAward = 100 + SaveDataHandler.Upgrades.ExtraAward;
        // display
        GlobalUI.Instance.battleUI.console.Log("<size=20>Win :)</size>");

        var settlement = GlobalUI.Instance.settlementUI;
        settlement.OnConfirm
            = () => { _ = GlobalUI.Instance.programmingUI.OpenAsync(); };
        
        settlement.AddDisplay("Level Clean", SaveDataHandler.Data.level);
        settlement.AddDisplay("Enemy Killed", _battleData.KillEnemyCount);
        settlement.AddDisplay("Level Award", levelAward);
        settlement.AddDisplay("Get Point", _battleData.AwardPoint);

        _ = settlement.Open(true);

        // logic
        SaveDataHandler.Data.point += levelAward + _battleData.AwardPoint;
        SaveDataHandler.Data.level++;
        SaveDataHandler.Save();
    }

    public void Lose()
    {
        _isGameOver = true;
        // display
        GlobalUI.Instance.battleUI.console.Log("<size=20><color=red>Lose :(</color></size>");
        GlobalUI.Instance.settlementUI.OnConfirm = () => { _ = GlobalUI.Instance.startUI.OpenAsync(); };

        _ = GlobalUI.Instance.settlementUI.Open(false);
        
        //logic
        SaveDataHandler.Delete();
    }


    public void EnemyDead(EnemyController controller)
    {
        var pt = 1 + SaveDataHandler.Upgrades.ExtraEnemyAward;
        _battleData.AwardPoint += pt;
        _battleData.KillEnemyCount++;
        GlobalUI.Instance.battleUI.console.Log($"<size=10>enemy killed, get pt:{pt}</size>");
    }

    #endregion
}


public class BattleData
{
    public int AwardPoint;
    public int KillEnemyCount;
}
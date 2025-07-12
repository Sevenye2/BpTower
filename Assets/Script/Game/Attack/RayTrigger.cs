using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private Action<RuntimeData> _onHitCallback;

    public void SetCallBack(Action<RuntimeData> onHitCallback)
    {
        _onHitCallback = onHitCallback;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        var e = other.GetComponent<EnemyViewer>();
        _onHitCallback?.Invoke(new RuntimeData()
        {
            Position = e.transform.position,
            Enemy = e
        });
    }
}

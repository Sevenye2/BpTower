using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class RayLine : MonoBehaviour
{
    private float _duration = 2;
    private float _length = 200;
    private float _width = 5;
    private float _angle;
    

    public Transform linePivot;
    public Transform tailPos;
    public Transform head;
    public Transform tailCap;
    
    public RayTrigger trigger;

    public AnimationCurve capScaleCurve;
    public AnimationCurve lengthCurve;
    public AnimationCurve widthCurve;


    private static BufferPool<RayLine> _pool;

    public static void Create(RayLineData data)
    {
        _pool ??= new BufferPool<RayLine>(Resources.Load<RayLine>($"Prefabs/RayLine"));
        _ = _pool.CreateAsync(1, (r, i) =>
        {
            r._time = 0;
            r._duration = data.Duration;
            r._length = data.Length;
            r._width = data.Width;
            r._angle = data.Angle;
            r.trigger.SetCallBack(data.OnHitCallback);
            r.transform.position = data.Origin;
            r.gameObject.SetActive(true);
        });
    }

    void Start()
    {
    }

    private float _time;

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, _angle);

        _time += Time.deltaTime;

        if (_time >= _duration)
        {
 
            gameObject.SetActive(false);
            _pool.Destroy(this);
            return;
        }


        var x = _length * lengthCurve.Evaluate(_time / _duration);
        var w = _width * widthCurve.Evaluate(_time / _duration);
        linePivot.transform.localScale = new Vector3(x, w, 1);
        
        var s = capScaleCurve.Evaluate(_time / _duration);
        head.transform.localScale = new Vector3(s, s, 1) * (_width * 0.03f);
        tailCap.transform.localScale = new Vector3(w,w,1)* 0.05f;
        tailCap.transform.position = tailPos.position;
    }

}

public struct RayLineData
{
    public float Duration;
    public float Length;
    public float Width;
    public Vector2 Origin;
    public float Angle;
    public Action<RuntimeData> OnHitCallback;
}
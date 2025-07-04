using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private float _time;
    private float _time2;

    private void Update()
    {
        _time += Time.deltaTime;
        _time2 += Time.deltaTime;

        if (_time > 1.1f)
        {
            _time -= 1.1f;

            _ = Doom.Create(new DoomData()
            {
                Target = new Vector3(Random.Range(-3,3), Random.Range(-3,3), 0)
            }, Debug.Log);
        }

        if (_time2 > 0.7f)
        {
            _time2 -= 0.7f;
            
            _ = Doom.Create(new DoomData()
            {
                Target = new Vector3(Random.Range(-3,3), Random.Range(-3,3), 0)
            }, Debug.Log);
        }
    }
}
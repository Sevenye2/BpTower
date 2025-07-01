using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class JoyStickController : MonoBehaviour, IPointerDownHandler , IPointerUpHandler
{
    public RectTransform joyStick;
    public RectTransform joyStickPanel;
    // 默认图片是正方形
    private float Size => joyStickPanel.rect.width * 0.5f;
    private Vector2 _downPos = Vector2.zero;
    private bool _isDrag = false;

    public static Vector2 Direction;

    public void OnPointerDown(PointerEventData eventData)
    {
        _downPos = Input.mousePosition;
        _isDrag = true;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        joyStick.localPosition = Vector2.zero;
        Direction = Vector2.zero;
        _isDrag = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isDrag) 
            return;
        
        var vec = (Vector2)Input.mousePosition - _downPos;
        vec = vec.magnitude < Size ? vec : vec.normalized * Size;
        Direction = vec;
        joyStick.localPosition = vec;
    }
}

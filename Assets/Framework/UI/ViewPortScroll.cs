using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ViewPortScroll : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool _mouseIn = false;
    
    private float _scale = 1;
    public float min = 0.4f;
    public float max = 1.5f;
    public UnityEvent<float> onScroll;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!_mouseIn) return;

        var delta = Input.mouseScrollDelta.y;
        _scale = Mathf.Clamp(_scale + delta / 50f, 0.4f, 1.5f);
        transform.localScale = Vector3.one * _scale;
        onScroll?.Invoke(_scale);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _mouseIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _mouseIn = false;
    }
}
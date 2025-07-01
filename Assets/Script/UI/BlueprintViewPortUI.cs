using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlueprintViewPortUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool _mouseIn = false;

    private float _scale = 1;

    public BlueprintUI blueprintUI;
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
        blueprintUI.OnViewPortChange();
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
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class WrapGridLayout : MonoBehaviour
{
    private GridLayoutGroup _gridLayout;
    private RectTransform _rectTransform;

    void Start()
    {
        _gridLayout = GetComponent<GridLayoutGroup>();
        _rectTransform = GetComponent<RectTransform>();
        WrapContent();
    }

    void WrapContent()
    {
        var childCount = transform.childCount;
        if (childCount == 0) return;

        var cellSize = _gridLayout.cellSize;
        var spacing = _gridLayout.spacing;
        var constraintCount = _gridLayout.constraintCount;


        switch (_gridLayout.constraint)
        {
            case GridLayoutGroup.Constraint.Flexible:
                break;
            case GridLayoutGroup.Constraint.FixedColumnCount:
                _rectTransform.sizeDelta = new Vector2(1,Mathf.CeilToInt((float)childCount / constraintCount) * cellSize.y + (constraintCount - 1) * spacing.y);
                break;
            case GridLayoutGroup.Constraint.FixedRowCount:
                _rectTransform.sizeDelta = new Vector2(Mathf.CeilToInt((float)childCount / constraintCount) * cellSize.x + (constraintCount - 1) * spacing.x, 1);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
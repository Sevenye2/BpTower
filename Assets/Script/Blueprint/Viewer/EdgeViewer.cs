using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Vectrosity;
using Object = UnityEngine.Object;

public sealed class EdgeViewer
{
    private static Texture _lineTexture;
    private static Texture _capTexture;
    private readonly VectorLine _line;

    public PortViewer Output;
    public PortViewer Input;

    public readonly BpEdgeSaveData Data;

    private Vector2 _start, _left, _right, _end;

    private const int Segment = 20;

    public EdgeViewer(Transform panel, BpEdgeSaveData saveData)
    {
        _lineTexture ??= Resources.Load<Texture2D>($"Image/ThinLine");
        _capTexture ??= Resources.Load<Texture2D>($"Image/Dot");

        Data = saveData;
        
        var uName = GetHashCode().ToString();
        var lineName = $"{uName}-line";

        _line = new VectorLine(lineName, new List<Vector2>(Segment * 2 + 4), 5, LineType.Continuous)
        {
            texture = _lineTexture
        };

        _line.SetCanvas(GlobalUI.Instance.canvas);
        _line.rectTransform.SetParent(panel);
        _line.rectTransform.position = Vector3.zero;
    }

    public void Start()
    {
    }


    public void SetStart(Vector3 pos)
    {
        UpdateLine(pos, _end);

    }

    public void SetEnd(Vector3 pos)
    {
        UpdateLine(_start, pos);
    }

    public void UpdateLine(Vector2 start, Vector2 end)
    {
        _start = start;
        _left = start + Vector2.right * 20;
        _end = end;
        _right = end + Vector2.left * 20;

        // var mid = (_left + _right) / 2;
        // var cl = new Vector2(_mid.x, _left.y);
        // var cr = new Vector2(_mid.x, _right.y);

        var cl = _left + Vector2.right * 60;
        var cr = _right + Vector2.left * 60f;

        _line.points2[0] = _start;
        _line.points2[1] = _left;
        
        // _line.MakeCurve(_left, cl, _mid, cl, Segment, 2);
        // _line.MakeCurve(_mid, cr, _right, cr, Segment, Segment + 2);
        _line.MakeCurve(_left, cl, _right, cr, Segment * 2, 2);

        _line.points2[Segment * 2 + 2] = _right;
        _line.points2[Segment * 2 + 3] = _end;

        _line.Draw();
    }


    public void Confirm(PortViewer output, PortViewer input)
    {
        Output = output;
        Input = input;

        Data.outputUid = output.bp.data.uid;
        Data.inputUid = input.bp.data.uid;
        Data.outputPort = output.index;
        Data.inputPort = input.index;
        
        UpdateLine(output.RefPosition,input.RefPosition);

        Output.Connected(this);
        Input.Connected(this);
    }

    public void UpdateLine()
    {
        if(Input == null || Output == null) return;
        UpdateLine(Output.RefPosition, Input.RefPosition);
    }


    // 销毁物体
    public void Discard()
    {
        Input?.Disconnected();
        Output?.Disconnected();

        Object.Destroy(_line.rectTransform.gameObject);

        GlobalUI.Instance.programmingUI.blueprintUI.Refresh();
    }
}
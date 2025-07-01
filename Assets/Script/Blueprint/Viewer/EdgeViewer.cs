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
    private readonly VectorLine _cap;

    private readonly GameObject _lineObj;
    private readonly GameObject _capObj;

    public PortViewer Output;
    public PortViewer Input;

    public BpEdgeSaveData Data = new();

    public EdgeViewer(Transform panel)
    {
        _lineTexture ??= Resources.Load<Texture2D>($"Image/ThinLine");
        _capTexture ??= Resources.Load<Texture2D>($"Image/Dot");

        var uName = GetHashCode().ToString();
        var points = new List<Vector2>(2);

        var lineName = $"{uName}-line";
        var capName = $"{uName}-cap";

        _line = new VectorLine(lineName, points, 5)
        {
            texture = _lineTexture
        };

        _line.SetCanvas(GlobalUI.Instance.canvas);
        _lineObj = _line.m_go;
        _lineObj.transform.SetParent(panel);
        _lineObj.transform.localPosition = Vector3.zero;
        
        _cap = new VectorLine(capName, points, 10, LineType.Points)
        {
            texture = _capTexture
        };

        _cap.SetCanvas(GlobalUI.Instance.canvas);
        _capObj = _cap.m_go;
        _capObj.transform.SetParent(panel);
        _capObj.transform.localPosition = Vector3.zero;
    }

    public void Start()
    {
    }


    public void SetStart(Vector3 pos)
    {
        _line.points2[0] = pos;
        _line.Draw();
        _cap.points2[0] = pos;
        _cap.Draw();
    }

    public void SetEnd(Vector3 pos)
    {
        _line.points2[1] = pos;
        _line.Draw();
        _cap.points2[1] = pos;
        _cap.Draw();
    }

    public void Confirm(PortViewer output, PortViewer input)
    {
        Output = output;
        Input = input;

        Data.outputUid = output.bp.data.uid;
        Data.inputUid = input.bp.data.uid;
        Data.outputPort = output.index;
        Data.inputPort = input.index;


        SetStart(output.RefPosition);
        SetEnd(input.RefPosition);

        Output.Connected(this);
        Input.Connected(this);
    }


    // 销毁物体
    public void Discard()
    {
        Input?.Disconnected();
        Output?.Disconnected();

        Object.Destroy(_lineObj);
        Object.Destroy(_capObj);
        
        GlobalUI.Instance.programmingUI.blueprintUI.Refresh();
    }
}
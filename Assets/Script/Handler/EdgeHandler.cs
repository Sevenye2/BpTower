using Framework;
using UnityEngine;

public class EdgeHandler
{
    // 0: output position 1: input position

    private static EdgeHandler _instance;
    public static EdgeHandler Instance => _instance ??= new EdgeHandler();

    private bool _isControlStart = false;
    // 左为起始
    private PortViewer _startPort;

    private EdgeViewer _edge;

    public void ToStart(PortViewer port)
    {
        if (port.Edge == null)
        {
            _startPort = port;
            _edge = GlobalUI.Instance.programmingUI.blueprintUI.CreateEdge();
            _edge.UpdateLine(_startPort.RefPosition, _startPort.RefPosition);
            _isControlStart = IOType.Input == port.config.ioType;

        }
        else
        {
            _edge = port.Edge;
            _isControlStart = IOType.Input != port.config.ioType;

            if (_isControlStart)
            {
                _startPort = _edge.Input;
                _edge.Output.Disconnected();
            }
            else
            {
                _startPort = _edge.Output;
                _edge.Input.Disconnected();
            }
            
        }
    }

    public void OnDrag(Vector3 position)
    {
        if (_isControlStart)
        {
            _edge.SetStart(position);
        }
        else
        {
            _edge.SetEnd(position);
        }
    }
    
    public void ToEnd(PortViewer port)
    {
        if (_isControlStart)
        {
            if (port.config.ioType == IOType.Input)
            {
                Discard();
                return;
            }
        }
        else
        {
            if (port.config.ioType == IOType.Output)
            {
                Discard();
                return;
            }
        }

        if (_startPort.config.portType != port.config.portType)
        {
            Discard();
            return;
        }

        if (port.Edge != null)
        {
            var e = port.Edge;
            GlobalUI.Instance.programmingUI.blueprintUI.RemoveEdge(e);
        }

        if (_isControlStart)
            _edge.Confirm(port, _startPort);
        else
            _edge.Confirm(_startPort, port);

        
        GlobalUI.Instance.programmingUI.blueprintUI.Refresh();

        _instance = null;
    }

    public void Discard()
    {
        GlobalUI.Instance.programmingUI.blueprintUI.RemoveEdge(_edge); 
        _instance = null;
    }
}
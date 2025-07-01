using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BPViewerBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler,
    IPointerClickHandler
{
    public BpNodeSaveData data;
    public BpNodeConfig config;

    public List<PortViewer> ports;

    // Start is called before the first frame update
    public abstract void Initialize(BpNodeSaveData saveData);
    public abstract void Refresh();

    public PortViewer GetPort(int port)
    {
        return ports[port];
    }

    // 拖拽
    private Vector2 _offset;
    private bool _isDragging;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        
        _isDragging = true;
        transform.SetAsLastSibling();
        _offset = transform.position - Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging) return;
        
        transform.position = eventData.position + _offset;
        SetLine();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isDragging) return;
        
        _isDragging = false;
        transform.position = eventData.position + _offset;
        SetLine();
        data.position = new List<float>() { transform.position.x, transform.position.y };
    }

    public void SetLine()
    {
        ports.Where(port => port.Edge != null).ToList().ForEach(port => port.OnNodeBeDrag());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;
        var menu = GlobalUI.Instance.menuUI;

        GetMenuList(menu);
        menu.Open(Input.mousePosition);
    }

    public virtual void GetMenuList(ContentMenuUI menu)
    {
        menu.AddDisplay("Node");
        var sellPt = (int)(config.prise * 0.5f);
        menu.AddBtn($"Sell:   {sellPt} point", () =>
        {
            SaveDataHandler.Temp.point += sellPt;
            GlobalUI.Instance.programmingUI.blueprintUI.RemoveNode(this);
        });
        menu.AddBtn("Clean Link", () =>
        {
            foreach (var port in ports.Where(port => port.Edge != null))
                GlobalUI.Instance.programmingUI.blueprintUI.RemoveEdge(port.Edge);
        });
    }
}
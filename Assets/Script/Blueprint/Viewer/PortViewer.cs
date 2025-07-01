using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PortViewer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
    IPointerUpHandler,
    IDragHandler,
    IPointerClickHandler
{
    public TextMeshProUGUI right;
    public TextMeshProUGUI left;

    public Image normal;
    public Image hint;
    public Image connected;

    public EdgeViewer Edge;
    public BPViewerBase bp;

    public int index;
    public BpPortConfig config;

    private TextMeshProUGUI _label;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Initialize(int i, BPViewerBase n, BpPortConfig c)
    {
        index = i;
        bp = n;
        config = c;

        if (n.config.style == NodeStyle.Root)
        {
            normal.color = ReferenceManager.Instance.red.lightColor;
            var dark = ReferenceManager.Instance.red.lightColor;
            dark.a = 0.5f;
            connected.color = dark;
        }

        if (c.flag == PortFlag.Amplify)
        {
            normal.pixelsPerUnitMultiplier = 1;
            hint.pixelsPerUnitMultiplier = 1;
            connected.pixelsPerUnitMultiplier = 1;
        }

        // label
        if (c.type == IOType.Input)
        {
            _label = right;
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(true);
        }
        else
        {
            _label = left;
            left.gameObject.SetActive(true);
            right.gameObject.SetActive(false);
        }

        _label.text = c.description;
    }

    public Vector3 RefPosition
    {
        get
        {
            var offset = transform.position - bp.transform.position;
            return bp.transform.localPosition + offset;
        }
    }

    public void Connected(EdgeViewer edge)
    {
        Edge = edge;
        connected.gameObject.SetActive(true);
    }

    public void Disconnected()
    {
        Edge = null;
        connected.gameObject.SetActive(false);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        hint.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hint.gameObject.SetActive(false);
    }

    private Vector3 _mousePosition;
    private bool _isDragging;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Left) return;
        _isDragging = true;
        
        _mousePosition = Input.mousePosition;
        EdgeHandler.Instance.ToStart(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging) return;
        
        // TODO: 转换坐标
        var offset = Input.mousePosition - _mousePosition;
        EdgeHandler.Instance.OnDrag(RefPosition + offset);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isDragging) return;
        _isDragging = false;
        
        var cast = eventData.pointerCurrentRaycast;
        if (!cast.gameObject)
        {
            EdgeHandler.Instance.Discard();
            return;
        }

        var point = cast.gameObject.GetComponent<PortViewer>();
        if (point == null)
        {
            EdgeHandler.Instance.Discard();
            return;
        }

        EdgeHandler.Instance.ToEnd(point);
    }

    public void OnNodeBeDrag()
    {
        if (config.type == IOType.Input)
            Edge?.SetEnd(RefPosition);
        else
            Edge?.SetStart(RefPosition);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        var menu = GlobalUI.Instance.menuUI;
        
        bp.GetMenuList(menu);
        GetMenuItem(menu);
        menu.Open(Input.mousePosition);
    }


    public void GetMenuItem(ContentMenuUI menu)
    {
        if(Edge == null) return;
        menu.AddDisplay("Port");
        menu.AddBtn("Disconnect", () =>
        {
            GlobalUI.Instance.programmingUI.blueprintUI.RemoveEdge(Edge);
        });
        
    }
}
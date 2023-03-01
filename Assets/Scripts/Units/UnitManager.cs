using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private bool _isDraggingMouseBox = false;
    private Vector3 _dragStartPosition;
    Ray _ray;
    RaycastHit _raycastHit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDraggingMouseBox = true;
            _dragStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonDown(2) && (!_isDraggingMouseBox))
        {
            _DeselectAllUnits();
        }

        if (Input.GetMouseButtonUp(0))
            _isDraggingMouseBox = false;

        if (_isDraggingMouseBox && _dragStartPosition != Input.mousePosition)
            _SelectUnitsInDraggingBox();

        if (Input.GetMouseButton(1))
        {
            if(this.gameObject.GetComponent<Unit>().isSelected)
            {
                this.gameObject.GetComponent<Unit>().moveGoal = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (this.gameObject.GetComponent<Unit>().isSelected)
            {
                this.gameObject.GetComponent<Unit>().autoMove = false;
                this.gameObject.GetComponent<Unit>().moveGoal = this.gameObject.transform.position;
            }
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (this.gameObject.GetComponent<Unit>().isSelected)
            {
                this.gameObject.GetComponent<Unit>().autoMove = true;
                this.gameObject.GetComponent<Unit>().autoMoveGoalAndRotate();
            }
        }

        if (Globals.SELECTED_UNITS.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
                _DeselectAllUnits();
            if (Input.GetMouseButtonDown(0))
            {
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(
                    _ray,
                    out _raycastHit,
                    1000f
                ))
                {
                    if (_raycastHit.transform.tag == "Terrain")
                        _DeselectAllUnits();
                }
            }
        }
    }
    private void _SelectUnitsInDraggingBox()
    {
        if (!gameStatistics.purchasingUnit)
        {
            Bounds selectionBounds = Utils.GetViewportBounds(
                Camera.main,
                _dragStartPosition,
                Input.mousePosition
            );
            GameObject[] selectableUnits = GameObject.FindGameObjectsWithTag("Unit");
            bool inBounds;
            foreach (GameObject unit in selectableUnits)
            {
                inBounds = selectionBounds.Contains(
                    Camera.main.WorldToViewportPoint(unit.transform.position)
                );
                if (inBounds)
                    unit.GetComponent<UnitManager>().Select();
                else
                    unit.GetComponent<UnitManager>().Deselect();
            }
        }
    }

    private void OnMouseDown()
    {
        if (IsActive())
            Select(
                true,
                Input.GetKey(KeyCode.LeftShift) ||
                Input.GetKey(KeyCode.RightShift)
            );
    }
    private void _SelectUtil()
    {
        if (!gameStatistics.purchasingUnit)
        {
            if (Globals.SELECTED_UNITS.Contains(this)) return;
            Globals.SELECTED_UNITS.Add(this);
            this.gameObject.GetComponent<Unit>().isSelected = true;
        }
    }
    protected virtual bool IsActive()
    {
        return true;
    }
    public void Select() { Select(false, false); }
    public void Select(bool singleClick, bool holdingShift)
    {
        // basic case: using the selection box
        if (!singleClick)
        {
            _SelectUtil();
            return;
        }

        // single click: check for shift key
        if (!holdingShift)
        {
            List<UnitManager> selectedUnits = new List<UnitManager>(Globals.SELECTED_UNITS);
            foreach (UnitManager um in selectedUnits)
                um.Deselect();
            _SelectUtil();
        }
        else
        {
            if (!Globals.SELECTED_UNITS.Contains(this))
                _SelectUtil();
            else
                Deselect();
        }
    }

    public void Deselect()
    {
        if (!Globals.SELECTED_UNITS.Contains(this)) return;
        Globals.SELECTED_UNITS.Remove(this);
        this.gameObject.GetComponent<Unit>().isSelected = false;
    }

    private void _DeselectAllUnits()
    {
        List<UnitManager> selectedUnits = new List<UnitManager>(Globals.SELECTED_UNITS);
        foreach (UnitManager um in selectedUnits)
        {
            um.gameObject.GetComponent<Unit>().autoMove = false;
            um.Deselect();
        }
    }
}

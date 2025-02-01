
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlUi : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _elementToFocus;
    [SerializeField] private List<GameObject> _elementToOff;

    public void ControlFocusElement(bool change)
    {
        OffElementUi();
        _elementToFocus.SetActive(change);
        _menuPanel.SetActive(change);
        EventSystem.current.SetSelectedGameObject(_elementToFocus);
    }

    private void OffElementUi()
    {
        if (_elementToFocus == null) { return; }
        for (int i = 0; i < _elementToOff.Count; i++)
        {
            _elementToOff[i].SetActive(false);
        }
    }
}

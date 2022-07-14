using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class SizeFitterByText : MonoBehaviour
{
    [SerializeField] private float _offset;

    private RectTransform _rectTransform;
    
    public void ResizeHeight()
    {
        _rectTransform ??= GetComponent<RectTransform>();

        var rect = Vector2.up * _offset * 2;
        var children = transform.GetComponentsInChildren<TMP_Text>();
        foreach (var child in children)
        {
            var rectTransform = child.rectTransform;
            rect.y += rectTransform.sizeDelta.y == 0 
                ? child.preferredHeight : child.rectTransform.sizeDelta.y;
        }

        _rectTransform.sizeDelta = rect;
    }
}

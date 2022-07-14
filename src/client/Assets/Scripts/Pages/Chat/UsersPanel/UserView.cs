using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class UserView : MonoBehaviour
{
    [SerializeField] private Image _status;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _color;

    public string Name
    {
        get => _name.text;
        set => _name.text = value;
    }

    public Color Color
    {
        get => _color.color;
        set => _color.color = value;
    }

    public bool Status
    {
        get => _status.enabled;
        set => _status.enabled = value;
    }

    public void Init(string name, Color color, bool status)
    {
        Name = name;
        Color = color;
        Status = status;
    }
}

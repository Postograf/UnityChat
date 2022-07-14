using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class MessageView : MonoBehaviour
{
    [SerializeField] private Image _backroung;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _sender;
    [SerializeField] private SizeFitterByText _fitter;

    public Color Color
    {
        get => _backroung.color;
        private set => _backroung.color = value;
    }

    public string Text
    {
        get => _text.text;
        private set => _text.text = value;
    }

    public string Sender
    {
        get => _sender.text;
        private set => _sender.text = value;
    }

    public void Init(string sender, string text, Color color)
    {
        Sender = sender;
        Text = text;
        Color = color;
        _fitter.ResizeHeight();
    }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ChatPanel : Panel
{
    [SerializeField] private MessageView _messagePrefab;

    public void AddMessage(Message message)
    {
        var color = App.Instance.DefaultColor;
        ColorUtility.TryParseHtmlString(message.sender.color, out color);

        AddMessage(message.sender.name, color, message.text);
    }

    public void AddMessage(string name, Color color, string text)
    {
        Instantiate(_messagePrefab, _content)
            .Init(name, text, color);
    }
}

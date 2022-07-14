using TMPro;

using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class MessageSenderField : MonoBehaviour
{
    private TMP_InputField _inputField;

    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
        _inputField.onSubmit.AddListener(SendText);
    }

    private async void SendText(string message)
    {
        _inputField.text = "";
        if (message == "" || message == "\n") return;

        await App.Instance.Connector.SendMessageAsync(App.Instance.User, message);
    }
}

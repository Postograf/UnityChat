using TMPro;

using UnityEngine;

public class AuthorizationPage : Page
{
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private ColorPickField _colorPickField;

    public void LogIn()
    {
        App.Instance.LogInAsync(
            new User
            {
                name = _nameField.text,
                color = $"#{ColorUtility.ToHtmlStringRGBA(_colorPickField.Color)}",
                status = true
            }
        );
    }
}
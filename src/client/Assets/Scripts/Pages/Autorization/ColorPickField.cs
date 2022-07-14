using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class ColorPickField : MonoBehaviour
{
    private Image _fieldImage;
    private Button _pickButton;

    public Color Color => _fieldImage.color;

    private void Awake()
    {
        _pickButton = GetComponent<Button>();
        _fieldImage = GetComponent<Image>();

        _pickButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        ColorPicker.Create(_fieldImage.color, "", null, OnColorSelected);
    }

    private void OnColorSelected(Color color)
    {
        _fieldImage.color = color;
    }
}

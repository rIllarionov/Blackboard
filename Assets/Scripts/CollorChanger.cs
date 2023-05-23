using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollorChanger : MonoBehaviour
{
    [SerializeField] private MouseDrawing _mouseDrawing;
    [SerializeField] private Image _image;
    private Color _color;
    
    //при запуске события нажатие мышкой надо передать цвет в метод по смене цвета в контроллере

    public void ChangeColor()
    {
        _color = _image.color;
        _mouseDrawing.ChangeColor(_color);
    }
}

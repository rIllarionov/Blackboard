using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class MarkerChangeCollor : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _thisImage;

    public void ChangeColor()
    {
        _thisImage.color = _image.color;
    }

}

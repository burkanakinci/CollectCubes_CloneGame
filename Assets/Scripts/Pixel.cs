using UnityEngine;

[System.Serializable]
public class Pixel
{
    public int PixelWidthIndex;
    public int PixelHeightIndex;
    public Color PixelColor;

    public Pixel(int _widthIndex, int _heightIndex, Color _color)
    {
        PixelWidthIndex = _widthIndex;
        PixelHeightIndex = _heightIndex;
        PixelColor = _color;
    }
}

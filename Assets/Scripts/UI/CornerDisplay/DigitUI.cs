using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigitUI : MonoBehaviour {

    public Sprite digit_0;
    public Sprite digit_1;
    public Sprite digit_2;
    public Sprite digit_3;
    public Sprite digit_4;
    public Sprite digit_5;
    public Sprite digit_6;
    public Sprite digit_7;
    public Sprite digit_8;
    public Sprite digit_9;

    public Image digitImage = null;

    private int _digit = 0;

	public void SetValue(int digit)
    {
        _digit = digit;
        digitImage.sprite = GetSpriteForDigit(digit);
        digitImage.enabled = true;
    }

    public void Hide()
    {
        digitImage.enabled = false;
    }

    public void Show()
    {
        digitImage.sprite = GetSpriteForDigit(_digit);
        digitImage.enabled = true;
    }

    private Sprite GetSpriteForDigit(int digit)
    {
        switch (digit)
        {
            case 0:
                return digit_0;
            case 1:
                return digit_1;
            case 2:
                return digit_2;
            case 3:
                return digit_3;
            case 4:
                return digit_4;
            case 5:
                return digit_5;
            case 6:
                return digit_6;
            case 7:
                return digit_7;
            case 8:
                return digit_8;
            case 9:
                return digit_9;
            default:
                break;
        }
        return null;
    }


}

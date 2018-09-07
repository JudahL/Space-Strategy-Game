using UnityEngine;
using Zenject;

public class BarFillComponent : MonoBehaviour
{

    private RectTransform _barFillTr;
    private float _barFillAtZero = -94f;

    private float _currentValue = 1f;
    private float _maxValue = 1f;

    [Inject]
    public void Construct(RectTransform barFillTr)
    {
        _barFillTr = barFillTr;
    }

    private void CalculateBarFill()
    {
        _barFillTr.anchoredPosition = new Vector2(_barFillAtZero + (_barFillAtZero * -(_currentValue / _maxValue)), 0);
    }

    public void UpdateValue(float value)
    {
        _currentValue = value;
        CalculateBarFill();
    }

    public void SetValues(float current, float max)
    {
        _maxValue = max;
        _currentValue = current;
        CalculateBarFill();
    }
}

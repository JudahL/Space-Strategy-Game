using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthBarUI : MonoBehaviour
{
    private Text _barText;
    private BarFillComponent _barFill;

    private float _health = 1;
    private float _maxHealth = 1;
    private Transform _targetTransform;
    private Vector3 _positionOffset;

    [Inject]
    public void Construct(float health, Transform targetTransform, Vector3 positionOffset, BarFillComponent barFill, Text barText)
    {
        _barFill = barFill;
        _barText = barText;
        
        _maxHealth = health;
        _health = health;
        _targetTransform = targetTransform;
        _positionOffset = positionOffset;
    }

    private void Start()
    {
        _barFill.SetValues(_maxHealth, _maxHealth);
        SetText();
        transform.position = Camera.main.WorldToScreenPoint(_targetTransform.position + _positionOffset);
    }

    public void UpdateHealth(int updatedHealth)
    {
        _health = updatedHealth;
        _barFill.UpdateValue(updatedHealth);
        SetText();
    }
    

    private void SetText()
    {
        _barText.text = _health + "/" + _maxHealth;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public class Factory : PlaceholderFactory<float, Transform, Vector3, int, HealthBarUI> {}
}

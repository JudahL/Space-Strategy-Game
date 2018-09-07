using UnityEngine;
using Zenject;

public class AttachHealthBarComponent : MonoBehaviour
{
    private HealthBarUI.Factory _healthBarFactory;
    private IHealthComponent _healthComponent;

    [SerializeField]
    private Vector3 _healthBarOffset;

    [Inject]
    public void Construct(HealthBarUI.Factory factory, IHealthComponent healthComponent)
    {
        _healthBarFactory = factory;
        _healthComponent = healthComponent;
    }

    private void Start()
    {
        _healthBarFactory.Create(_healthComponent.GetHealth(), transform, _healthBarOffset*transform.lossyScale.y, gameObject.GetInstanceID());
    }
}

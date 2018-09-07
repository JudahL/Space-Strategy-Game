using UnityEngine;

public abstract class AttachmentVFX : ScriptableObject
{
    protected ICommand _effectCommand;
    protected Transform _casterTransform;
    protected Transform _targetTransform;
    protected bool _targetHasEvaded;

    public void Activate(ICommand effectCommand, Transform casterTransform, Transform targetTransform, bool targetHasEvaded)
    {
        _effectCommand = effectCommand;
        _casterTransform = casterTransform;
        _targetTransform = targetTransform;
        _targetHasEvaded = targetHasEvaded;
        OnActivate();
    }

    protected abstract void OnActivate();
}

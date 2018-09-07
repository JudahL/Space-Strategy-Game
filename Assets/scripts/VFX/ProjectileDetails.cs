using UnityEngine;

public struct ProjectileDetails
{
    public Sprite Artwork;
    public Vector3 Scale;
    public float Speed;
    public Transform CasterTransform;
    public Transform TargetTransform;
    public ICommand OnHitCommand;
    public bool TargetHasEvaded;

    public ProjectileDetails (Sprite artwork, Vector3 scale, float speed, Transform casterTransform, Transform targetTransform, ICommand onHitCommand, bool targetHasEvaded)
    {
        Artwork = artwork;
        Scale = scale;
        Speed = speed;
        CasterTransform = casterTransform;
        TargetTransform = targetTransform;
        OnHitCommand = onHitCommand;
        TargetHasEvaded = targetHasEvaded;
    }
	
}

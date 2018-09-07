using UnityEngine;

public class EmitParticles : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] _particleSystems;

    public void Emit()
    {
        for (int i = 0; i < _particleSystems.Length; i++)
        {
            _particleSystems[i].Play();
        }
    }
}

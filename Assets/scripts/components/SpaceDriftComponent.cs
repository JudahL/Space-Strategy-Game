using UnityEngine;
using Zenject;

public class SpaceDriftComponent : MonoBehaviour 
{
    [SerializeField]
    private float _driftMagnitude = 0.25f;
    [SerializeField]
    private float _driftFrequency = 0.2f;

    private Transform _targetTransform;
    private Vector3 _position;

    private float _seedOffset;
    readonly private float _seedRangeMin = 0f;
    readonly private float _seedRangeMax = 9999f;
    
    private void Start() 
    {
        _targetTransform = transform;
        _position = _targetTransform.localPosition;
        _seedOffset = Random.Range(_seedRangeMin, _seedRangeMax); // Ensures each drift component has a different perlin pattern
    }

    private void Update() 
    {
        Drift();
    }

    //Applies a perlin shake or "drift" to the transforms position by adding an offset to its base position
    private void Drift() 
    {
        float seed = (Time.time + _seedOffset) * _driftFrequency;
        _targetTransform.localPosition = _position + new Vector3((Mathf.Clamp01(Mathf.PerlinNoise(seed, 0f)) - 0.5f) * _driftMagnitude, //x
                                                               (Mathf.Clamp01(Mathf.PerlinNoise(0f, seed)) - 0.5f) * _driftMagnitude, 0f); //y
    }

}

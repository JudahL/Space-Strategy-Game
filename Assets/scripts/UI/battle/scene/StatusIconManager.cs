using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class StatusIconManager : MonoBehaviour
{
    [SerializeField]
    private Vector2 _rootPosition;
    [SerializeField]
    private float _xInterval;
    [SerializeField]
    private float _xOddRowIndent;
    [SerializeField]
    private float _yInterval;
    [SerializeField]
    private int _numberOfIconsPerRow;
    
    private DisplayIcon.Pool _iconPool;
    private Dictionary<Sprite, DisplayIcon> _icons = new Dictionary<Sprite, DisplayIcon>();

    [Inject]
    public void Construct(DisplayIcon.Pool iconPool)
    {
        _iconPool = iconPool;
    }    

    public void AddIcon(Sprite sprite)
    {
       if (!_icons.ContainsKey(sprite))
            _icons.Add(sprite, _iconPool.Spawn(sprite, GetPosition(_icons.Count)));
    }

    public void RemoveIcon(Sprite sprite)
    {
        if (_icons.ContainsKey(sprite))
        {
            _iconPool.Despawn(_icons[sprite]);
            _icons.Remove(sprite);

            UpdateIcons();
        }
    }

    private void UpdateIcons()
    {
        int counter = 0;
        foreach (KeyValuePair<Sprite, DisplayIcon> entry in _icons)
        {
            entry.Value.UpdateDetails(entry.Key, GetPosition(counter));
            counter++;
        }
    }

    private Vector2 GetPosition(int index)
    {
        return new Vector2(GetXPosition(index), GetYPosition(index));
    }

    private float GetXPosition(int index)
    {
        return _rootPosition.x                            /// The position from which to start the row
               + index % _numberOfIconsPerRow             /// Calculate the position on the row
               * _xInterval                               /// Multiply the position on row by the interval between icons
               + (GetRow(index) % 2 * _xOddRowIndent);    /// Add an indentation if the Icon is on an odd numbered row (and the indentation is not set to 0)
    }

    private float GetYPosition(int index)
    {
        return _rootPosition.y 
               + GetRow(index) * _yInterval;
    }

    private int GetRow(int index)
    {
        return index / _numberOfIconsPerRow;
    }
}

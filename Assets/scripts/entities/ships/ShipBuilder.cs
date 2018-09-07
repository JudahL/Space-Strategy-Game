using UnityEngine;
using Zenject;

public class ShipBuilder : MonoBehaviour
{
    public ShipInfo[] Ships;

    private Camera _camera;
    private Ship.Factory _factory;
    private int _teamId;

    [Inject]
    public void Construct(Ship.Factory factory, int teamId, Camera camera)
    {
        _factory = factory;
        _camera = camera;
        _teamId = teamId;
    }

    private void Start()
    {
        BuildShips();
    }

    private void BuildShips()
    {
        for (int i = 0; i < Ships.Length; i++)
        {
            Build(Ships[i], GetSpawnPosition(i));
        }
    }

    private void Build(ShipInfo shipInfo, Vector3 position)
    {
        Ship ship = _factory.Create(shipInfo, position);
        ship.ShipTransform.gameObject.layer = gameObject.layer;
    }

    private Vector3 GetSpawnPosition(int index)
    {
        //Generate the x coordinate for spawning based on a viewport position.
        //The x positions are evenly spaced out across the width of the screen.
        Vector3 globalSpawnPos = _camera.ViewportToWorldPoint(new Vector3((index + 1f) / (Ships.Length + 1f), 0f, 0f));

        //Convert the global spawn position's X coord to local position (from the camera's perspective).
        //Adding a small multiplier (x1.2) to space them out a bit more. 
        //Setting Y and Z to 0 as it should inherit those values from the Ship's parent transform.
        Vector3 localSpawnPos = new Vector3((globalSpawnPos.x-_camera.transform.position.x) * 1.2f, 0f, 0f);

        return localSpawnPos;
    }
}

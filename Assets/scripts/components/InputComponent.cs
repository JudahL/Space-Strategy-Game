using UnityEngine;

public class InputComponent : MonoBehaviour
{

    public RaycastTargetSystem TouchListener; //TODO: Change to a more modular dependency (ie unity event listeners)

    private void Update()
    {
        CheckTouch();
    }

    private void CheckTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Touch();
        }
    }

    private void Touch()
    {
        TouchListener.OnInput(Input.mousePosition);
    }
    	
}

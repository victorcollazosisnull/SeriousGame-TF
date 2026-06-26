using UnityEngine;
using UnityEngine.InputSystem;

public class HelicopterInput : MonoBehaviour
{
    public Vector2 Move { get; private set; }
    public float Throttle { get; private set; }
    public float Yaw { get; private set; }
    public void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }
    public void OnThrottle(InputAction.CallbackContext context)
    {
        Throttle = context.ReadValue<float>();
    }
    public void OnYaw(InputAction.CallbackContext context)
    {
        Yaw = context.ReadValue<float>();
    }
}

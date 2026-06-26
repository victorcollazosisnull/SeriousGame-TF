using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HelicopterPhysics : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HelicopterInput input;

    [Header("Lift")]
    [SerializeField] private float liftForce = 15000f;
    [SerializeField] private float gravity = 9.81f;

    [Header("Rotation")]
    [SerializeField] private float maxPitchAngle = 20f;
    [SerializeField] private float maxRollAngle = 20f;
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private float yawSpeed = 2f;

    [Header("Stability")]
    [SerializeField] private float angularDrag = 4f;
    [SerializeField] private float maxAngularVelocity = 2f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (input == null)
            input = GetComponent<HelicopterInput>();

        rb.useGravity = false;
        rb.angularDamping = angularDrag;
        rb.maxAngularVelocity = maxAngularVelocity;
    }

    private void FixedUpdate()
    {
        ApplyGravity();

        HandleLift();

        HandleRotation();
    }

    private void ApplyGravity()
    {
        rb.AddForce(Vector3.down * gravity * rb.mass, ForceMode.Force);
    }

    private void HandleLift()
    {
        rb.AddForce(transform.up * input.Throttle * liftForce, ForceMode.Force);
    }

    private void HandleRotation()
    {
        Vector2 move = input.Move;

        float targetPitch = -move.y * maxPitchAngle;
        float targetRoll = move.x * maxRollAngle;

        Quaternion targetRotation = Quaternion.Euler(targetPitch, rb.rotation.eulerAngles.y, targetRoll);

        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));

        rb.AddRelativeTorque(Vector3.up * input.Yaw * yawSpeed, ForceMode.Acceleration);
    }
}
using InputManagement;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private bool usesBuffer;
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private int priority = 0;

    private bool IsGrounded()
    {
        return Physics2D.OverlapPoint(groundCheckPosition.position);
    }

    private void Update()
    {
        if (usesBuffer)
        {
            if (IsGrounded() && inputManager.Input.jump.TryUseBuffer(priority))
            {
                Jump();
            }
        }
        else
        {
            if (IsGrounded() && inputManager.Input.jump.GetPressedThisFrame())
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(0, jumpHeight);
    }
}

using UnityEngine;

public class Player1 : PlayerControl
{
    public float isGroundRad = 0.2f;

    protected override void MovePlayer()
    {
        var direction = 0;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            direction = Input.GetKey(KeyCode.D) ? 1 : -1;
            Flip(direction);
            SetAnimationRun(true);
        }
        else
            SetAnimationRun(false);

        var velocity = new Vector2(direction * speed * Time.fixedDeltaTime, rb.velocity.y);
        if (state == PlayerState.grounded && Input.GetButton("Jump"))
        {
            velocity.y = jumpForce;
            state = PlayerState.jumped;
            SetAnimationJump(true);
        }
        rb.velocity = transform.TransformDirection(velocity);
    }

    protected override void UpdateTexture()
    {
        
    }

    private void SetAnimationRun(bool on)
    {
        animator.SetBool("isRunning", on);
    }

    private void SetAnimationJump(bool on)
    {
        animator.SetBool("isJumping", on);
    }

    protected override void Flip(int direction)
    {
        transform.localScale = new Vector3(direction, 1, 1);
    }

    protected override void CheckCollisions()
    {
        var colliders = Physics2D.OverlapCircleAll(legs.position, isGroundRad);
        foreach (var c in colliders)
        {
            if (c.gameObject.CompareTag("Ground")){
                state = PlayerState.grounded;
                SetAnimationJump(false);
                break;
            }
        }
    }
}
using UnityEngine;

public class ParallaxMovement
{
    private readonly IParallaxMover _parallaxMover;
    private readonly float _jumpSpeedMultiplier;
    private readonly float _maxJumpSpeed;

    public ParallaxMovement(IParallaxMover parallaxMover, float jumpSpeedMultiplier, float maxJumpSpeed)
    {
        _parallaxMover = parallaxMover;
        _jumpSpeedMultiplier = jumpSpeedMultiplier;
        _maxJumpSpeed = maxJumpSpeed;
    }

    public void MoveUp(float jumpSpeed)
    {
        float parallaxSpeed = Mathf.Clamp(jumpSpeed * _jumpSpeedMultiplier, 0, _maxJumpSpeed);
        _parallaxMover.Move(Vector2.up, parallaxSpeed);
    }

    public void MoveDown(float jumpSpeed)
    {
        float parallaxSpeed = Mathf.Clamp(jumpSpeed * _jumpSpeedMultiplier, 0, _maxJumpSpeed);
        _parallaxMover.Move(Vector2.down, parallaxSpeed);
    }

    public void MoveRight(float speed = 0.1f)
    {
        _parallaxMover.Move(Vector2.right, speed);
    }

    public void MoveLeft(float speed = 0.1f)
    {
        _parallaxMover.Move(Vector2.left, speed);
    }

    public void Stop()
    {
        _parallaxMover.Stop();
    }
}
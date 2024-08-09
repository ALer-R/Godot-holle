using Godot;
using System;
using System.Reflection.Metadata;

public partial class PlayerDashState : PlayerState
{
    [Export] private Timer dashTimerNode;
    [Export(PropertyHint.Range, "0,20,0.1")] private float speed = 10;

    public override void _Ready()
    {
        base._Ready();
        dashTimerNode.Timeout += HandleDashTimerTimeout;
    }

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_DASH);
        characterNode.Velocity = new(characterNode.direction.X, 0, characterNode.direction.Y);
        if (characterNode.Velocity == Vector3.Zero)
        {
            characterNode.Velocity = characterNode.SpriteNode.FlipH ? Vector3.Left : Vector3.Right;
        }
        characterNode.Velocity *= speed;
        dashTimerNode.Start();
    }


    public override void _PhysicsProcess(double delta)
    {
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }
    private void HandleDashTimerTimeout()
    {
        characterNode.Velocity = Vector3.Zero;
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }
}

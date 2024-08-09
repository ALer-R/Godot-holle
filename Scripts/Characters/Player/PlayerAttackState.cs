using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    [Export] private Timer comboTimerNode;

    private int comboCounter = 1;
    private int maxComboCount = 2;
    public override void _Ready()
    {
        base._Ready();

        comboTimerNode.Timeout += () => comboCounter = 1;
    }
    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(
            GameConstants.ANIM_ATTACK + comboCounter,
            -1,
            1.5f
        );

        characterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;

    }

    protected override void ExitState()
    {
        characterNode.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;
        comboTimerNode.Start();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        comboCounter++;
        comboCounter = Mathf.Wrap(comboCounter, 1, maxComboCount + 1);
        characterNode.ToggleHitbox(true);
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();

    }

    public void PerformHit()
    {
        Vector3 newPosition = characterNode.SpriteNode.FlipH ? Vector3.Left : Vector3.Right;
        characterNode.HitboxNode.Position = newPosition;

        characterNode.ToggleHitbox(false);
    }

}

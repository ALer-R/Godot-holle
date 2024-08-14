using Godot;
using System;

public partial class EnemyStunState : EnemyState
{
    protected override void EnterState()
    {
        base.EnterState();

        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_STUN);
        characterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
    }
    protected override void ExitState()
    {
        characterNode.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        if (characterNode.AttackAreaNode.HasOverlappingBodies())
        {
            characterNode.StateMachineNode.SwitchState<EnemyAttaskState>();
        }
        else if (characterNode.ChaseAreaNode.HasOverlappingBodies())
        {
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
        }
        else
        {
            characterNode.StateMachineNode.SwitchState<EnemyIdleState>();
        }
    }

}

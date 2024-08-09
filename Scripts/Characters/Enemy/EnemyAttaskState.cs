using Godot;
using System;
using System.Linq;

public partial class EnemyAttaskState : EnemyState
{

    private Vector3 targetPosition;


    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK);

        Node3D target = characterNode.AttackAreaNode.GetOverlappingBodies().First();
        targetPosition = target.GlobalPosition;
        characterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        characterNode.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;
    }
    private void HandleAnimationFinished(StringName animName)
    {
        characterNode.ToggleHitbox(true);
        Node3D target = characterNode.AttackAreaNode.GetOverlappingBodies().FirstOrDefault();
        if (target == null)
        {
            Node3D chaseTarget = characterNode.ChaseAreaNode.GetOverlappingBodies().FirstOrDefault();
            if (chaseTarget != null)
            {
                characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
                return;
            }
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
            return;
        }

        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK);
        targetPosition = target.GlobalPosition;

        Vector3 direction = characterNode.GlobalPosition.DirectionTo(targetPosition);
        characterNode.SpriteNode.FlipH = direction.X < 0;

    }


    private void PerformHit()
    {
        characterNode.ToggleHitbox(false);
        characterNode.HitboxNode.GlobalPosition = targetPosition;
    }
}

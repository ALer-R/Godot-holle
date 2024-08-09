using Godot;
using System;

public partial class EnemyReturnState : EnemyState
{
    public override void _Ready()
    {
        base._Ready();

        destination = GetPointGlobalPosition(0);
    }

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE);
        characterNode.AgentNode.TargetPosition = destination;
        characterNode.ChaseAreaNode.BodyEntered += HandleChaesAreaBodyEntered;
    }
    protected override void ExitState()
    {
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaesAreaBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.AgentNode.IsNavigationFinished())
        {
            characterNode.StateMachineNode.SwitchState<EnemyPatrolState>();
            return;
        }
        Move();

    }
}

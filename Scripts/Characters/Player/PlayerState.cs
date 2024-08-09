using System;
using Godot;

public abstract partial class PlayerState : CharacterState
{

    public override void _Ready()
    {
        base._Ready();
        characterNode.GetStatResource(Stat.Health).OnZero += HandleStatZeroHealth;
    }

    private void HandleStatZeroHealth()
    {
        characterNode.StateMachineNode.SwitchState<PlayerDeathState>();
    }

    protected void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            characterNode.StateMachineNode.SwitchState<PlayerAttackState>();
        }
    }
}

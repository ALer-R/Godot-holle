using Godot;
using System;
using System.Reflection.Metadata;

public partial class Bomb : Ability
{
    public override void _Ready()
    {
        playerNode.AnimationFinished += HandleAnimationFinished;

    }

    private void HandleAnimationFinished(StringName animName)
    {
        if (animName == GameConstants.ANIM_EXPAND)
        {
            playerNode.Play(GameConstants.ANIM_EXPLOSION);
        }

        else
        {
            QueueFree();
        }

    }

}

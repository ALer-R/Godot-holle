using Godot;
using System;

public partial class Ligtning : Ability
{
    public override void _Ready()
    {
        playerNode.AnimationFinished += (animName) => QueueFree();
    }
}

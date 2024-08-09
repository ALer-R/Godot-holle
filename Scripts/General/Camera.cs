using System;
using System.Reflection.Metadata;
using Godot;

public partial class Camera : Camera3D
{
    [Export] private Node target;
    [Export] private Vector3 positionFromTarget;

    public override void _Ready()
    {
        GameEvents.OnStartGame += HandleStartGame;
        GameEvents.OnEndGame += HandleEndGame;

    }

    private void HandleEndGame()
    {
        Reparent(GetTree().CurrentScene);

        Position = Vector3.Zero;
    }

    private void HandleStartGame()
    {
        Reparent(target);

        Position = positionFromTarget;
    }
}
using System;
using Godot;
using System.Linq;
public abstract partial class Character : CharacterBody3D
{
    [Export] private StatResource[] statsNode;

    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimationPlayerNode { get; private set; }
    [Export] public Sprite3D SpriteNode { get; private set; }
    [Export] public StateMachine StateMachineNode { get; private set; }
    [Export] public Area3D HurtboxNode { get; private set; }
    [Export] public Area3D HitboxNode { get; private set; }
    [Export] public CollisionShape3D HitboxShapeNode { get; private set; }

    [ExportGroup("AI Nodes")]
    [Export] public Area3D ChaseAreaNode { get; private set; }
    [Export] public Path3D PathNode { get; private set; }
    [Export] public NavigationAgent3D AgentNode { get; private set; }
    [Export] public Area3D AttackAreaNode { get; private set; }

    public Vector2 direction = new();

    public override void _Ready()
    {
        HurtboxNode.AreaEntered += HurtboxAreaEntered;
    }

    private void HurtboxAreaEntered(Area3D area)
    {
        StatResource health = GetStatResource(Stat.Health);
        Character player = area.GetOwner<Character>();
        health.StatValue -= player.GetStatResource(Stat.Strength).StatValue;
    }


    public StatResource GetStatResource(Stat stat)
    {
        return statsNode.Where(x => x.StatType == stat).FirstOrDefault();
    }

    public void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;
        if (isNotMovingHorizontally) return;

        bool isMovingLift = Velocity.X < 0;
        SpriteNode.FlipH = isMovingLift;
    }

    public void ToggleHitbox(bool flag)
    {
        HitboxShapeNode.Disabled = flag;
    }

}
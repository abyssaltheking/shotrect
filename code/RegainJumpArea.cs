using Godot;
using System;

public partial class RegainJumpArea : Area2D
{
    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;

        base._Ready();
    }

    public void OnBodyEntered(Node body) {
        if (body.IsInGroup("player")) {
            Player player = GetNode<Player>(GetPathTo(body));
            player.jumps = player.maxJumps;
            player.landParticles.Emitting = true;
            player.smashing = false;
        }
    }
}

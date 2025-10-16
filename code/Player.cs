using Godot;
using System;

public partial class Player : RigidBody2D
{
    public float speed = 450;
    public float jumpPower = 450;
    public int health;
    public int maxHealth = 100;

    public int maxJumps = 2;
    public int jumps;

    private float downwardsGravity = 1.5f;
    private float smashGravity = 5f;

    public bool smashing = false;

    private Sprite2D sprite;

    public override void _Ready()
    {
        sprite = GetNode<Sprite2D>("Sprite2D");
        jumps = maxJumps;
        health = maxHealth;

        BodyEntered += OnBodyEntered;
        base._Ready();
    }

    public override void _Process(double delta)
    {
        if (health <= 0) {
            GetTree().ChangeSceneToFile("res://scenes/game.tscn");
        }

        if (Input.IsActionPressed("right")) {
            ApplyImpulse(new Vector2(speed * (float)delta, 0));
        } 

        if (Input.IsActionPressed("left")) {
            ApplyImpulse(new Vector2(-speed * (float)delta, 0));
        }

        if (Input.IsActionJustPressed("jump") && jumps > 0) {
            ApplyImpulse(new Vector2(0, -jumpPower));
            LinearDamp = 1f;
            jumps--;
        }

        if (Input.IsActionJustPressed("smash") && Mathf.Abs(LinearVelocity.Y) > 0) {
            ApplyImpulse(new Vector2(0, jumpPower * 1.25f));
            smashing = true;
        }

        if (LinearVelocity.Y > 0 && !smashing) GravityScale = downwardsGravity;
        else GravityScale = 1f;

        base._Process(delta);
    }

    public void OnBodyEntered(Node body) {
        if (body.IsInGroup("death")) health = 0;
    }
}

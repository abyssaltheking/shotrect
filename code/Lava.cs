using Godot;
using System;

public partial class Lava : StaticBody2D
{
    private float riseValue;
    public override void _Process(double delta)
    {
        riseValue += (float)delta;
        Position = new Vector2(Position.X, Position.Y + (Mathf.Sin(riseValue)/5f));
        base._Process(delta);
    }
}

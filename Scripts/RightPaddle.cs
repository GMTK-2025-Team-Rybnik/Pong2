using Godot;
using System;

public partial class RightPaddle : CharacterBody2D
{
    const float SPEED = 120f;
    const float JUMP_VELOCITY = -375.0f;

    float gravity = (float)(ProjectSettings.GetSetting("physics/2d/default_gravity"));

    public override void _PhysicsProcess(double delta)
    {
        Vector2 vel = Velocity;

        if (Input.IsKeyPressed(Key.Left)) vel.X = -1 * SPEED;
        else if (Input.IsKeyPressed(Key.Right)) vel.X = SPEED;
        else vel.X = 0;

        //dem normal grav workz
        if (IsOnFloor() && Input.IsKeyPressed(Key.Up)) vel.Y = JUMP_VELOCITY;
        if (!IsOnFloor()) vel.Y += (float)(gravity * delta);

        //Dirty paddle boundary (there be some wibration, but paddle could leave the game when riding ball)
        if (vel.X < 0 && GlobalPosition.X < 200)
        {
            vel.X = 0;
            GlobalPosition = new Vector2(200, GlobalPosition.Y);
        }
        if (vel.X > 0 && GlobalPosition.X > 520)
        {
            vel.X = 0;
            GlobalPosition = new Vector2(520, GlobalPosition.Y);
        }
        Velocity = vel;
        MoveAndSlide();
    }
}

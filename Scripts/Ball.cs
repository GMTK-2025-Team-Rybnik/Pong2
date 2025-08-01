using Godot;
using System;

public partial class Ball : CharacterBody2D
{
    int vDirection = 1, hDirection = 1;
    RayCast2D rayCastLeft, rayCastRight;
    Timer resetTimer;
    double mult = 1.00;
    const float SPEED = 300;
    int startingDirection = 1;
    private Node gameManager;
    public override void _Ready()
    {
        //Gotta get dem nodes
        rayCastLeft = GetNode<RayCast2D>("RayCastLeft");
        rayCastRight = GetNode<RayCast2D>("RayCastRight");
        resetTimer = GetNode<Timer>("ResetTimer");
        
//Game manager has unique way to acquire: needs GetTree().Root.GetNode<GameManager>("Path_From_Starting_Node_In_Scene/GameManager")
        //Otherwise NativeCalls..cs:7320 @ Godot.GodotObject Godot.NativeCalls.godot_icall_1_831(nint, nint, ...) will apear
        gameManager = GetTree().Root.GetNode<GameManager>("Node2D/GameManager");
    }

    //After time runs out at line 56, activates code
    private void OnResetTimerTimeout()
    {
        gameManager.Call("AddScore", $"{hDirection}");
        mult = 1.00;
        GlobalPosition = Vector2.Zero;
        hDirection *= -1;
    }

    public override void _PhysicsProcess(double delta)
    {
        //Le up and down movement
        Vector2 vel = Velocity;
        if (IsOnFloor() || IsOnCeiling())
        {
            vDirection *= -1;
        }
    //Side to side. Side side to side! This task a grueling one, hope to find some diamonds tonight night night! Diamonds tonight!
        if (rayCastRight.IsColliding() || rayCastLeft.IsColliding())
        {
            mult += 0.01;
            hDirection *= -1;
        }

        //Do dem maths
        vel.X = (float)(hDirection * mult * SPEED);
        vel.Y = (float)(vDirection * SPEED);
        if (GlobalPosition.X < -600 || GlobalPosition.X > 550)
        {
            mult = 0;
            GlobalPosition = new Vector2(0, 600);
            resetTimer.Start(2);
        }

        //MOOOOOOOOOOOOOOOOOVE
        Velocity = vel;
        MoveAndSlide();
    }
}
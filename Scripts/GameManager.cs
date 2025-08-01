using Godot;
using System;

public partial class GameManager : Node
{
    AudioStreamPlayer2D scored, music, bigScore;
    Label leftScore, rightScore;
    public override void _Ready()
    {
        //Getters, yada yada
        scored = GetNode<AudioStreamPlayer2D>("Scored");
        bigScore = GetNode<AudioStreamPlayer2D>("BigScore");
        music = GetNode<AudioStreamPlayer2D>("Music");
        leftScore = GetNode<Label>("LeftScore");
        rightScore = GetNode<Label>("RightScore");
    }
    int leftPoints = 0, rightPoints = 0;
    public void AddScore(int winner)
    {
        //Change dem label points
        if (winner == -1)
        {
            rightPoints++;
            if (rightPoints % 10 == 0) bigScore.Play();
            else scored.Play();
        }
        else
        {
            leftPoints++;
            if(leftPoints % 10 == 0) bigScore.Play();
            else scored.Play();
        }
        leftScore.Text = $"{leftPoints}";
        rightScore.Text = $"{rightPoints}";
    }
}
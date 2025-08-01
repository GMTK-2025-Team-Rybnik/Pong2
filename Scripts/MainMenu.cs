using Godot;
using Microsoft.VisualBasic;
using System;

public partial class MainMenu : Node2D
{
    Label label1, label2;
    AudioStreamPlayer2D _audio;
    Button creditsButton, settingsButton, bucket, back1, back2;
    VBoxContainer _main, _credits, _settings;
    public override void _Ready()
    {
        //Too many getters for me
        _audio = GetNode<AudioStreamPlayer2D>("CenterContainer/MainButtons/Bucket/BucketSound");
        _main = GetNode<VBoxContainer>("CenterContainer/MainButtons");
        creditsButton = GetNode<Button>("CenterContainer/MainButtons/Credits");
        settingsButton = GetNode<Button>("CenterContainer/MainButtons/Settings");
        _credits = GetNode<VBoxContainer>("CenterContainer/Credits");
        bucket = GetNode<Button>("CenterContainer/MainButtons/Bucket");
        _settings = GetNode<VBoxContainer>("CenterContainer/Settings");
        label1 = GetNode<Label>("Label");
        label2 = GetNode<Label>("Label2");

        ShowOnly(_main);
    }

    private void OnPlayPressed()
    {
        _main.Visible = false;
        var scene = ResourceLoader.Load<PackedScene>("res://Scenes/Game.tscn").Instantiate();
        GetTree().Root.AddChild(scene);
        label1.Visible = false;
        label2.Visible = false;
    }
    private void OnSettingsPressed()
    {
        ShowOnly(_settings);
    }
    private void OnCreditsPressed()
    {
        ShowOnly(_credits);
    }
    private void OnBucketPressed()
    {
        _audio.Play();
    }
    private void OnQuitPressed()
    {
        GetTree().Quit();
    }
    private void ShowOnly(Control toShow)
    {
        _main.Visible = false;
        _settings.Visible = false;
        _credits.Visible = false;

        toShow.Visible = true;
    }
    private void OnBackPressed()
    {
        ShowOnly(_main);
    }
}

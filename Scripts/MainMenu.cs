using Godot;
using System;

public partial class MainMenu : Node2D
{
	private Label _scoreLabel1;
	private Label _scoreLabel1Value;

	private Label _scoreLabel2;
	private Label _scoreLabel2Value;

	private Label _highScoreLabel;
	private Label _highScoreLabelValue;

	private Label _play;
	private Label _title;

	private int _score = 0;
	private int _highScore = 0;

	private Vector2 _viewportSize;
	private Timer _timer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_viewportSize = GetViewport().GetVisibleRect().Size;

		_scoreLabel1 = new();
		_scoreLabel1.Text = $"SCORE<1>";
		_scoreLabel1Value = new();
		_scoreLabel1Value.Text = $"{_score:D4}";

		_scoreLabel1.Position = new Vector2((_viewportSize.X / 2) - (_viewportSize.X / 4), 20);
		_scoreLabel1Value.Position = new Vector2((_viewportSize.X / 2) - (_viewportSize.X / 4), 50);

		AddChild(_scoreLabel1);
		AddChild(_scoreLabel1Value);

		//Set highscore label
		_highScoreLabel = new();
		_highScoreLabel.Text = $"HI-SCORE";
		_highScoreLabelValue = new();
		_highScoreLabelValue.Text = $"{_highScore:D4}";

		_highScoreLabel.Position = new Vector2((_viewportSize.X / 2) - 30, 20);
		_highScoreLabelValue.Position = new Vector2((_viewportSize.X / 2) - 30, 50);

		AddChild(_highScoreLabel);
		AddChild(_highScoreLabelValue);

		//Set score2 label
		_scoreLabel2 = new();
		_scoreLabel2.Text = $"SCORE<2>";
		_scoreLabel2Value = new();
		_scoreLabel2Value.Text = $"{_score:D4}";

		_scoreLabel2.Position = new Vector2((_viewportSize.X / 2) + (_viewportSize.X / 5), 20);
		_scoreLabel2Value.Position = new Vector2((_viewportSize.X / 2) + (_viewportSize.X / 5), 50);

		AddChild(_scoreLabel2);
		AddChild(_scoreLabel2Value);

		//-------------------
		_play = new();
		_play.Text = "PLAY";
		_play.Position = new Vector2((_viewportSize.X / 2 ) - 40, _viewportSize.Y / 4);

		_title = new();
		_title.Text = "SPACE INVADERS";
		_title.Position = new Vector2((_viewportSize.X / 2) - 75, (_viewportSize.Y / 5) * 2);

		AddChild(_play);
		AddChild(_title);

		_timer = new();
		_timer.WaitTime = 1.0f;
		_timer.Timeout += _on_timeout;

		AddChild(_timer);
		_timer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void _on_timeout()
	{
		_play.Visible = !_play.Visible;
	}
}

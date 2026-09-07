using Godot;
using System;
using System.Collections.Generic;

public partial class LevelManager : Node
{
	private PackedScene _enemySpawner = GD.Load<PackedScene>("res://Scenes/enemy.tscn");
	private List<Enemy> _enemyList;
	private PackedScene _playerSpawner = GD.Load<PackedScene>("res://Scenes/player.tscn");
	private Player _player;
	private PackedScene _bulletSpawner = GD.Load<PackedScene>("res://Scenes/bullet.tscn");
	private Vector2 _viewportSize;

	private Label _scoreTextLabel = new();
	private Label _scoreLabel = new();
	private Label _playerLivesLeftText = new();
	private Label _playerLivesLeft = new();

	private Random _random = new();

	private int _score;

	int i = 0;
	int enemiesAlive = 50;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_viewportSize = GetViewport().GetVisibleRect().Size;

		_player = _playerSpawner.Instantiate<Player>();
		AddChild(_player);
		_player.Position = new Vector2(500,500);

		_scoreTextLabel.Position = new Vector2(10,10);
		_scoreTextLabel.Text = "<SCORE>";
		_scoreLabel.Position = new Vector2(15,40);
		_scoreLabel.Text = $"{_score:D4}";
		AddChild(_scoreTextLabel);
		AddChild(_scoreLabel);

		_playerLivesLeftText.Position = new Vector2(10,100);
		_playerLivesLeftText.Text = $"<LIVES>";
		_playerLivesLeft.Position = new Vector2(10, 130);
		_playerLivesLeft.Text = $"{_player.GetLives()}";
		AddChild(_playerLivesLeftText);
		AddChild(_playerLivesLeft);

		_enemyList = new();
		PopulateEnemies();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("ui_accept"))
		{
			//shoot upwards
			Shoot(-1);
		}

		if(_random.NextDouble() < (0.01 + (0.25/enemiesAlive)))
		{
			EnemyShoot();
		}
	}

	public void PopulateEnemies()
	{
		for(int i = 0; i < 50; i++)
		{
			Enemy enemy = _enemySpawner.Instantiate<Enemy>();
			AddChild(enemy);
			enemy.Position = new Vector2((_viewportSize.X / 4) + 5 + (50 * (_enemyList.Count % 10)), 20 + (50 * (_enemyList.Count / 10)));
			enemy.SetId(i);
			_enemyList.Add(enemy);
		}
	}

	public void EnemyShoot()
	{
		GD.Print("Enemy Shoot");
		//Get column of shooter
		int randInt = _random.Next(0,9);

		//Get enemy in the lowest row of the selected column to shoot
		for(int i = 4; i >= 0; i--)
		{
			int index = randInt + (i*10);
			if(_enemyList[index].IsInsideTree())
			{
				Bullet bullet = _bulletSpawner.Instantiate<Bullet>();
				AddChild(bullet);
				//bullet.Position = new Vector2(_player.Position.X, _player.Position.Y-10);
				bullet.Position = new Vector2(_enemyList[index].Position.X, _enemyList[index].Position.Y + 50);
				bullet.SetDirection(1);

				bullet.enemyHitEvent += OnEnemyHit;
				bullet.playerHitEvent += OnPlayerHit;

				break;
			}
		}

	}

	public void Shoot(int direction)
	{
		Bullet bullet = _bulletSpawner.Instantiate<Bullet>();
		AddChild(bullet);
		bullet.Position = new Vector2(_player.Position.X, _player.Position.Y-20);
		bullet.SetDirection(direction);

		bullet.enemyHitEvent += OnEnemyHit;
		bullet.playerHitEvent += OnPlayerHit;
	}

    private void OnPlayerHit(object sender, Player e)
    {
        _player.SetLives(_player.GetLives()-1);
		_playerLivesLeft.Text = $"{_player.GetLives()}";
    }


    private void OnEnemyHit(object sender, Enemy enemy)
    {
		_score++;
		_scoreLabel.Text = $"{_score:D4}";
		//_enemyList.Remove(enemy);
		RemoveChild(enemy);
		enemiesAlive--;
    }
}

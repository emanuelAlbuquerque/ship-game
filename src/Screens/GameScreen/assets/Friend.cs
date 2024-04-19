using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Friend : GameObject
{
  public Bullet _bullet;
  private Rectangle[] _frames;
  private int _index;
  private Timer _timer;
  public Timer _timerGenerate;
  public bool _isVisible = false;
  private float SPEED_X = 100;

  public Friend(Texture2D image) : base(image)
  {
    int frameWidth = 44;
    int totalFrames = 528 / frameWidth;

    _frames = new Rectangle[totalFrames];

    for (int i = 0; i < totalFrames; i++)
    {
      int frameX = i * frameWidth;
      _frames[i] = new Rectangle(frameX, 0, frameWidth, 51);
    }
  }

  public override void Initialize()
  {
    _bounds.Width = _bounds.Width / _frames.Length;
    _bounds.X = 0;
    _bounds.Y = Globals.SCREEN_HEIGHT - _bounds.Height;
    _index = 0;
    _timer = new Timer();
    _timer.Start(IncraseIndex, 0.2f, true);
    _timerGenerate = new Timer();
    _timerGenerate.Start(Generate, 3.0f, true);
  }

  public override void Update(float deltaTime)
  {
    if (_isVisible)
    {
      _bounds.X = _bounds.X + (int)(SPEED_X * deltaTime);

      if (_bounds.X > Globals.SCREEN_WIDTH)
      {
        _isVisible = false;
      }

      _timer.Update(deltaTime);
    }

    _timerGenerate.Update(deltaTime);
  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    if (_isVisible)
    {
      spriteBatch.Draw(_image, _bounds, _frames[_index], Color.White);
    }
  }

  private void IncraseIndex()
  {
    _index++;
    if (_index > 11)
    {
      _index = 0;
    }
  }

  private void Generate()
  {
    if (!_isVisible)
    {
      _bounds.X = 0;
      _bounds.Y = Globals.SCREEN_HEIGHT - _bounds.Height;
      _isVisible = true;
    }
  }

  public void CheckCollision(SecondEnemy _secondEnemy, Action<Rectangle> _callbackFriendCollisionEnemy)
  {
    if (_bounds.Intersects(_secondEnemy.Bounds) && _secondEnemy._isVisible && _isVisible)
    {
      _timerGenerate.Reset();
      _isVisible = false;
      _callbackFriendCollisionEnemy.Invoke(_bounds);
    }
  }

  public void ResetLocation()
  {
    _bounds.X = 0;
    _bounds.Y = Globals.SCREEN_HEIGHT - _bounds.Height;
  }
}
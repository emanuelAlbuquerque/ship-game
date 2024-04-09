using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class FiristEnemy : GameObject
{
  private float SPEED_X = 250;
  public bool _isVisible = false;
  public Timer _timerGenerate;
  private Rectangle[] _frames;
  private int _index;
  private Timer _timerAnimation;

  public FiristEnemy(Texture2D image) : base(image)
  {
    _frames = new Rectangle[2]
    {
      new Rectangle(0, 0, 256, 67), new Rectangle(256, 0, 256, 67)
    };
  }

  public override void Initialize()
  {
    _bounds.Width = _bounds.Width / _frames.Length;
    _bounds.Y = new Random().Next(0, Globals.SCREEN_HEIGHT - _bounds.Height);
    _bounds.X = Globals.SCREEN_WIDTH - _bounds.Width;
    _isVisible = true;
    _index = 0;
    _timerAnimation = new Timer();
    _timerAnimation.Start(IncraseIndex, 0.2f, true);
    _timerGenerate = new Timer();
    _timerGenerate.Start(Generate, 5.0f, true);
  }

  public override void Update(float deltaTime)
  {
    if (_isVisible)
    {
      _bounds.X = _bounds.X - (int)(SPEED_X * deltaTime);

      if (_bounds.X < 0)
      {
        _bounds.Y = new Random().Next(0, Globals.SCREEN_HEIGHT - _bounds.Height);
        _bounds.X = Globals.SCREEN_WIDTH - _bounds.Width;
      }

      _timerAnimation.Update(deltaTime);
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

  public void Generate()
  {
    if (!_isVisible)
    {
      _bounds.Y = new Random().Next(0, Globals.SCREEN_HEIGHT - _bounds.Height);
      _bounds.X = Globals.SCREEN_WIDTH - _bounds.Width;
      _isVisible = true;
    }
  }

  public void IncraseIndex()
  {
    _index++;
    if (_index > 1)
    {
      _index = 0;
    }
  }

}
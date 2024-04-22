using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FiristEnemy : GameObject
{
  private float SPEED_X = 250;
  public bool _isVisible = false;
  public Timer _timerGenerate;
  private Rectangle[] _frames;
  private int _index;
  private Timer _timerAnimation;
  private bool _initialized = false;

  public FiristEnemy(Texture2D image) : base(image)
  {
    _frames = new Rectangle[2]
    {
      new Rectangle(0, 0, 256, 67), new Rectangle(256, 0, 256, 67)
    };
  }

  public void Initialize(GameObject _secondEnemy)
  {
    if (!_initialized)
    {
      _bounds.Width = _bounds.Width / _frames.Length;
      _initialized = true;
    }
    _bounds.Y = new Random().Next(0, Globals.SCREEN_HEIGHT - _bounds.Height - _secondEnemy.Bounds.Height);
    _bounds.X = Globals.SCREEN_WIDTH;
    _isVisible = true;
    _index = 0;
    _timerAnimation = new Timer();
    _timerAnimation.Start(IncraseIndex, 0.2f, true);
    _timerGenerate = new Timer();
    _timerGenerate.Start(() => Generate(_secondEnemy), 3.0f, true);
  }

  public void Update(float deltaTime, GameObject _secondEnemy, GameScreen gameScreen)
  {
    if (_isVisible)
    {
      _bounds.X = _bounds.X - (int)(SPEED_X * deltaTime);

      if (_bounds.X + _image.Width < 0 )
      {
        _bounds.Y = new Random().Next(0, Globals.SCREEN_HEIGHT - _bounds.Height - _secondEnemy.Bounds.Height);
        _bounds.X = Globals.SCREEN_WIDTH;
        gameScreen.DecreasePoints(50);
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

  private void Generate(GameObject _secondEnemy)
  {
    if (!_isVisible)
    {
      _bounds.Y = new Random().Next(0, Globals.SCREEN_HEIGHT - _bounds.Height - _secondEnemy.Bounds.Height);
      _bounds.X = Globals.SCREEN_WIDTH;
      _isVisible = true;
    }
  }

  private void IncraseIndex()
  {
    _index++;
    if (_index > 1)
    {
      _index = 0;
    }
  }

  public void ResetLocation()
  {
    _bounds.Y = new Random().Next(0, Globals.SCREEN_HEIGHT - _bounds.Height);
    _bounds.X = Globals.SCREEN_WIDTH;
  }
}
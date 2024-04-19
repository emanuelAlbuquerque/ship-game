using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SecondEnemy : GameObject
{
  private float SPEED_X = 170;
  public bool _isVisible = false;
  public Timer _timerGenerate;

  public SecondEnemy(Texture2D image) : base(image)
  {
  }

  public override void Initialize()
  {
    _bounds.Y = Globals.SCREEN_HEIGHT - _bounds.Height;
    _bounds.X = Globals.SCREEN_WIDTH;
    _isVisible = true;
    _timerGenerate = new Timer();
    _timerGenerate.Start(Generate, 2.0f, true);
  }

  public override void Update(float deltaTime)
  {
    if (_isVisible)
    {
      _bounds.X = _bounds.X - (int)(SPEED_X * deltaTime);

      if (_bounds.X < 0)
      {
        _isVisible = false;
      }
    }

    _timerGenerate.Update(deltaTime);
  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    if (_isVisible)
    {
      spriteBatch.Draw(_image, _bounds, Color.White);
    }
  }

  private void Generate()
  {
    if (!_isVisible)
    {
      _bounds.Y = Globals.SCREEN_HEIGHT - _bounds.Height;
      _bounds.X = Globals.SCREEN_WIDTH;
      _isVisible = true;
    }
  }

  public void ResetLocation()
  {
    _bounds.Y = Globals.SCREEN_HEIGHT - _bounds.Height;
    _bounds.X = Globals.SCREEN_WIDTH;
  }
}
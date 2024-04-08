using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class FiristEnemy : GameObject
{
  private float SPEED_X = 200;
  private SpriteEffects _orientation;
  public bool _isVisible = false;
  public Timer _timer;

  public FiristEnemy(Texture2D image) : base(image)
  {
  }

  public override void Initialize()
  {
    _orientation = SpriteEffects.None;
    _timer = new Timer();
    _timer.Start(Generate, 5.0f, true);
  }

  public override void Update(float deltaTime)
  {
    if (_isVisible)
    {
      _bounds.X = _bounds.X - (int)(SPEED_X * deltaTime);

      if (_bounds.X < 0)
      {
        _bounds.Y = new Random().Next(0, Globals.SCREEN_HEIGHT);
        _bounds.X = Globals.SCREEN_WIDTH - _bounds.Width;
      }
    }

    _timer.Update(deltaTime);
  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    if (_isVisible)
    {
      spriteBatch.Draw(_image, _bounds, null, Color.White, 0, Vector2.Zero, _orientation, 0);
    }
  }

  public void Generate()
  {
    if (!_isVisible)
    {
      _bounds.Y = new Random().Next(0, Globals.SCREEN_HEIGHT);
      _bounds.X = Globals.SCREEN_WIDTH - _bounds.Width;
      _isVisible = true;
    }
  }
}
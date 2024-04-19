using System;
using Microsoft.Xna.Framework.Graphics;

public class Background : GameObject
{
  private float _speedX = 100;

  public Background(Texture2D image) : base(image)
  {
  }

  public override void Update(float deltaTime)
  {
    _bounds.X -= (int)(_speedX * deltaTime);

    if (Math.Abs(_bounds.X) + Globals.SCREEN_WIDTH == _bounds.Width)
    {
      _bounds.X = 0;
    }
  }
}

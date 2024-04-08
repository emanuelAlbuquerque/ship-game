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
    // Move o background na direção horizontal com base na velocidade e no tempo decorrido
    _bounds.X -= (int)(_speedX * deltaTime);

    // Se o background sair completamente da tela pela esquerda, reposiciona-o à direita para criar um loop
    if (Math.Abs(_bounds.X) + Globals.SCREEN_WIDTH == _bounds.Width)
    {
      _bounds.X = 0;
    }
  }
}

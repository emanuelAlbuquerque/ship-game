using Microsoft.Xna.Framework.Graphics;

public class Background : GameObject
{
  private float _speedX;

  public Background(Texture2D image, float speedX) : base(image)
  {
    _speedX = speedX;
  }

  public override void Update(float deltaTime)
  {
    // Move o background na direção horizontal com base na velocidade e no tempo decorrido
    _bounds.X -= (int)(_speedX * deltaTime);

    // Se o background sair completamente da tela pela esquerda, reposiciona-o à direita para criar um loop
    if (_bounds.Right <= 0)
    {
      _bounds.X = _image.Width - 1; // Subtrai 1 para evitar uma possível linha visível na tela
    }
  }
}

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class CreditsScreen : IScreen
{
  private GameObject _background;
  private GameObject _credits;
  private Button _backButton;

  public void Initialize(int _points, int _saveds, int _losts)
  {
    _background.Initialize();
    _credits.Initialize();
    _credits.X = (Globals.SCREEN_WIDTH / 2) - _credits.Bounds.Width / 2;
    _credits.Y = 80;

    _backButton.Initialize(105, 45, (Globals.SCREEN_WIDTH / 2) - (105 / 2), 320);
  }

  public void LoadContent(ContentManager content)
  {
    Texture2D _backgroundTexture = content.Load<Texture2D>("background");
    _background = new GameObject(_backgroundTexture);

    Texture2D _creditsTexture = content.Load<Texture2D>("credits");
    _credits = new GameObject(_creditsTexture);

    Texture2D _backButtonTexture = content.Load<Texture2D>("back-button");
    _backButton = new Button(_backButtonTexture, Back);
  }

  public void Update(float deltaTime)
  {
    _backButton.Update(deltaTime);
  }

  public void Draw(SpriteBatch _spriteBatch)
  {
    _background.Draw(_spriteBatch);
    _credits.Draw(_spriteBatch);
    _backButton.Draw(_spriteBatch);
  }

  public void Back()
  {
    Globals.GameInstance.ChangeScreen(EScreen.Menu);
  }

  public (int saveds, int points, int losts) GetParameters()
  {
    return (0, 0, 0);
  }
}
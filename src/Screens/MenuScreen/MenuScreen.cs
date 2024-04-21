using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class MenuScreen : IScreen
{
  private GameObject _background;
  private GameObject _instructions;
  private Button _playButton;
  private Button _exitButton;
  private Button _creditsButton;

  public void Initialize(int _points, int _saveds, int _losts)
  {
    _background.Initialize();
    _instructions.Initialize();
    _instructions.X = (Globals.SCREEN_WIDTH / 2) - _instructions.Bounds.Width / 2;
    _instructions.Y = 80;

    _playButton.Initialize(105, 45, 200, 300);
    _creditsButton.Initialize(105, 45, 350, 300);
    _exitButton.Initialize(105, 45, 500, 300);
  }

  public void LoadContent(ContentManager content)
  {
    Texture2D _backgroundTexture = content.Load<Texture2D>("background");
    _background = new GameObject(_backgroundTexture);

    Texture2D _instructionsTexture = content.Load<Texture2D>("instructions");
    _instructions = new GameObject(_instructionsTexture);

    Texture2D _playButtonTexture = content.Load<Texture2D>("play-button");
    _playButton = new Button(_playButtonTexture, Play);

    Texture2D _endButtonTexture = content.Load<Texture2D>("end-button");
    _exitButton = new Button(_endButtonTexture, Exit);

    Texture2D _creditsButtonTexture = content.Load<Texture2D>("credits-button");
    _creditsButton = new Button(_creditsButtonTexture, Credits);
  }

  public void Update(float deltaTime)
  {
    _playButton.Update(deltaTime);
    _exitButton.Update(deltaTime);
    _creditsButton.Update(deltaTime);
  }

  public void Draw(SpriteBatch _spriteBatch)
  {
    _background.Draw(_spriteBatch);
    _instructions.Draw(_spriteBatch);
    _playButton.Draw(_spriteBatch);
    _exitButton.Draw(_spriteBatch);
    _creditsButton.Draw(_spriteBatch);
  }

  public void Play()
  {
    Globals.GameInstance.ChangeScreen(EScreen.Game);
  }

  public void Exit()
  {
    Globals.GameInstance.Exit();
  }

  public void Credits()
  {
    Globals.GameInstance.ChangeScreen(EScreen.Credits);
  }

  public (int saveds, int points, int losts) GetParameters()
  {
    return (0, 0, 0);
  }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class GameOverScreen : IScreen
{
  private GameObject _background;
  private GameObject _instructions;
  private Button _playButton;
  private Button _exitButton;
  private SpriteFont _font;
  private int points = 0;
  private int saveds = 0;
  private int losts = 0;

  public void Initialize(int _points, int _saveds, int _losts)
  {
    points = _points;
    saveds = _saveds;
    losts = _losts;
    _background.Initialize();
    _instructions.Initialize();
    _instructions.X = (Globals.SCREEN_WIDTH / 2) - _instructions.Bounds.Width / 2;
    _instructions.Y = 80;

    _playButton.Initialize(204, 45, 220, 320);
    _exitButton.Initialize(105, 45, 470, 320);
  }

  public void LoadContent(ContentManager content)
  {
    Texture2D _backgroundTexture = content.Load<Texture2D>("background");
    _background = new GameObject(_backgroundTexture);

    Texture2D _instructionsTexture = content.Load<Texture2D>("game-over-background");
    _instructions = new GameObject(_instructionsTexture);

    _font = content.Load<SpriteFont>("arial24");

    Texture2D _playButtonTexture = content.Load<Texture2D>("play-again-button");
    _playButton = new Button(_playButtonTexture, Play);

    Texture2D _endButtonTexture = content.Load<Texture2D>("end-button");
    _exitButton = new Button(_endButtonTexture, Exit);
  }

  public void Update(float deltaTime)
  {
    _playButton.Update(deltaTime);
    _exitButton.Update(deltaTime);
  }

  public void Draw(SpriteBatch _spriteBatch)
  {
    _background.Draw(_spriteBatch);
    _instructions.Draw(_spriteBatch);
    _playButton.Draw(_spriteBatch);
    _exitButton.Draw(_spriteBatch);
    _spriteBatch.DrawString(_font, string.Format("Quantidade de Pontos: {0}", points), new Vector2((Globals.SCREEN_WIDTH / 2) - _instructions.Bounds.Width / 4, 180), Color.Black);
    _spriteBatch.DrawString(_font, string.Format("Quantidade de salvamentos: {0}", saveds), new Vector2((Globals.SCREEN_WIDTH / 2) - _instructions.Bounds.Width / 4, 210), Color.Black);
    _spriteBatch.DrawString(_font, string.Format("Quantidade de percas: {0}", losts), new Vector2((Globals.SCREEN_WIDTH / 2) - _instructions.Bounds.Width / 4, 240), Color.Black);
  }

  public void Play()
  {
    Globals.GameInstance.ChangeScreen(EScreen.Game);
  }

  public void Exit()
  {
    Globals.GameInstance.Exit();
  }

  public (int saveds, int points, int losts) GetParameters()
  {
    return (saveds, points, losts);
  }
}
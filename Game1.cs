using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ship_game;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private GameObject _background;

    private Player _player;

    private FiristEnemy _firistEnemy;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        Globals.SCREEN_WIDTH = _graphics.PreferredBackBufferWidth;
        Globals.SCREEN_HEIGHT = _graphics.PreferredBackBufferHeight;

        _player.Initialize();
        _firistEnemy.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Texture2D backgroundImage = Content.Load<Texture2D>("background");
        _background = new Background(backgroundImage);

        Texture2D playerImage = Content.Load<Texture2D>("helicopter");
        Texture2D bulletPlayer = Content.Load<Texture2D>("bullet");
        _player = new Player(playerImage, bulletPlayer);

        Texture2D firistEnemy = Content.Load<Texture2D>("inimigo1");
        _firistEnemy = new FiristEnemy(firistEnemy);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _background.Update(deltaTime);
        _player.Update(deltaTime);
        _firistEnemy.Update(deltaTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _background.Draw(_spriteBatch);
        _player.Draw(_spriteBatch);
        _firistEnemy.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

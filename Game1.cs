using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
    private Explosion _explosion;
    private Sounds _sounds;


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
        _explosion.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _sounds = new Sounds();
        _sounds.LoadContent(Content);

        Texture2D _backgroundTexture = Content.Load<Texture2D>("background");
        _background = new Background(_backgroundTexture);

        Texture2D _playerTexture = Content.Load<Texture2D>("helicopter");
        Texture2D _bulletPlayerTexture = Content.Load<Texture2D>("bullet");
        _player = new Player(_playerTexture, _bulletPlayerTexture);

        Texture2D _firistEnemyTexture = Content.Load<Texture2D>("inimigo1");
        _firistEnemy = new FiristEnemy(_firistEnemyTexture);

        Texture2D _explosionTexture = Content.Load<Texture2D>("explosao");
        _explosion = new Explosion(_explosionTexture);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _background.Update(deltaTime);
        _player.Update(deltaTime, _sounds);
        _firistEnemy.Update(deltaTime);
        _explosion.Update(deltaTime);
        _player._bullet.CheckCollision(_firistEnemy, CallbackExplosionFiristEnemy);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _background.Draw(_spriteBatch);
        _player.Draw(_spriteBatch);
        _firistEnemy.Draw(_spriteBatch);
        _explosion.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void CallbackExplosionFiristEnemy()
    {
        if (_firistEnemy._isVisible && _player._bullet.isVisible)
        {
            _sounds.ExecuteSoundExplosion();
            _explosion.Position = _firistEnemy.Position;
            _firistEnemy._isVisible = false;
            _explosion._isVisible = true;
            _player._bullet.isVisible = false;
            _player._bullet.Position = new Point(0, 0);
        }
    }
}

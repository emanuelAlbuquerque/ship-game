using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameObject
{
    protected Rectangle _bounds;
    protected Texture2D _image;

    public Rectangle Bounds
    {
        get { return _bounds; }
    }

    public int X
    {
        get { return _bounds.X; }
        set { _bounds.X = value; }
    }

    public int Y
    {
        get { return _bounds.Y; }
        set { _bounds.Y = value; }
    }

    public Point Position
    {
        get { return _bounds.Location; }
        set { _bounds.Location = value; }
    }

    public GameObject(Texture2D image)
    {
        _image = image;
        _bounds = new Rectangle(0, 0, _image.Width, _image.Height);
    }

    public virtual void Initialize()
    {

    }

    public virtual void Update(float deltaTime)
    {

    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_image, _bounds, Color.White);
    }
}

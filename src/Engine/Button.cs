using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Button : GameObject
{
    private Action _callback;
    private MouseState _previousMouseState;

    public Button(Texture2D image, Action callback) : base(image)
    {
        _callback = callback;
        _previousMouseState = Mouse.GetState();
    }

    public void Initialize(int _width, int _heigth, int _x, int _y)
    {
        _bounds.Width = _width;
        _bounds.Height = _heigth;
        _bounds.X = _x;
        _bounds.Y = _y;
    }

    public override void Update(float deltaTime)
    {
        MouseState mouseState = Mouse.GetState();
        if (mouseState.LeftButton == ButtonState.Released)
        {
            if(_previousMouseState.LeftButton == ButtonState.Pressed && _bounds.Contains(_previousMouseState.Position))
            {
                if (_bounds.Contains(mouseState.X, mouseState.Y))
                {
                    _callback.Invoke();
                }
            }
        }
        _previousMouseState = mouseState;
    }
}

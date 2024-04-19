using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Button : GameObject
{
    private Action _callback;

    public Button(Texture2D image, Action callback) : base (image)
    {
        _callback = callback;
    }

    public override void Update(float deltaTime)
    {
        MouseState mouseState = Mouse.GetState();
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            if (_bounds.Contains(mouseState.X, mouseState.Y))
            {
                _callback.Invoke();
            }
        }
    }
}

using Snake.Game.Enums;
using System;

namespace Snake.Game.Menu.Interface
{
    public interface ICanvas
    {
        CanvasEnum Canvas { get; set; }
        int CountOption { get; set; }
        Action Render { get; set; }
        Action Action { get; set; }
    }
}

using SiliconStudio.Core;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox;
using SiliconStudio.Paradox.UI;
using SiliconStudio.Paradox.UI.Controls;
using SiliconStudio.Paradox.UI.Panels;

namespace LudumDare32
{
    public class UiGameHud : ScriptContext
    {
        private ImageElement healthBar;

        public UiGameHud(IServiceRegistry registry)
            : base(registry)
        {
        }

        private Canvas hudCanvas;

        public void LoadContent()
        {
            healthBar = new ImageElement();
            healthBar.SetCanvasAbsolutePosition(new Vector3(0.1f, 0.1f, 0.1f));
            healthBar.BackgroundColor = Color.DarkRed;

            hudCanvas = new Canvas();
            hudCanvas.Children.Add(healthBar);
        }
    }
}
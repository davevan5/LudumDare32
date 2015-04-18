using SiliconStudio.Core;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox;
using SiliconStudio.Paradox.Graphics;
using SiliconStudio.Paradox.UI;
using SiliconStudio.Paradox.UI.Controls;
using SiliconStudio.Paradox.UI.Panels;

namespace LudumDare32
{
    public class UIGameBar : ScriptContext
    {
        private readonly Canvas gameCanvas;

        private ImageElement barOutline;

        private ImageElement barElement;

        public UIGameBar(IServiceRegistry registry, Canvas gameCanvas, barText)
            : base(registry)
        {
            this.gameCanvas = gameCanvas;
        }

        public void LoadContent()
        {
            var barOutlineTexture = Asset.Load<Texture>("bar_outline");
            barOutline = new ImageElement
            {
                Source = new UIImage(barOutlineTexture),
                Width = barOutlineTexture.Width,
                Height = barOutlineTexture.Height
            };
            barOutline.SetCanvasRelativePosition(new Vector3(0.1f, 0.5f, 1f));

            gameCanvas.Children.Add(barOutline);
        }
    }
}
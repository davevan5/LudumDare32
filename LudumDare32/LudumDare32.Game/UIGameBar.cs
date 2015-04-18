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

        private ImageElement barFill;

        private readonly string barTextureName;

        public UIGameBar(IServiceRegistry registry, Canvas gameCanvas, string barTextureName)
            : base(registry)
        {
            this.gameCanvas = gameCanvas;
            this.barTextureName = barTextureName;
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
            barOutline.SetCanvasRelativePosition(new Vector3(0.1f, 0.9f, 1f));

            var barFillTexture = Asset.Load<Texture>(barTextureName);
            barFill = new ImageElement
            {
                Source = new UIImage(barFillTexture),
                Width = barFillTexture.Width,
                Height = barFillTexture.Height
            };
            barFill.SetCanvasRelativePosition(new Vector3(0.1f, 0.9f, 1f));

            gameCanvas.Children.Add(barOutline);
            gameCanvas.Children.Add(barFill);
        }
    }
}
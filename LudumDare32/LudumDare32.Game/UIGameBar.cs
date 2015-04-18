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

        private float barFillMaxWidth;

        private readonly string barTextureName;

        public UIGameBar(IServiceRegistry registry, Canvas gameCanvas, string barTextureName)
            : base(registry)
        {
            this.gameCanvas = gameCanvas;
            this.barTextureName = barTextureName;
        }

        /// <summary>
        /// Set the percentage fill of this bar, value between 0-1.0f
        /// </summary>
        /// <param name="percent"></param>
        public void SetPercentFill(float percent)
        {
            if (percent < 0 || percent > 1)
                return;

            barFill.Width = barFillMaxWidth * percent;
        }

        public void LoadContent(Vector3 barPosition)
        {
            var barOutlineTexture = Asset.Load<Texture>("BarContainer");
            barOutline = new ImageElement
            {
                Source = new UIImage(barOutlineTexture),
                Width = barOutlineTexture.Width,
                Height = barOutlineTexture.Height
            };
            barOutline.SetCanvasRelativePosition(barPosition);

            var barFillTexture = Asset.Load<Texture>(barTextureName);
            barFill = new ImageElement
            {
                Source = new UIImage(barFillTexture),
                StretchType = StretchType.Fill,
                Width = barFillTexture.Width,
                Height = barFillTexture.Height
            };

            barFillMaxWidth = barFillTexture.Width;
            barFill.SetCanvasRelativePosition(Vector3.Add(barPosition, new Vector3(0.003f, 0.004f, 0f)));

            gameCanvas.Children.Add(barOutline);
            gameCanvas.Children.Add(barFill);
        }
    }
}
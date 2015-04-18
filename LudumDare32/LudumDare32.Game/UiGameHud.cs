using System;
using SiliconStudio.Core;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox;
using SiliconStudio.Paradox.Graphics;
using SiliconStudio.Paradox.UI;
using SiliconStudio.Paradox.UI.Controls;
using SiliconStudio.Paradox.UI.Panels;

namespace LudumDare32
{
    public class UiGameHud : ScriptContext
    {
        private readonly Canvas hudCanvas;

        private SpriteFont spriteFont;
        private TextBlock xpTextBlock;
        private UIGameBar healthBar;
        private UIGameBar staminaBar;

        public UiGameHud(IServiceRegistry registry)
            : base(registry)
        {
            hudCanvas = new Canvas();
        }

        public void LoadContent()
        {
            spriteFont = Asset.Load<SpriteFont>("ScoreFont");

            healthBar = new UIGameBar(Services, hudCanvas, "Health");
            healthBar.LoadContent(new Vector3(0.1f, 0.91f, 1f), new Vector3(0.005f, 0.012f, 0f));

            staminaBar = new UIGameBar(Services, hudCanvas, "Stamina");
            staminaBar.LoadContent(new Vector3(0.1f, 0.85f, 1f), new Vector3(0.006f, 0.004f, 0f));

            xpTextBlock = new TextBlock
            {
                Font = spriteFont,
                Text = "138",
                TextColor = Color.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            var xp = new ContentDecorator
            {
                Content = xpTextBlock
            };
            xp.SetCanvasRelativePosition(new Vector3(0.03f, 0.91f, 1f));
            hudCanvas.Children.Add(xp);

            UI.RootElement = hudCanvas;
        }

        private void SetCurrentXP(int xp)
        {
            xpTextBlock.Text = xp.ToString();
        }

        public void Update(float time)
        {
            SetCurrentXP((int)Math.Floor(time));
            staminaBar.SetPercentFill(1f - (time / 10));
        }
    }
}
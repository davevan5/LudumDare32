using System.Threading.Tasks;
using SiliconStudio.Core;
using SiliconStudio.Core.Diagnostics;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox;
using SiliconStudio.Paradox.Effects;
using SiliconStudio.Paradox.Games.Time;
using SiliconStudio.Paradox.Graphics;
using SiliconStudio.Paradox.UI;
using SiliconStudio.Paradox.UI.Controls;
using SiliconStudio.Paradox.UI.Panels;

namespace LudumDare32
{
    public class LudumDare32Game : Game
    {
        private Canvas gameHud;

        public LudumDare32Game()
        {
            // Target 9.1 profile by default
            GraphicsDeviceManager.PreferredGraphicsProfile = new[] {GraphicsProfile.Level_9_1};
            ConsoleLogMode = ConsoleLogMode.Always;
        }

        protected override async Task LoadContent()
        {
            await base.LoadContent();

            CreatePipeline();

            /*staminaBar = new ImageElement {Source = new UIImage(Asset.Load<Texture>("stamina"))};
            staminaBar.SetCanvasRelativeSize(new Vector3(0.1f));
            staminaBar.SetCanvasRelativePosition(new Vector3(0.8f, 0.9f, 1f));

            var uiHealth = new UIImage(Asset.Load<Texture>("health"));
            healthBar = new ImageElement {Source = uiHealth, StretchType = StretchType.Fill};
            healthBar.Width = healthBar.Source.Texture.Width;
            healthBar.Height = healthBar.Source.Texture.Height;
            healthBar.SetCanvasRelativePosition(new Vector3(0.1f, 0.9f, 1f));*/

            gameHud = new Canvas();
            var healthBar = new UIGameBar(Services, gameHud);
            healthBar.LoadContent();
            UI.RootElement = gameHud;

            // Add a custom script
            Script.Add(GameScript1);
        }

        private void CreatePipeline()
        {
            // Setup the default rendering pipeline
            RenderSystem.Pipeline.Renderers.Add(new CameraSetter(Services));
            RenderSystem.Pipeline.Renderers.Add(new RenderTargetSetter(Services) {ClearColor = Color.CornflowerBlue});
            RenderSystem.Pipeline.Renderers.Add(new ModelRenderer(Services, "LudumDare32EffectMain"));
            RenderSystem.Pipeline.Renderers.Add(new UIRenderer(Services));
        }

        private async Task GameScript1()
        {
            while (IsRunning)
            {
                // Wait next rendering frame
                await Script.NextFrame();
            }
        }
    }

    public class UIGameBar : ScriptContext
    {
        private readonly Canvas gameCanvas;

        private ImageElement barOutline;

        private ImageElement barElement;

        public UIGameBar(IServiceRegistry registry, Canvas gameCanvas)
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

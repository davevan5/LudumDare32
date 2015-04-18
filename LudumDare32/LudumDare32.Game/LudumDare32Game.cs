using System.Threading.Tasks;
using SiliconStudio.Core.Diagnostics;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox;
using SiliconStudio.Paradox.Effects;
using SiliconStudio.Paradox.Graphics;
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
}

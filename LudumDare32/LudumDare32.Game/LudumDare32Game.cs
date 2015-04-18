using System.Threading.Tasks;
using SiliconStudio.Core.Diagnostics;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox;
using SiliconStudio.Paradox.Effects;
using SiliconStudio.Paradox.Graphics;
using SiliconStudio.Paradox.UI.Panels;
using SiliconStudio.Paradox.EntityModel;
using SiliconStudio.Core;
using SiliconStudio.Paradox.Engine;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using SiliconStudio.Paradox.Input;

namespace LudumDare32
{
    public class LudumDare32Game : Game
    {
        private Canvas gameHud;
        private Entity cameraEntity;
        private Level level;
        private Player player;

        public LudumDare32Game()
        {
            // Target 9.1 profile by default
            GraphicsDeviceManager.PreferredGraphicsProfile = new[] {GraphicsProfile.Level_9_1};
            ConsoleLogMode = ConsoleLogMode.Always;
        }

        protected override async Task LoadContent()
        {
            await base.LoadContent();

            // For now lets set our virtual resolution the same as the actual resolution
            // But we may want to hard code this to some value
            VirtualResolution = new Vector3(GraphicsDeviceManager.PreferredBackBufferWidth, GraphicsDeviceManager.PreferredBackBufferHeight, 20f);
            
            // Create our camera, cause yay :D
            cameraEntity = new Entity("Camera") { 
                new CameraComponent() { UseProjectionMatrix = true, ProjectionMatrix = SpriteBatch.CalculateDefaultProjection(VirtualResolution) }
            };

            // Create our player entitiy
            var playerEntity = new Entity() {
                new SpriteComponent() { SpriteGroup = Asset.Load<SpriteGroup>("Temp"), CurrentFrame = 0 },
                new TransformationComponent() { Translation = new Vector3(100, 100, 0) }
            };            
            // Make it so the engine knows about it
            Entities.Add(playerEntity);
            // A wrapper class for the entity that actual handles our stuff,
            // Yeah, components all the way might of been better, but for now this'll do, just
            // following JumpyJets example :P
            player = new Player(playerEntity, Input);

            // Create our level
            var platforms = Asset.Load<SpriteGroup>("Platforms");
            level = new Level();
            for (var i = 0; i < 10; i++)
            {
                level.Items.Add(new LevelItem(platforms.Images[0], new Vector2(80 + (i * 64), 500)));
            }

            // Set up the rendering pipeline
            CreatePipeline();

            gameHud = new Canvas();
            var healthBar = new UIGameBar(Services, gameHud);
            healthBar.LoadContent();
            UI.RootElement = gameHud;
            // Kick off our update loop
            Script.Add(UpdateLoop);
        }

        private void RenderLevel(RenderContext context)
        {
            var sb = new SpriteBatch(GraphicsDevice);
            sb.Begin(SpriteSortMode.Texture, null);
            level.Draw(sb);
            sb.End();
        }

        private void CreatePipeline()
        {
            var levelRenderer = new DelegateRenderer(Services) { Render = RenderLevel };

            
            // Setup the default rendering pipeline
            RenderSystem.Pipeline.Renderers.Add(new CameraSetter(Services) { Camera = cameraEntity.Get<CameraComponent>() });
            RenderSystem.Pipeline.Renderers.Add(new RenderTargetSetter(Services) {ClearColor = Color.CornflowerBlue});
            RenderSystem.Pipeline.Renderers.Add(levelRenderer);
            RenderSystem.Pipeline.Renderers.Add(new SpriteRenderer(Services));
            RenderSystem.Pipeline.Renderers.Add(new UIRenderer(Services));
            
        }

        private async Task UpdateLoop()
        {
            while (IsRunning)
            {
                // Wait next rendering frame
                await Script.NextFrame();
                player.Update((float)UpdateTime.Elapsed.TotalSeconds);
                 
                var result = level.CheckCollision(player.GetCollider());
                if (result.Collided)
                    player.OnCollision(result);
            }
        }
    }
}

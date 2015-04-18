using SiliconStudio.Core;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox;
using SiliconStudio.Paradox.UI.Panels;

namespace LudumDare32
{
    public class UiGameHud : ScriptContext
    {
        private Canvas hudCanvas;
        private UIGameBar healthBar;
        private UIGameBar staminaBar;

        public UiGameHud(IServiceRegistry registry)
            : base(registry)
        {
            hudCanvas = new Canvas();
        }

        public void LoadContent()
        {
            healthBar = new UIGameBar(Services, hudCanvas, "health");
            healthBar.LoadContent(new Vector3(0.05f, 0.9f, 1f));

            staminaBar = new UIGameBar(Services, hudCanvas, "stamina");
            staminaBar.LoadContent(new Vector3(0.8f, 0.9f, 1f));

            UI.RootElement = hudCanvas;
        }

        public void Update(float time)
        {
            staminaBar.SetPercentFill(1f - (time / 10));
        }
    }
}

namespace LudumDare32
{
    class LudumDare32App
    {
        static void Main(string[] args)
        {
            // Profiler.EnableAll();
            using (var game = new LudumDare32Game())
            {
                game.Run();
            }
        }
    }
}

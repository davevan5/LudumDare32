using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox.EntityModel;
using SiliconStudio.Paradox.Engine;
using System.Collections.Generic;
using System;
using SiliconStudio.Paradox.Input;

namespace LudumDare32
{
    class Player
    {
        private Entity entity;

        private readonly InputManager inputManager;

        public Vector2 Velocity;
        public Vector2 Position;


        private Vector2 gravity = new Vector2(0, 32);

        public Player(Entity entity, InputManager inputManager)
        {
            this.inputManager = inputManager;
            this.entity = entity;
            var translation = entity.Get<TransformationComponent>().Translation;
            this.Position = new Vector2(translation.X, translation.Y);
        }

        public void Update(float elapsedTime)
        {
            Vector2 movement = Vector2.Zero;
            if (inputManager.IsKeyDown(Keys.Right))
            {
                movement += Vector2.UnitX;
            }

            if (inputManager.IsKeyDown(Keys.Left))
            {
                movement += -Vector2.UnitX;
            }

            if (movement != Vector2.Zero)
            {
                movement.Normalize();
                if (Vector2.Dot(Velocity, movement) < 100)
                    Velocity += movement * 100;
            }

            if (inputManager.IsKeyDown(Keys.Space))
                Velocity += -Vector2.UnitY * 100;

            Velocity += gravity;
            Position += Velocity * elapsedTime;

            entity.Get<TransformationComponent>().Translation = new Vector3(Position, 0);
        }

        public void OnCollision(CollisionResult result)
        {
            Position += result.Resolution;

            Velocity.X += Math.Abs(Velocity.X) * Math.Sign(result.Resolution.X);
            Velocity.Y += Math.Abs(Velocity.Y) * Math.Sign(result.Resolution.Y);
            
            entity.Get<TransformationComponent>().Translation = new Vector3(Position, 0);
        }

        public RectangleF GetCollider()
        {
            return new RectangleF(Position.X - 16, Position.Y - 32, 32, 64);
        }
    }
}

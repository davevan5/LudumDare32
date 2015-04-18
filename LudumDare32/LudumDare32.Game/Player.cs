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

        private Vector2 velocity;
        private Vector2 position;
        private Vector2 gravity = new Vector2(0, 32);

        public Player(Entity entity, InputManager inputManager)
        {
            this.inputManager = inputManager;
            this.entity = entity;
            var translation = entity.Get<TransformationComponent>().Translation;
            this.position = new Vector2(translation.X, translation.Y);
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
                if (Vector2.Dot(velocity, movement) < 100)
                    velocity += movement * 100;
            }

            velocity += gravity;
            position += velocity * elapsedTime;

            entity.Get<TransformationComponent>().Translation = new Vector3(position, 0);
        }

        public void OnCollision(CollisionResult result)
        {
            position += result.Resolution;
            velocity += velocity * Vector2.Normalize(result.Resolution);
            entity.Get<TransformationComponent>().Translation = new Vector3(position, 0);
        }

        public RectangleF GetCollider()
        {
            return new RectangleF(position.X - 16, position.Y - 32, 32, 64);
        }
    }
}

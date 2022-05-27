using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample_Text;
using Sample_Text.EntityData;
using Sample_Text.ModelManagers;

namespace Sample_Text.ModelManagers
{
    public class MovementManager
    {
        private readonly CollisionManager CollisionManager;
        public MovementManager(CollisionManager collisionManager)
        {
            CollisionManager = collisionManager;
        }

        public void MoveEnemies(HashSet<Enemy> Enemies, PlayerEntity player)
        {
            foreach (var enemy in Enemies)
            {
                if (enemy.IsAlive)
                {
                    enemy.SetMoveDirectionToPlayerLocation(player);
                    CollisionManager.PlayerWithEnemyCollision(enemy, player);
                    enemy.Move(Enemies);
                }
            }
        }
        public void MoveBullets(HashSet<Bullet> Bullets)
        {
            foreach (var entity in Bullets)
            {
                if (entity.IsAlive)
                    entity.Move();
            }
        }
    }
}

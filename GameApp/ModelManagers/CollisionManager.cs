using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample_Text;
using Sample_Text.EntityData;

namespace Sample_Text.ModelManagers
{
    public class CollisionManager
    {
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            // Прямоугольники не пересекутся, если их противоположные вертикальные
            // и горизонтальные стороны не будут проходить друг через друга
            if ((r1.Top > r2.Bottom) || (r2.Top > r1.Bottom))
            {
                return false;
            }
            if ((r1.Right < r2.Left) || (r2.Right < r1.Left))
            {
                return false;
            }
            return true;
        }
        public void BulletWithEnemyCollisions(HashSet<Bullet> bullets, HashSet<Enemy> enemies)
        {
            foreach (var b in bullets)
            {
                if (b.IsAlive)
                {
                    var bulletRectangle = b.GetHitBoxRectangle();
                    foreach (var e in enemies)
                    {
                        if (e.IsAlive)
                        {
                            var runnerRectangle = e.GetHitBoxRectangle();
                            if (AreIntersected(bulletRectangle, runnerRectangle))
                            {
                                if (b.IsAlive)
                                {
                                    e.TakeDamage(b.Damage);
                                    b.IsAlive = false;
                                }
                                
                            }
                        }

                    }

                }

            }
        }
        public void PlayerWithEnemyCollisions(Enemy enemy, PlayerEntity player)
        {
            if (player.InvulnerabilityFrames > 0) return;
            var playerRectangle = player.GetHitBoxRectangle();
            var enemyRectangle = enemy.GetHitBoxRectangle();
            if (AreIntersected(playerRectangle, enemyRectangle))
            {
                player.TakeDamage(1);
            }
        }
    }
}

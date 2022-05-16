using Sample_Text.EntityData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample_Text
{
    public partial class Sample_Text : Form
    {
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
        }
        public PlayerEntity player;
        // Сущности хранятся в hashset
        // Есть ли целесообразность в реализации такой сборки мусора?
        // foreach (entity in entities) if (!entity.IsAlive) entities.Remove(entity)
        public HashSet<Bullet> Bullets;
        public HashSet<Runner> Enemies;
        public Random random;
        public Sample_Text()
        {
            //Action<HashSet<Entity>> action = x => x.Add(new Bullet(100,100,10));
            //action(entities);
            random = new Random();
            InitializeComponent();
            updateTimer.Interval = 10;
            updateTimer.Tick += new EventHandler(Update);
            KeyDown += new KeyEventHandler(OnPress);
            KeyUp += new KeyEventHandler(OnRelease);
            KeyDown += new KeyEventHandler(Shoot);
            KeyUp += new KeyEventHandler(ReleaseShooting);
            Enemies = new HashSet<Runner>();
            Bullets = new HashSet<Bullet>();
            
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UI_Paint);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Player_Paint);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Bullet_Paint);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Enemy_Paint);
            StartGame();
            
        }

        public void OnPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.movingUp = true;
                    break;
                case Keys.S:
                    player.movingDown = true;
                    break;
                case Keys.D:
                    player.movingRight = true;
                    break;
                case Keys.A:
                    player.movingLeft = true;
                    break;
            }
        }

        public void OnRelease(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.movingUp = false;
                    break;
                case Keys.S:
                    player.movingDown = false;
                    break;
                case Keys.D:
                    player.movingRight = false;
                    break;
                case Keys.A:
                    player.movingLeft = false;
                    break;
            }
        }

        public void Shoot(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    player.shootingUp = true;
                    break;
                case Keys.Down:
                    player.shootingDown = true;
                    break;
                case Keys.Left:
                    player.shootingLeft = true;
                    break;
                case Keys.Right:
                    player.shootingRight = true;
                    break;
            }
        }
        public void ReleaseShooting(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    player.shootingUp = false;
                    break;
                case Keys.Down:
                    player.shootingDown = false;
                    break;
                case Keys.Left:
                    player.shootingLeft = false;
                    break;
                case Keys.Right:
                    player.shootingRight = false;
                    break;
            }
        }

        public void StartGame()
        {
            player = new PlayerEntity(700, 520, 4);
            
            updateTimer.Start();
        }

        public void Update(object sender, EventArgs e)
        {
            player.Move();
            var rngX = random.Next(0, 1440);
            var rngY = random.Next(0, 1024);
            var rngTick = random.Next(0, 100);
            if (rngTick == 1) Enemies.Add(new Runner(rngX, rngY, 2));


            BulletWithEnemyCollisions(Bullets,Enemies);
            player.Shoot(Bullets);
            foreach (var enemy in Enemies)
            {
                if (enemy.IsAlive)
                {
                    enemy.FindPlayer(player);
                    CheckEnemyCollisions(enemy, player);
                    enemy.Move();
                }
                
            }
            foreach (var entity in Bullets)
            {
                if (entity.IsAlive)
                    entity.Move();
            }
            Enemies.RemoveWhere(x => !x.IsAlive);
            Bullets.RemoveWhere(x => !x.IsAlive);
            Invalidate();
        }

        private void Player_Paint(object sender, PaintEventArgs e)
        {
                var g = e.Graphics;
                var playerModel = player.HitBoxRectangle();
                g.DrawImage(Properties.Resources.mage, playerModel);
            
        }

        private void Bullet_Paint(object sender, PaintEventArgs e)
        {
            // Реализовать через PictureBox (спрайты)
            // Каждое entity должно иметь свойственный ему pbox
            var g = e.Graphics;
            foreach (var entity in Bullets)
            {
                if (entity.IsAlive)
                {
                    var entityModel = entity.HitBoxRectangle();
                    g.DrawRectangle
                        (new Pen(Color.Yellow, 1), entityModel);
                    g.FillRectangle(new SolidBrush(Color.Yellow), entityModel);
                }

            }
        }
        private void Enemy_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            foreach (var enemy in Enemies)
            {
                if (enemy.IsAlive)
                {
                    var enemyModel = enemy.HitBoxRectangle();
                    //g.DrawImage(Properties.Resources.sheben, enemyModel);
                    g.DrawRectangle(new Pen(Color.Red, 1), enemyModel);
                    g.FillRectangle(new SolidBrush(Color.Red), enemyModel);
                }
            }
        }
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
        private void CheckEnemyCollisions(Runner runner, PlayerEntity player)
        {
            var playerRectangle = player.HitBoxRectangle();
            var enemyRectangle = runner.HitBoxRectangle();
            if (AreIntersected(playerRectangle, enemyRectangle))
            {
                player.TakeDamage(1);
            }
        }

        private void BulletWithEnemyCollisions(HashSet<Bullet> bullets, HashSet<Runner> runners)
        {
            foreach (var b in bullets)
            {
                if (b.IsAlive)
                {
                    var bulletRectangle = b.HitBoxRectangle();
                    foreach (var r in runners)
                    {
                        if (r.IsAlive)
                        {
                            var runnerRectangle = r.HitBoxRectangle();
                            if (AreIntersected(bulletRectangle, runnerRectangle))
                            {
                                r.TakeDamage(b.Damage);
                                b.IsAlive = false;
                            }
                        }
                        
                    }

                }
                
            }
        }
        private void UI_Paint(object sender, PaintEventArgs e)
        {
            // Todo
            // можно добавить unit тесты (no player entity) (game over) (draws correctly)
            var g = e.Graphics;
            var healthBar = new Rectangle(0, 20, player.Health * 3, 10);
            g.DrawRectangle(new Pen(Color.Red, 10), healthBar);
        }
    }
}
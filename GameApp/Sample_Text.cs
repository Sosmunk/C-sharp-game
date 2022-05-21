using Sample_Text.EntityData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Sample_Text.ModelManagers;
using Sample_Text.View;
using Sample_Text.Controller;

namespace Sample_Text
{
    public partial class Sample_Text : Form
    {
        public PlayerEntity Player;
        public HashSet<Bullet> Bullets;
        public HashSet<Enemy> Enemies;
        public Random random;
        public CollisionManager CollisionManager;
        public MovementManager MovementManager;
        public PaintManager PaintManager;
        public InputManager InputManager;
        public SpriteManager SpriteManager;
        public Sample_Text()
        {
            random = new Random();
            InitializeComponent();
            updateTimer.Interval = 10;
            updateTimer.Tick += new EventHandler(Update);
            
            Enemies = new HashSet<Enemy>();
            Bullets = new HashSet<Bullet>();
            CollisionManager = new CollisionManager();
            MovementManager = new MovementManager(CollisionManager);
            SpriteManager = new SpriteManager();
            Paint += new PaintEventHandler(UI_Paint);
            Paint += new PaintEventHandler(Player_Paint);
            Paint += new PaintEventHandler(Bullet_Paint);
            Paint += new PaintEventHandler(Enemy_Paint);
            StartGame();
        }

        private void StartGame()
        {
            Player = new PlayerEntity(700, 520, 4);
            InputManager = new InputManager(Player);
            KeyDown += new KeyEventHandler(InputManager.MoveOnPress);
            KeyUp += new KeyEventHandler(InputManager.StopMovingOnRelease);
            KeyDown += new KeyEventHandler(InputManager.Shoot);
            KeyUp += new KeyEventHandler(InputManager.ReleaseShooting);
            updateTimer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            Player.Move();
            Player.UpdateInvulnerabilityFrames();
            var rngX = random.Next(0, 1440);
            var rngY = random.Next(0, 1024);
            var rngTick = random.Next(0, 100);
            if (rngTick == 1) Enemies.Add(new Ghoul(rngX, rngY, 2));

            Player.Shoot(Bullets);
            CollisionManager.BulletWithEnemyCollisions(Bullets,Enemies);

            MovementManager.MoveEnemies(Enemies, Player);
            MovementManager.MoveBullets(Bullets);

            Enemies.RemoveWhere(x => !x.IsAlive);
            Bullets.RemoveWhere(x => !x.IsAlive);
            Invalidate();
        }

        private void Player_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var playerModel = Player.GetHitBoxRectangle();
            var spriteSize = new Rectangle(0, 0, 104, 100);
            if (Player.lookDirection == Entity.LookDirection.Right)
            {
                g.DrawImage(SpriteManager.PlayerImage, playerModel, spriteSize, GraphicsUnit.Pixel);
            }
            if (Player.lookDirection == Entity.LookDirection.Left)
            {
                g.DrawImage(SpriteManager.PlayerLeftImage, playerModel, spriteSize, GraphicsUnit.Pixel);
            }
            
        }

        private void Bullet_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            foreach (var bullet in Bullets)
            {
                if (bullet.IsAlive)
                {
                    var entityModel = bullet.GetHitBoxRectangle();
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
                    var enemyModel = enemy.GetHitBoxRectangle();
                    var spriteSize = new Rectangle(0, 0, 300, 300); 
                    if (enemy.lookDirection == Entity.LookDirection.Right)
                    {
                        g.DrawImage(SpriteManager.GhoulImage, enemyModel, spriteSize, GraphicsUnit.Pixel);
                    }
                    if (enemy.lookDirection == Entity.LookDirection.Left)
                    {
                        g.DrawImage(SpriteManager.GhoulLeftImage, enemyModel, spriteSize, GraphicsUnit.Pixel);
                    }
                }
            }
        }

        private void UI_Paint(object sender, PaintEventArgs e)
        {
            // Todo
            // можно добавить unit тесты (no player entity) (game over) (draws correctly)
            var g = e.Graphics;
            var healthBar = new Rectangle(0, 20, Player.Health * 3, 10);
            g.DrawRectangle(new Pen(Color.Red, 10), healthBar);
        }
    }
}
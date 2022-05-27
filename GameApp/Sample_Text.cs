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
        bool GameOver;
        public PlayerEntity Player;
        public HashSet<Bullet> Bullets;
        public HashSet<Enemy> Enemies;
        public Random Random;
        public CollisionManager CollisionManager;
        public MovementManager MovementManager;
        public PaintManager PaintManager;
        public GameplayInputManager InputManager;
        public SpriteManager SpriteManager;
        public MenuInputManager MenuInputManager;
        public GameStatistics GameStatistics;
        public LabelManager LabelManager;
        public Sample_Text()
        {
            Random = new Random();
            InitializeComponent();
            updateTimer.Interval = 10;
            updateTimer.Tick += new EventHandler(Update);
            PaintManager = new PaintManager();
            Paint += new PaintEventHandler(UI_Paint);
            Paint += new PaintEventHandler(Player_Paint);
            Paint += new PaintEventHandler(Bullet_Paint);
            Paint += new PaintEventHandler(Enemy_Paint);
            MenuInputManager = new MenuInputManager(this);
            StartGame();
        }

        public void StartGame()
        {
            
            Controls.Clear();
            KeyUp -= new KeyEventHandler(MenuInputManager.PressEnterToRestart);

            Player = new PlayerEntity(700, 520, 4);
            InputManager = new GameplayInputManager(Player);
            KeyDown += new KeyEventHandler(InputManager.MoveOnPress);
            KeyUp += new KeyEventHandler(InputManager.StopMovingOnRelease);
            KeyDown += new KeyEventHandler(InputManager.Shoot);
            KeyUp += new KeyEventHandler(InputManager.ReleaseShooting);
            Enemies = new HashSet<Enemy>();
            Bullets = new HashSet<Bullet>();
            CollisionManager = new CollisionManager();
            MovementManager = new MovementManager(CollisionManager);
            SpriteManager = new SpriteManager();
            GameStatistics = new GameStatistics();
            LabelManager = new LabelManager(Controls,this);
            Controls.Add(LabelManager.KillCount);
            GameOver = false;
            updateTimer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            if (Player.IsAlive)
            {
                Player.Move();
                Player.UpdateInvulnerabilityFrames();
                var rngX = Random.Next(0, 1440);
                var rngY = Random.Next(0, 1024);
                var rngTick = Random.Next(0, 100);
                if (rngTick == 1) Enemies.Add(new Ghoul(rngX, rngY, 2));

                Player.Shoot(Bullets);
                CollisionManager.BulletWithEnemyCollisions(Bullets, Enemies);
                

                MovementManager.MoveEnemies(Enemies, Player);
                MovementManager.MoveBullets(Bullets);

                foreach (var enemy in Enemies)
                {
                    if (!enemy.IsAlive)
                        GameStatistics.AddKill();
                }
                LabelManager.ChangeKillCount(GameStatistics.KillCount);
                Enemies.RemoveWhere(x => !x.IsAlive);
                Bullets.RemoveWhere(x => !x.IsAlive);
            }
            else
            {
                if (!GameOver)
                {
                    GameOver = true;
                    LabelManager.CreateGameOverLabels();
                    KeyUp += new KeyEventHandler(MenuInputManager.PressEnterToRestart);
                }
            }
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
            var g = e.Graphics;
            var healthBar = new Rectangle(0, 20, Player.Health * 3, 10);
            g.DrawRectangle(new Pen(Color.Red, 10), healthBar);
        }
    }
}
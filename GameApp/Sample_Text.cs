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
        public Entity player;
        // Сущности хранятся в hashset
        // Есть ли целесообразность в реализации такой сборки мусора?
        // foreach (entity in entities) if (!entity.IsAlive) entities.Remove(entity)
        public HashSet<Entity> entities = new HashSet<Entity>();
        
        public Sample_Text()
        {
            //Action<HashSet<Entity>> action = x => x.Add(new Bullet(100,100,10));
            //action(entities);
            InitializeComponent();
            updateTimer.Interval = 10;
            updateTimer.Tick += new EventHandler(Update);
            KeyDown += new KeyEventHandler(OnPress);
            KeyUp += new KeyEventHandler(OnRelease);
            KeyDown += new KeyEventHandler(Shoot);
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

        // Очень много копипасты, нужно отрефакторить
        // Пока что костыль
        // Не придумал, как реализовать механику изменения вида стрельбы (например triple-shot при подбирании power-up)
        // Может быть через делегаты?
        // Перенести в логику PlayerEntity
        // shootingleft,shootingRight; player.shoot(Hashset<Entity> entities)
        
        public void Shoot(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    var u = new Bullet((int)player.Position.X, (int)player.Position.Y, 10);
                    u.movingUp = true;
                    entities.Add(u);
                    break;
                case Keys.Down:
                    var d = new Bullet((int)player.Position.X, (int)player.Position.Y, 10);
                    d.movingDown = true;
                    entities.Add(d);
                    break;
                case Keys.Left:
                    var l = new Bullet((int)player.Position.X, (int)player.Position.Y, 10);
                    l.movingLeft = true;
                    entities.Add(l);
                    break;
                case Keys.Right:
                    var r = new Bullet((int)player.Position.X, (int)player.Position.Y, 10);
                    r.movingRight = true;
                    entities.Add(r);
                    break;

            }
        }

        public void StartGame()
        {
            player = new PlayerEntity(700,520,4);
            updateTimer.Start();
        }

        public void Update(object sender, EventArgs e)
        {
            player.Move();
            foreach (var entity in entities)
            {
                if (entity.IsAlive)
                entity.Move();
            }
            Invalidate();
        }

        private void Player_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var playerModel = new Rectangle
                ((int)player.Position.X,
                (int)player.Position.Y,
                player.Hitbox,
                player.Hitbox);
            
            g.DrawRectangle(new Pen(Color.Turquoise, 1), playerModel);
            g.FillRectangle(new SolidBrush(Color.Turquoise), playerModel);
        }

        private void Entity_Paint(object sender, PaintEventArgs e)
        {
            // Реализовать через PictureBox (спрайты)
            // Каждое entity должно иметь свойственный ему pbox
            var g = e.Graphics;
            foreach (var entity in entities)
            {
                if (entity.IsAlive)
                {
                    
                    var entityModel = new Rectangle
                        ((int)entity.Position.X - player.Hitbox / 2,
                        (int)entity.Position.Y - player.Hitbox / 2,
                        entity.Hitbox,
                        entity.Hitbox);
                    g.DrawImage(, entityModel);
                    g.DrawRectangle
                        (new Pen(Color.Yellow, 1), entityModel);
                    g.FillRectangle(new SolidBrush(Color.Yellow), entityModel);
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

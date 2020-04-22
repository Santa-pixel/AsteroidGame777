using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidGame
{
    internal static class Game
    {

        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;

        private static VisualObject[] __GameObjects;

        public static int Width { get; set; }

        public static int Height { get; set; }
        
        public static void Initialize(Form form)
        {
            Width = form.Width;
            Height = form.Height;

            __Context = BufferedGraphicsManager.Current;
            Graphics g = form.CreateGraphics();
            __Buffer = __Context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += OnVimerTick;
        }

        private static void OnVimerTick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            Graphics g = __Buffer.Graphics;

            g.Clear(Color.Black);
            g.DrawRectangle(Pens.White, new Rectangle(400, 300, 100, 100));
            g.FillEllipse(Brushes.Red, new Rectangle(400, 300, 100, 100));

            foreach (var game_object in __GameObjects)
                game_object.Draw(g);

            __Buffer.Render();
        }

        public static void Update()
        {
            foreach (var game_object in __GameObjects)
                game_object.Update();
        }

        public static void Load()
        {
            __GameObjects = new VisualObject[30]; //Игровой обьект

            for(var i =0; i < __GameObjects.Length; i++)
            {
                __GameObjects[i] = new VisualObject(
                    new Point(600, i * 20),  //Место оложения
                    new Point(15 - i, 20 - i),  //Скорость еремещения
                    new Size(20, 20));  //Размер
            }
        }
    }
}

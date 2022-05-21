using System;
using System.Collections.Generic;
using System.Drawing;
using Sample_Text.Properties;

namespace Sample_Text.View
{
    public class SpriteManager
    {
        public Bitmap PlayerImage;
        public Bitmap PlayerLeftImage;
        public Bitmap GhoulImage;
        public Bitmap GhoulLeftImage;
        public Bitmap BulletImage;
        

        public SpriteManager()
        {
            PlayerImage = Resources._2;
            PlayerLeftImage = Resources._2;
            PlayerLeftImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            GhoulImage = Resources.g1;
            GhoulLeftImage = Resources.g1;
            GhoulLeftImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            BulletImage = null;
        }

    }
}

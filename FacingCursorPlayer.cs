using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GazeAtCursor
{
    public class FacingCursorPlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            if (Main.myPlayer != Player.whoAmI || Main.netMode == NetmodeID.Server || Main.gamePaused)
                return;

            Vector2 delta = Main.MouseWorld - Player.Center;
            int desiredDir = delta.X > 0 ? 1 : -1;
            Player.direction = desiredDir;

            if (Player.itemAnimation > 0 && Player.HeldItem.DamageType == DamageClass.Melee)
                return;

            float aim = delta.ToRotation() + (desiredDir == -1 ? MathHelper.Pi : 0f);
            Player.itemRotation = aim;
        }
    }
}

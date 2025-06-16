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
            // only run for the local, non-paused client
            if (Main.myPlayer != Player.whoAmI || Main.netMode == NetmodeID.Server || Main.gamePaused)
                return;

            // figure out which way to face based purely on cursor X
            Vector2 delta = Main.MouseWorld - Player.Center;
            int desiredDir = delta.X > 0 ? 1 : -1;
            Player.direction = desiredDir;

            // if we’re in a melee swing, bail out here and let vanilla handle the rotation
            if (Player.itemAnimation > 0 && Player.HeldItem.DamageType == DamageClass.Melee)
                return;

            // otherwise (ranged/magic/idle) aim item at cursor
            float aim = delta.ToRotation() + (desiredDir == -1 ? MathHelper.Pi : 0f);
            Player.itemRotation = aim;
        }
    }
}

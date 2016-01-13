/**
 * Boyan Bonev @2011/2012
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game.Classes
{
    internal static class DrawModel
    {
        internal static void DrawRoad(Model model, Matrix viewMatrix, Matrix projectionMatrix)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect currentEffect in mesh.Effects)
                {
                    Matrix[] transforms = new Matrix[model.Bones.Count];
                    model.CopyAbsoluteBoneTransformsTo(transforms);
                    currentEffect.World = transforms[mesh.ParentBone.Index];
                    currentEffect.View = viewMatrix;
                    currentEffect.Projection = projectionMatrix;
                    currentEffect.EnableDefaultLighting();
                }
                mesh.Draw();
            }
        }

        internal static void DrawBicycle(Model model, Matrix viewMatrix, Matrix projectionMatrix, float createRotation, Quaternion bicycleRotation, Vector3 bicyclePosition, ref Matrix worldMatrix)
        {
             worldMatrix = Matrix.CreateScale(0.7f) * Matrix.CreateRotationY(createRotation) * Matrix.CreateFromQuaternion(bicycleRotation) * Matrix.CreateTranslation(bicyclePosition);

                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect currentEffect in mesh.Effects)
                    {
                        Matrix[] transforms = new Matrix[model.Bones.Count];
                        model.CopyAbsoluteBoneTransformsTo(transforms);
                        currentEffect.World = transforms[mesh.ParentBone.Index] * worldMatrix;
                        currentEffect.View = viewMatrix;
                        currentEffect.Projection = projectionMatrix;
                        currentEffect.EnableDefaultLighting();
                    }
                    mesh.Draw();
                }
        }
    }
}

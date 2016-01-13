/**
 * Boyan Bonev @2011/2012
 **/

using system;
using system.collections.generic;
using system.linq;
using system.text;
using microsoft.xna.framework.graphics;
using microsoft.xna.framework;

namespace game.classes
{
    public static class boundingboxrenderer
    {
        #region fields

        boundingbox myboundingbox = new boundingbox(new vector3(0, 0, 0), new vector3(4, 4, 4));
        static vertexpositioncolor[] verts = new vertexpositioncolor[8];
        static int[] indices = new int[]
        {
            0, 1,
            1, 2,
            2, 3,
            3, 0,
            0, 4,
            1, 5,
            2, 6,
            3, 7,
            4, 5,
            5, 6,
            6, 7,
            7, 4,
        };

        static basiceffect effect;
        static vertexdeclaration vertdecl;

        #endregion

         <summary>
         renders the bounding box for debugging purposes.
         </summary>
         <param name="box">the box to render.</param>
         <param name="graphicsdevice">the graphics device to use when rendering.</param>
         <param name="view">the current view matrix.</param>
         <param name="projection">the current projection matrix.</param>
         <param name="color">the color to use drawing the lines of the box.</param>
        public static void render(
            boundingbox box,
            graphicsdevice graphicsdevice,
            matrix view,
            matrix projection,
            color color)
        {
            if (effect == null)
            {
                effect = new basiceffect(graphicsdevice, null);
                effect.vertexcolorenabled = true;
                effect.lightingenabled = false;
                vertdecl = new vertexdeclaration(graphicsdevice, vertexpositioncolor.vertexelements);
            }

            vector3[] corners = box.getcorners();
            for (int i = 0; i < 8; i++)
            {
                verts[i].position = corners[i];
                verts[i].color = color;
            }

            graphicsdevice.vertexdeclaration = vertdecl;

            effect.view = view;
            effect.projection = projection;

            effect.begin();
            foreach (effectpass pass in effect.currenttechnique.passes)
            {
                pass.begin();

                graphicsdevice.drawuserindexedprimitives(
                    primitivetype.linelist,
                    verts,
                    0,
                    8,
                    indices,
                    0,
                    indices.length / 2);

                pass.end();
            }
            effect.end();
        }
    }
}

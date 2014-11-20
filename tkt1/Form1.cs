using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
namespace tkt1
{
    public partial class Form1 : Form
    {
        bool loaded = false;
        Point mouse;
        Point ptmp;
        float xx = 0;
        float yy = 0;
        int vw = 300;
        int vh = 300;
        public Form1()
        {
            InitializeComponent();
            glControl1.MouseWheel += glControl1_MouseWheel;
        }

        void glControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            vw += e.Delta;
            vh += e.Delta;
            glControl1.Invalidate();
        }

        private void SetupViewport()
        {
            GL.MatrixMode(MatrixMode.Projection); 
            GL.LoadIdentity();

            GL.Ortho(0, 400, 0, 400, -200, 200);
            GL.Viewport(0, 0,vw, vh);
        }
        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;
            GL.ClearColor(Color.White);
            SetupViewport();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!loaded)
            {
                return;
            }
            SetupViewport();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.Translate(200, 200, 100);
            //draw the Coordinate system 
            GL.Rotate(xx, OpenTK.Vector3d.UnitX);
            GL.Rotate(yy, OpenTK.Vector3d.UnitY);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex2(0, 0);
            GL.Vertex2(400, 0);
            GL.Color3(Color.Blue);
            GL.Vertex2(0, 0);
            GL.Vertex3(0, 400,0);
            GL.Color3(Color.Green);
           GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 800);
            GL.End();

            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(10, 20, 0);
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(100, 20, 50);
            GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(100, 50, 50);
            GL.End();
            //GL.Rotate(rtti, OpenTK.Vector3d.UnitY);
            //GL.Begin(PrimitiveType.Triangles);
            //GL.Color3(1.0f, 0.0f, 0.0f);
            //GL.Vertex3(0.0f, 20.0f, 0.0f);
            //GL.Color3(0.0f, 1.0f, 0.0f);
            //GL.Vertex3(-20.0f, -20.0f, 20.0f);
            //GL.Color3(0.0f, 0.0f, 1.0f);
            //GL.Vertex3(20.0f, -20.0f, 20.0f);
            //GL.End();
            GL.LoadIdentity();
            glControl1.SwapBuffers();
            label1.Text = xx.ToString();
            label2.Text = yy.ToString();
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            if (!loaded)
            {
                return;
            }
            glControl1.Invalidate();
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                xx += 1f;
            }
            if (e.KeyCode == Keys.W)
            { 
                xx -= 1f;
            }

            if (e.KeyCode == Keys.D)
            {
                yy += 1f;
            }
            if ( e.KeyCode == Keys.A)
            {
                yy -= 1f;
            }
            glControl1.Invalidate();
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse = e.Location;
            label3.Text = mouse.ToString();
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mnow = e.Location;
               

                label4.Text = mnow.ToString();
                if (ptmp.Y < mnow.Y)
                {
                    xx += 1f;
                }
                else if (ptmp.Y > mnow.Y)
                {
                    xx -= 1f;
                }
                if (ptmp.X < mnow.X)
                {
                    yy += 1f;
                }
                else if (ptmp.X > mnow.X)
                {
                    yy -= 1f;
                }
                yy %= 360;
                xx %= 360;
                glControl1.Invalidate();
                ptmp = e.Location;
            }
            label5.Text = e.Location.ToString();
        }
    }
}

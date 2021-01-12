using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace _3D_graphics
{
    public partial class Form1 : Form
    {
        readonly List<Matrix3D> Cube;
        //Graphics gr;
        public int x_center;
        public int y_center;
        public int centerX = 0, centerY = 0;
        Nullable<int> clicklPos = null;
        public int scale = 20;

        public bool proectXY, proectXZ, proectYZ, proectO;

        public Form1()
        {
            InitializeComponent();
            Cube = new List<Matrix3D>
            {
                new Matrix3D(0, 0, 0), //0 НИЗ
                new Matrix3D(1, 0, 0), //1
                new Matrix3D(1, 0, 1), //2
                new Matrix3D(0, 0, 1), //3

                new Matrix3D(0, 1, 0), //4 ВВЕРХ
                new Matrix3D(1, 1, 0), //5
                new Matrix3D(1, 1, 1), //6
                new Matrix3D(0, 1, 1)  //7
            };

            pictureBox1.BorderStyle = BorderStyle.FixedSingle;

            x_center = pictureBox1.Width / 2;
            y_center = pictureBox1.Height / 2;
        }
        private void checkBoxRotateLine_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRotateLine.Checked)
            {
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                timer1.Stop();
                timer1.Enabled = false;
            }
        }
        private void trackBarRotateLine_Scroll(object sender, EventArgs e)
        {
            textBoxRotateLine.Text = trackBarRotateLine.Value.ToString();
            pictureBox1.Invalidate();
        }
        private void НазадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            Close();
        }
        private void ОчиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = null;
            x_center = pictureBox1.Width / 2;
            y_center = pictureBox1.Height / 2;
            scale = 20;
            ButtonStop_Click(sender, e);
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            trackBarSpeed.Value = 10000;

            textBox1.Text = scale.ToString();
            trackBar1.Value = scale;

            trackBarX.Value = 0;
            textBoxX.Text = trackBarX.Value.ToString();
            trackBarZ.Value = 0;
            textBoxZ.Text = trackBarZ.Value.ToString();
            trackBarY.Value = 0;
            textBoxY.Text = trackBarY.Value.ToString();

            trackBarRotateX.Value = 0;
            textBoxRotateX.Text = trackBarRotateX.Value.ToString();
            trackBarRotateZ.Value = 0;
            textBoxRotateZ.Text = trackBarRotateZ.Value.ToString();
            trackBarRotateY.Value = 0;
            textBoxRotateY.Text = trackBarRotateY.Value.ToString();

            radioButton1.Checked = false;
            proectXY = false; checkBox1.Checked = false;
            proectXZ = false; checkBox2.Checked = false;
            proectYZ = false; checkBox3.Checked = false;

            comboColor.SelectedItem = null;
            checkFilling.Checked = false;

            textBoxEndX.Text = "0"; textBoxEndY.Text = "1"; textBoxEndZ.Text = "0";
            textBoxStartX.Text = "0"; textBoxStartY.Text = "0"; textBoxStartZ.Text = "0";

            pictureBox1.Invalidate();
        }
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            centerX = e.X;
            centerY = e.Y;
            clicklPos = 0;
            pictureBox1.Invalidate();
        }
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicklPos != null)
            {
                x_center += e.X - centerX;
                y_center += e.Y - centerY;
                centerX = e.X;
                centerY = e.Y;
                pictureBox1.Invalidate();
            }
        }
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            clicklPos = null;
        }
        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            scale = trackBar1.Value;
            textBox1.Text = scale.ToString();
            pictureBox1.Invalidate();
        }
        private void TrackBarXYZ_Scroll(object sender, EventArgs e)
        {
            textBoxX.Text = trackBarX.Value.ToString();
            textBoxY.Text = trackBarY.Value.ToString();
            textBoxZ.Text = trackBarZ.Value.ToString();

            pictureBox1.Invalidate();
        }
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Invalidate();
            if (textBox1.Text == "") trackBar1.Value = 0;
            else trackBar1.Value = Convert.ToInt32(textBox1.Text);
            scale = trackBar1.Value;
            pictureBox1.Invalidate();
        }
        private void TextBoxXYZ_TextChanged(object sender, EventArgs e)
        {
            textBoxX.Invalidate();
            if (textBoxX.Text == "-" || textBoxX.Text == "--") trackBarX.Value = 0;
            else if (textBoxX.Text == "") trackBarX.Value = 0;
            else trackBarX.Value = Convert.ToInt32(textBoxX.Text);

            textBoxY.Invalidate();
            if (textBoxY.Text == "-" || textBoxY.Text == "--") trackBarY.Value = 0;
            else if (textBoxY.Text == "") trackBarY.Value = 0;
            else trackBarY.Value = Convert.ToInt32(textBoxY.Text);

            textBoxZ.Invalidate();
            if (textBoxZ.Text == "-" || textBoxZ.Text == "--") trackBarZ.Value = 0;
            else if (textBoxZ.Text == "") trackBarZ.Value = 0;
            else trackBarZ.Value = Convert.ToInt32(textBoxZ.Text);

            pictureBox1.Invalidate();
        }
        private void TextBoxRotateXYZ(object sender, EventArgs e)
        {
            textBoxRotateX.Invalidate();
            if (textBoxRotateX.Text == "-" || textBoxRotateX.Text == "--") trackBarRotateX.Value = 0;
            else if (textBoxRotateX.Text == "") trackBarRotateX.Value = 0;
            else trackBarRotateX.Value = Convert.ToInt32(textBoxRotateX.Text);

            textBoxRotateY.Invalidate();
            if (textBoxRotateY.Text == "-" || textBoxRotateY.Text == "--") trackBarRotateY.Value = 0;
            else if (textBoxRotateY.Text == "") trackBarRotateY.Value = 0;
            else trackBarRotateY.Value = Convert.ToInt32(textBoxRotateY.Text);

            textBoxRotateZ.Invalidate();
            if (textBoxRotateZ.Text == "-" || textBoxRotateZ.Text == "--") trackBarRotateZ.Value = 0;
            else if (textBoxRotateZ.Text == "") trackBarRotateZ.Value = 0;
            else trackBarRotateZ.Value = Convert.ToInt32(textBoxRotateZ.Text);

            pictureBox1.Invalidate();
        }
        private void TrackBarRotationX(object sender, EventArgs e)
        {
            textBoxRotateX.Text = trackBarRotateX.Value.ToString();

            pictureBox1.Invalidate();
        }
        private void TrackBarRotationY(object sender, EventArgs e)
        {
            textBoxRotateY.Text = trackBarRotateY.Value.ToString();

            pictureBox1.Invalidate();
        }
        private void TrackBarRotationZ(object sender, EventArgs e)
        {
            textBoxRotateZ.Text = trackBarRotateZ.Value.ToString();

            pictureBox1.Invalidate();
        }
        private void ButtonAnimation_Click(object sender, EventArgs e)
        {
            buttonAnimation.Visible = false;
            buttonStop.Visible = true;
            timer1.Enabled = true;
            timer1.Start();
        }
        private void ButtonStop_Click(object sender, EventArgs e)
        {
            buttonStop.Visible = false;
            buttonAnimation.Visible = true;
            timer1.Stop();
            timer1.Enabled = false;
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            int XX = trackBarRotateX.Value, YY = trackBarRotateY.Value, ZZ = trackBarRotateZ.Value;
            if (checkBox5.Checked)
            {
                if (XX != 360)
                {
                    XX += 1;
                    textBoxRotateX.Text = XX.ToString();
                }
                else trackBarRotateX.Value = -360;
            }
            if (checkBox6.Checked)
            {
                if (YY != 360)
                {
                    YY += 1;
                    textBoxRotateY.Text = YY.ToString();
                }
                else trackBarRotateY.Value = -360;
            }
            if (checkBox7.Checked)
            {
                if (ZZ != 360)
                {
                    ZZ += 1;
                    textBoxRotateZ.Text = ZZ.ToString();
                }
                else trackBarRotateZ.Value = -360;
            }
            if (checkBoxRotateLine.Checked)
            {
                if (trackBarRotateLine.Value != 360)
                    trackBarRotateLine.Value += 1;
                else
                    trackBarRotateLine.Value = -360;
                textBoxRotateLine.Text = trackBarRotateLine.Value.ToString();
            }
            pictureBox1.Invalidate();
        }
        private void CheckBoxYZ(object sender, EventArgs e)
        {
            CheckBox yz = (CheckBox)sender;
            if (yz.Checked == true)
            {
                proectYZ = true;
                pictureBox1.Invalidate();
            }
            else
            {
                proectYZ = false;
                pictureBox1.Invalidate();
            }
            pictureBox1.Invalidate();
        }
        private void CheckBoxXZ(object sender, EventArgs e)
        {
            CheckBox xz = (CheckBox)sender;
            if (xz.Checked == true)
            {
                proectXZ = true;
                pictureBox1.Invalidate();
            }
            else
            {
                proectXZ = false;
                pictureBox1.Invalidate();
            }
            pictureBox1.Invalidate();
        }
        private void CheckBoxXY(object sender, EventArgs e)
        {
            CheckBox xy = (CheckBox)sender;
            if (xy.Checked == true)
            {
                proectXY = true;
                pictureBox1.Invalidate();
            }
            else
            {
                proectXY = false;
                pictureBox1.Invalidate();
            }
            pictureBox1.Invalidate();
        }
        private void TrackBarSpeed_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = 100000000 / trackBarSpeed.Value;
        }
        public float A_to_r(float angle)
        {
            return (float)(angle * Math.PI / 180); // перевод в радианы
        }
        public static float[,] Mul(float[,] matrix1, float[,] matrix2)
        {
            float sum;
            float[,] result = new float[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    sum = 0;
                    for (int k = 0; k < 4; k++)
                        sum += matrix1[i, k] * matrix2[k, j];
                    result[i, j] = sum;
                }
            return result;
        }
        private List<Matrix3D> RotationVector(Graphics gr, List<Matrix3D> cubePoints, float angle = 0)
        {
            textBoxStartX.Invalidate();
            if (textBoxStartX.Text == "-" || textBoxStartX.Text == "--" || textBoxStartX.Text == "") textBoxStartX.Text = "0";

            textBoxStartY.Invalidate();
            if (textBoxStartY.Text == "-" || textBoxStartY.Text == "--" || textBoxStartY.Text == "") textBoxStartY.Text = "0";

            textBoxStartZ.Invalidate();
            if (textBoxStartZ.Text == "-" || textBoxStartZ.Text == "--" || textBoxStartZ.Text == "") textBoxStartZ.Text = "0";

            textBoxEndX.Invalidate();
            if (textBoxEndX.Text == "-" || textBoxEndX.Text == "--" || textBoxEndX.Text == "") textBoxEndX.Text = "0";

            textBoxEndY.Invalidate();
            if (textBoxEndY.Text == "-" || textBoxEndY.Text == "--" || textBoxEndY.Text == "") textBoxEndY.Text = "0";

            textBoxEndZ.Invalidate();
            if (textBoxEndZ.Text == "-" || textBoxEndZ.Text == "--" || textBoxEndZ.Text == "") textBoxEndZ.Text = "0";

            int endX = Convert.ToInt32(textBoxEndX.Text), endY = Convert.ToInt32(textBoxEndY.Text), endZ = Convert.ToInt32(textBoxEndZ.Text);
            int startX = Convert.ToInt32(textBoxStartX.Text), startY = Convert.ToInt32(textBoxStartY.Text), startZ = Convert.ToInt32(textBoxStartZ.Text);

            if (startX == endX && startY == endY && startZ == endZ)
                MessageBox.Show("Заданная прямая не может быть точкой");

            // координаты вектора
            float vectorX = endX - startX;
            float vectorY = endY - startY;
            float vectorZ = endZ - startZ;
            float length = (float)Math.Pow(Math.Sqrt(Math.Pow(vectorX, 2) + Math.Pow(vectorY, 2) + Math.Pow(vectorZ, 2)), scale / 15);

            Pen greenPen_dashed = new Pen(Color.Red, 2);
            greenPen_dashed.DashStyle = DashStyle.Dash;

            Matrix3D mStart = new Matrix3D(startX, startY, startZ);
            Matrix3D mEnd = new Matrix3D(endX, endY, endZ);
            Matrix3D mStartL = new Matrix3D(startX - vectorX * length, startY - vectorY * length, startZ - vectorZ * length);
            Matrix3D mEndL = new Matrix3D(endX + vectorX * length, endY + vectorY * length, endZ + vectorZ * length);
            if (checkBoxLine.Checked) //нарисовать вектор
            {
                gr.DrawLine(greenPen_dashed, mStartL.ScreenPoint(scale), mEndL.ScreenPoint(scale));
            }
            float sum = (float)Math.Sqrt(Math.Pow(vectorX, 2) + Math.Pow(vectorY, 2) + Math.Pow(vectorZ, 2));
            float dX = vectorX / sum, dY = vectorY / sum, dZ = vectorZ / sum;
            float d = (float)Math.Sqrt(dY * dY + dZ * dZ);

            float[,] Translation_Inverse = new float[4, 4]; // обратная матрица переноса(перенос вектора в начало координат)
            Array.Clear(Translation_Inverse, 0, 4);
            Translation_Inverse[0, 0] = 1; Translation_Inverse[1, 1] = 1;
            Translation_Inverse[2, 2] = 1; Translation_Inverse[3, 3] = 1;
            Translation_Inverse[3, 0] = -endX; Translation_Inverse[3, 1] = -endY;
            Translation_Inverse[3, 2] = -endZ;

            float[,] Translation = new float[4, 4]; // матрица переноса(перенос обратно)
            Array.Clear(Translation, 0, 4);
            Translation[0, 0] = 1; Translation[1, 1] = 1;
            Translation[2, 2] = 1; Translation[3, 3] = 1;
            Translation[3, 0] = endX; Translation[3, 1] = endY;
            Translation[3, 2] = endZ;

            float[,] RotationVec;// матрица результата 

            if (dY != 0 || dZ != 0)
            {
                float[,] RotationX_Inverse = new float[4, 4]; // обратная матрица поворота по Х(поворот системы координат вокруг оси X)
                Array.Clear(RotationX_Inverse, 0, 4);
                RotationX_Inverse[0, 0] = 1; RotationX_Inverse[1, 1] = dZ / d;
                RotationX_Inverse[2, 2] = dZ / d; RotationX_Inverse[3, 3] = 1;
                RotationX_Inverse[2, 1] = -dY / d; RotationX_Inverse[1, 2] = dY / d;

                float[,] RotationY_Inverse = new float[4, 4]; // обратная матрица поворота по Y(поворот системы координат вокруг оси Y)
                Array.Clear(RotationY_Inverse, 0, 4);
                RotationY_Inverse[0, 0] = d; RotationY_Inverse[1, 1] = 1;
                RotationY_Inverse[2, 2] = d; RotationY_Inverse[3, 3] = 1;
                RotationY_Inverse[2, 0] = -dX; RotationY_Inverse[0, 2] = dX;
                RotationVec = Mul(Translation_Inverse, RotationX_Inverse); // умножение обратной матрицы переноса на обратную матрицу поворота по Х
                RotationVec = Mul(RotationVec, RotationY_Inverse);// умножение результата на обратную матрицу поворота по Y

                float[,] RotationZ = new float[4, 4]; // матрица поворота по Z
                Array.Clear(RotationZ, 0, 4);
                float sinZ = (float)Math.Sin(A_to_r(angle));
                float cosZ = (float)Math.Cos(A_to_r(angle));
                RotationZ[0, 0] = cosZ; RotationZ[1, 1] = cosZ;
                RotationZ[2, 2] = 1; RotationZ[3, 3] = 1;
                RotationZ[0, 1] = -sinZ; RotationZ[1, 0] = sinZ;

                RotationVec = Mul(RotationVec, RotationZ); // умножение результата на матрицу поворота по Z

                float[,] RotationY = new float[4, 4]; // матрица поворота по Y
                Array.Clear(RotationY, 0, 4);
                RotationY[0, 0] = d; RotationY[1, 1] = 1;
                RotationY[2, 2] = d; RotationY[3, 3] = 1;
                RotationY[2, 0] = dX; RotationY[0, 2] = -dX;

                RotationVec = Mul(RotationVec, RotationY); // умножение результата на матрицу поворота по Y

                float[,] RotationX = new float[4, 4];// матрица поворота по X
                Array.Clear(RotationX, 0, 4);
                RotationX[0, 0] = 1; RotationX[1, 1] = dZ / d;
                RotationX[2, 2] = dZ / d; RotationX[3, 3] = 1;
                RotationX[2, 1] = dY / d; RotationX[1, 2] = -dY / d;

                RotationVec = Mul(RotationVec, RotationX); // умножение результата на матрицу поворота по X
                RotationVec = Mul(RotationVec, Translation); // умножение результата на матрицу переноса(возврат вектора с начала координат)
            }
            else // случай в котором d=0
            {
                float[,] RotationX = new float[4, 4];
                Array.Clear(RotationX, 0, 4);
                float sinX = (float)Math.Sin(A_to_r(angle));
                float cosX = (float)Math.Cos(A_to_r(angle));
                RotationX[0, 0] = 1; RotationX[1, 1] = cosX;
                RotationX[2, 2] = cosX; RotationX[3, 3] = 1;
                RotationX[1, 2] = sinX; RotationX[2, 1] = -sinX;
                RotationVec = Mul(Translation_Inverse, RotationX);
                RotationVec = Mul(RotationVec, Translation);
            }
            List<Matrix3D> newCubePoints = new List<Matrix3D>();// список для новых координат куба после перемещений
            for (int i = 0; i < 8; i++)
            {
                Matrix3D points = cubePoints[i].Mul(RotationVec); // умножение результата на координаты куба
                newCubePoints.Add(points);
            }
            return newCubePoints;
        }
        private void FillCube(Graphics cb, List<Matrix3D> cube)
        {
            string colorCube = "LightBlue";
            if (comboColor.SelectedItem == null) comboColor.SelectedItem = colorCube;
            else colorCube = comboColor.SelectedItem.ToString();
            Brush brush1 = new SolidBrush(Color.FromName(colorCube));

            PointF[] figure1 = new PointF[4]; // 0 1 2 3
            PointF[] figure2 = new PointF[4]; // 4 5 6 7
            for (int i = 0; i < 4; i++)
            {
                figure1[i] = cube[i].ScreenPoint(scale);
                figure2[i] = cube[i + 4].ScreenPoint(scale);
            }
            cb.FillPolygon(brush1, figure1);
            cb.FillPolygon(brush1, figure2);

            PointF[] figure3 = new PointF[4]; // 0 4 5 1
            figure3[0] = cube[0].ScreenPoint(scale);
            figure3[1] = cube[4].ScreenPoint(scale);
            figure3[2] = cube[5].ScreenPoint(scale);
            figure3[3] = cube[1].ScreenPoint(scale);
            cb.FillPolygon(brush1, figure3);

            PointF[] figure4 = new PointF[4]; // 2 3 7 6
            figure4[0] = cube[2].ScreenPoint(scale);
            figure4[1] = cube[3].ScreenPoint(scale);
            figure4[2] = cube[7].ScreenPoint(scale);
            figure4[3] = cube[6].ScreenPoint(scale);
            cb.FillPolygon(brush1, figure4);

            PointF[] figure5 = new PointF[4]; // 1 2 6 5
            figure5[0] = cube[1].ScreenPoint(scale);
            figure5[1] = cube[2].ScreenPoint(scale);
            figure5[2] = cube[6].ScreenPoint(scale);
            figure5[3] = cube[5].ScreenPoint(scale);
            cb.FillPolygon(brush1, figure5);

            PointF[] figure6 = new PointF[4]; // 0 4 7 3
            figure6[0] = cube[0].ScreenPoint(scale);
            figure6[1] = cube[4].ScreenPoint(scale);
            figure6[2] = cube[7].ScreenPoint(scale);
            figure6[3] = cube[3].ScreenPoint(scale);
            cb.FillPolygon(brush1, figure6);
        }
        private void DrawCube(Graphics cb, List<Matrix3D> cube)
        {
            string[] alfabet = { "A", "B", "C", "D", "E", "F", "G", "H" };
            PointF start; PointF end;
            Pen pen = new Pen(Color.LightSeaGreen, 1);
            for (int j = 0; j < cube.Count; j += 4)
            {
                start = cube[j].ScreenPoint(scale);
                for (int i = j + 1; i < j + 4; i++)
                {
                    end = cube[i].ScreenPoint(scale);
                    cb.DrawLine(pen, start, end);
                    start = end;
                }
                cb.DrawLine(pen, start, cube[j].ScreenPoint(scale));
            }

            for (int i = 0; i < 4; i++)
            {
                cb.DrawLine(pen, cube[i].ScreenPoint(scale), cube[i + 4].ScreenPoint(scale));
            }
            if (checkBoxPoints.Checked)
                for (int i = 0; i < cube.Count; i++)
                    cb.DrawString(alfabet[i], new Font("Arial", scale / 6), new SolidBrush(Color.LightSeaGreen), cube[i].ScreenPoint(scale));
        }
        private List<Matrix3D> MoveCube(List<Matrix3D> cube, float dx = 0, float dy = 0, float dz = 0)
        {
            List<Matrix3D> rez = new List<Matrix3D>();
            for (int i = 0; i < cube.Count; i++)
            {
                Matrix3D fig = new Matrix3D(cube[i].GetX() + dx, cube[i].GetY() + dy, cube[i].GetZ() + dz);
                rez.Add(fig);
            }
            return rez;
        }
        private void DrawAxis(Graphics cb, Matrix3D start, Matrix3D end, Pen pen)
        {
            cb.DrawLine(pen, start.ScreenPoint(1), end.ScreenPoint(1));
        }
        private List<Matrix3D> Rotation(List<Matrix3D> cube, float dx = 0, float dy = 0, float dz = 0)
        {
            List<Matrix3D> rez = new List<Matrix3D>();

            for (int i = 0; i < cube.Count; i++)
            {
                Matrix3D fig = cube[i].RotationOX(dx);
                fig = fig.RotationOY(dy);
                fig = fig.RotationOZ(dz);
                rez.Add(fig);
            }
            return rez;
        }
        private void OrtProectionYZ(Graphics cb, List<Matrix3D> cube)
        {
            PointF start; PointF end;
            Pen pen = new Pen(Color.LightSalmon, 1)
            {
                DashStyle = DashStyle.DashDotDot
            };
            for (int j = 0; j < 8; j++)
            {
                cb.DrawLine(pen, cube[j].ScreenPointYZ(scale), cube[j].ScreenPoint(scale));
            }
            for (int j = 0; j < 8; j += 4)
            {
                start = cube[j].ScreenPointYZ(scale);
                for (int i = j + 1; i < j + 4; i++)
                {
                    end = cube[i].ScreenPointYZ(scale);
                    cb.DrawLine(pen, start, end);
                    start = end;
                }
                cb.DrawLine(pen, start, cube[j].ScreenPointYZ(scale));
            }
            for (int i = 0; i < 4; i++)
            {
                cb.DrawLine(pen, cube[i].ScreenPointYZ(scale), cube[i + 4].ScreenPointYZ(scale));
            }
        }
        private void OrtProectionXZ(Graphics cb, List<Matrix3D> cube)
        {
            PointF start; PointF end;
            Pen pen = new Pen(Color.LightSalmon, 1)
            {
                DashStyle = DashStyle.DashDotDot
            };
            for (int j = 0; j < 8; j++)
            {
                cb.DrawLine(pen, cube[j].ScreenPointXZ(scale), cube[j].ScreenPoint(scale));
            }
            for (int j = 0; j < 8; j += 4)
            {
                start = cube[j].ScreenPointXZ(scale);
                for (int i = j + 1; i < j + 4; i++)
                {
                    end = cube[i].ScreenPointXZ(scale);
                    cb.DrawLine(pen, start, end);
                    start = end;
                }
                cb.DrawLine(pen, start, cube[j].ScreenPointXZ(scale));
            }
            for (int i = 0; i < 4; i++)
            {
                cb.DrawLine(pen, cube[i].ScreenPointXZ(scale), cube[i + 4].ScreenPointXZ(scale));
            }
        }
        private void OrtProectionXY(Graphics cb, List<Matrix3D> cube)
        {
            PointF start; PointF end;
            Pen pen = new Pen(Color.LightSalmon, 1)
            {
                DashStyle = DashStyle.DashDotDot
            };
            for (int j = 0; j < 8; j++)
            {
                cb.DrawLine(pen, cube[j].ScreenPointXY(scale), cube[j].ScreenPoint(scale));
            }
            for (int j = 0; j < 8; j += 4)
            {
                start = cube[j].ScreenPointXY(scale);
                for (int i = j + 1; i < j + 4; i++)
                {
                    end = cube[i].ScreenPointXY(scale);
                    cb.DrawLine(pen, start, end);
                    start = end;
                }
                cb.DrawLine(pen, start, cube[j].ScreenPointXY(scale));
            }
            for (int i = 0; i < 4; i++)
            {
                cb.DrawLine(pen, cube[i].ScreenPointXY(scale), cube[i + 4].ScreenPointXY(scale));
            }
        }
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.TranslateTransform(x_center, y_center);
            gr.Clear(Color.White);

            Pen p = new Pen(Color.DarkGray, 1)
            {
                DashStyle = DashStyle.Dash
            };
            DrawAxis(gr, new Matrix3D(0, 0, 0), new Matrix3D(-300, 0, 0), p);
            DrawAxis(gr, new Matrix3D(0, 0, 0), new Matrix3D(0, -240, 0), p);
            DrawAxis(gr, new Matrix3D(0, 0, 0), new Matrix3D(0, 0, -300), p);

            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // оси х и у
            p = new Pen(Color.Black, 1)
            {
                CustomEndCap = new AdjustableArrowCap(4, 4)
            };
            DrawAxis(gr, new Matrix3D(0, 0, 0), new Matrix3D(300, 0, 0), p);
            DrawAxis(gr, new Matrix3D(0, 0, 0), new Matrix3D(0, 260, 0), p);
            DrawAxis(gr, new Matrix3D(0, 0, 0), new Matrix3D(0, 0, 450), p);

            Font drawFont = new Font("Arial", 8);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            gr.DrawString("X", drawFont, drawBrush, new PointF(300, 0));
            gr.DrawString("Y", drawFont, drawBrush, new PointF(0, -270));
            gr.DrawString("Z", drawFont, drawBrush, new Matrix3D(0, 20, 450).ScreenPoint(1));

            List<Matrix3D> c = Rotation(RotationVector(gr, MoveCube(Cube, trackBarX.Value, trackBarY.Value, trackBarZ.Value), trackBarRotateLine.Value),
                trackBarRotateX.Value, trackBarRotateY.Value, trackBarRotateZ.Value);

            if (proectXY) OrtProectionXY(gr, c);
            if (proectXZ) OrtProectionXZ(gr, c);
            if (proectYZ) OrtProectionYZ(gr, c);
            if (checkFilling.Checked) FillCube(gr, c);
            //KKProection(/*gr,*/ c);
            DrawCube(gr, c);

        }

        private void Invalidate(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

    }
}
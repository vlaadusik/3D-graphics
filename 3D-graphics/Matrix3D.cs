using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_graphics
{
    class Matrix3D
    {
        private readonly float[] matrix_xyz;
        public Matrix3D(float x, float y, float z)
        {
            matrix_xyz = new float[] { x, y, z, 1 };
        }
        public float GetX()
        {
            return matrix_xyz[0];
        }
        public float GetY()
        {
            return matrix_xyz[1];
        }
        public float GetZ()
        {
            return matrix_xyz[2];
        }
        public Matrix3D KavalyeKabine(double k, double alfa)
        {
            return new Matrix3D(GetX(), GetY(), -GetX() * (float)k * (float)Math.Cos(alfa) - GetY() * (float)k * (float)Math.Sin(alfa));
        }
        public Matrix3D RotationOX(float angle)
        {
            float degreeX = (float)(angle * Math.PI) / 180;
            float cosX = (float)Math.Cos(degreeX);
            float sinX = (float)Math.Sin(degreeX);
            return new Matrix3D(GetX(),
                    GetY() * cosX + GetZ() * sinX,
                   -GetY() * sinX + GetZ() * cosX);
        }
        public Matrix3D RotationOY(float angle)
        {
            float degreeY = (float)(angle * Math.PI) / 180;
            float cosY = (float)Math.Cos(degreeY);
            float sinY = (float)Math.Sin(degreeY);

            return new Matrix3D(GetX() * cosY + GetZ() * sinY,
                    GetY(),
                    -GetX() * sinY + GetZ() * cosY);

        }
        public Matrix3D RotationOZ(float angle)
        {
            float degreeZ = (float)(angle * Math.PI) / 180;
            float cosZ = (float)Math.Cos(degreeZ);
            float sinZ = (float)Math.Sin(degreeZ);

            return new Matrix3D(GetX() * cosZ - GetY() * sinZ,
                   +GetX() * sinZ + GetY() * cosZ,
                   GetZ());
        }
        private float A_to_r(float angle)
        {
            return (float)(angle * Math.PI) / 180.0f;
        }
        public PointF ScreenPoint(int scale)
        {
            return new PointF((GetX() - GetZ() /** (float)(k * Math.Cos(A_to_r(-(float)alfa)))*/ / 2.0f) * scale,
                -(GetY() - GetZ()/* * (float)(k * Math.Sin(A_to_r(-(float)alfa))*/ / 2.0f) * scale);
        }
        public PointF ScreenPointXY(int scale)
        {
            return new PointF((GetX()) * scale, -(GetY()) * scale);
        }
        public PointF ScreenPointXZ(int scale)
        {
            return new PointF((GetX() - GetZ() / 2) * scale, (GetZ() / 2) * scale);
        }
        public PointF ScreenPointYZ(int scale)
        {
            return new PointF((-GetZ() / 2) * scale, -(GetY() - GetZ() / 2) * scale);
        }
        private float[] result;
        public Matrix3D Mul(float[,] matrix)
        {
            float sum;
            result = new float[4];
            for (int j = 0; j < 4; j++)
            {
                sum = 0;
                for (int k = 0; k < 4; k++)
                    sum += matrix_xyz[k] * matrix[k, j];
                result[j] = sum;
            }
            Matrix3D ans = new Matrix3D(result[0], result[1], result[2]);
            return ans;
        }
    }
}

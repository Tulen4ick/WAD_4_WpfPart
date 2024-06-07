using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlLibrary1
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {

        public Point OldPoint = new Point();
        public Point NewPoint = new Point();
        public Point ClickPoint = new Point();
        public double cam_theta = 0;
        public double cam_phi = 0;
        public double cam_radius = 30;
        public Viewport3D myViewport3d = new Viewport3D();
        public PerspectiveCamera myPCamera = new PerspectiveCamera();
        public int IndexOfFunction;
        public MeshGeometry3D func1Mesh;
        public MeshGeometry3D func2Mesh;
        /*public MeshGeometry3D SetGeometry(float abs, double shift)
        {
            MeshGeometry3D myMeshGeometry3D = new MeshGeometry3D();
            for (double z = -abs; z <= abs; z += shift)
            {
                for (double x = -abs; x <= abs; x += shift)
                {
                    int CountOfPoints = (int)((2 * abs) / shift) + 1;
                    myMeshGeometry3D.TextureCoordinates.Add(new Point(((x + abs) / shift) / (CountOfPoints - 1), (((z + abs) / shift) / (CountOfPoints - 1))));
                    myMeshGeometry3D.Positions.Add(new Point3D(x, formulaList[IndexOfFunction].Calc(x, z), z));
                    if ((x > -abs) && (z > -abs))
                    {
                        int p1 = CountOfPoints * (int)Math.Round((z + abs) / shift) + (int)Math.Round((x + abs) / shift);
                        int p2 = CountOfPoints * (int)Math.Round((z + abs) / shift) + (int)Math.Round((x + abs) / shift - 1);
                        int p3 = CountOfPoints * (int)Math.Round((z + abs) / shift - 1) + (int)Math.Round((x + abs) / shift);
                        int p4 = CountOfPoints * (int)Math.Round((z + abs) / shift - 1) + (int)Math.Round((x + abs) / shift - 1);


                        myMeshGeometry3D.TriangleIndices.Add(p1);
                        myMeshGeometry3D.TriangleIndices.Add(p2);
                        myMeshGeometry3D.TriangleIndices.Add(p4);

                        myMeshGeometry3D.TriangleIndices.Add(p1);
                        myMeshGeometry3D.TriangleIndices.Add(p4);
                        myMeshGeometry3D.TriangleIndices.Add(p3);
                    }
                }
            }
            return myMeshGeometry3D;
        }*/

        public MeshGeometry3D SetGeometry(double[,] pos, double abs, double deltaX, double deltaY)
        {
            int Steps = pos.GetLength(0);
            double shift = (2*abs) / (Steps - 1);
            MeshGeometry3D myMeshGeometry3D = new MeshGeometry3D();
            for (int z = 0; z < Steps; z++)
            {
                double zPos = -abs + z * shift;
                for (int x = 0; x < Steps; x++)
                {
                    double xPos = -abs + x * shift;
                    myMeshGeometry3D.Positions.Add(new Point3D(xPos + deltaX, pos[z, x] + deltaY, zPos));
                    myMeshGeometry3D.TextureCoordinates.Add(new Point((double)x / (Steps - 1), (double)z / (Steps - 1)));
                    if ((z > 0) && (x > 0))
                    {
                        int p1 = Steps * z + x;
                        int p2 = Steps * z + x - 1;
                        int p3 = Steps * (z - 1) + x - 1;
                        int p4 = Steps * (z - 1) + x;
                        myMeshGeometry3D.TriangleIndices.Add(p1);
                        myMeshGeometry3D.TriangleIndices.Add(p2);
                        myMeshGeometry3D.TriangleIndices.Add(p3);
                        myMeshGeometry3D.TriangleIndices.Add(p1);
                        myMeshGeometry3D.TriangleIndices.Add(p3);
                        myMeshGeometry3D.TriangleIndices.Add(p4);
                    }
                }
            }
            return myMeshGeometry3D;
        }

        /*public MeshGeometry3D SetGeometryOverall(double[,] pos1, double[,] pos2, double abs, double delta1, double delta2)
        {
            int Steps = pos1.GetLength(0) + (int)pos1.GetLength(0)/4;
            double shift = (2 * abs) / (Steps - 1);
            MeshGeometry3D myMeshGeometry3D = new MeshGeometry3D();
            for (int z = 0; z < Steps; z++)
            {
                double zPos = -abs + z * shift;
                for (int x = 0; x < Steps; x++)
                {
                    double xPos = -abs + x * shift;
                    myMeshGeometry3D.Positions.Add(new Point3D(xPos + delta1, pos1[z, x], zPos));
                    myMeshGeometry3D.TextureCoordinates.Add(new Point((double)x / (Steps - 1), (double)z / (Steps - 1)));
                    if ((z > 0) && (x > 0))
                    {
                        int p1 = Steps * z + x;
                        int p2 = Steps * z + x - 1;
                        int p3 = Steps * (z - 1) + x - 1;
                        int p4 = Steps * (z - 1) + x;
                        myMeshGeometry3D.TriangleIndices.Add(p1);
                        myMeshGeometry3D.TriangleIndices.Add(p2);
                        myMeshGeometry3D.TriangleIndices.Add(p3);
                        myMeshGeometry3D.TriangleIndices.Add(p1);
                        myMeshGeometry3D.TriangleIndices.Add(p3);
                        myMeshGeometry3D.TriangleIndices.Add(p4);
                    }
                }
            }
            return myMeshGeometry3D;
        }*/

        public MeshGeometry3D YAxisGeometry(double c)
        {
            MeshGeometry3D myMeshGeometry3D = new MeshGeometry3D();

            myMeshGeometry3D.Positions.Add(new Point3D(-c, -100, -c));
            myMeshGeometry3D.Positions.Add(new Point3D(-c, -100, c));
            myMeshGeometry3D.Positions.Add(new Point3D(c, -100, c));
            myMeshGeometry3D.Positions.Add(new Point3D(c, -100, -c));

            myMeshGeometry3D.Positions.Add(new Point3D(-c, 100, -c));
            myMeshGeometry3D.Positions.Add(new Point3D(-c, 100, c));
            myMeshGeometry3D.Positions.Add(new Point3D(c, 100, c));
            myMeshGeometry3D.Positions.Add(new Point3D(c, 100, -c));

            myMeshGeometry3D.TriangleIndices.Add(0);
            myMeshGeometry3D.TriangleIndices.Add(4);
            myMeshGeometry3D.TriangleIndices.Add(5);

            myMeshGeometry3D.TriangleIndices.Add(0);
            myMeshGeometry3D.TriangleIndices.Add(1);
            myMeshGeometry3D.TriangleIndices.Add(5);

            myMeshGeometry3D.TriangleIndices.Add(1);
            myMeshGeometry3D.TriangleIndices.Add(5);
            myMeshGeometry3D.TriangleIndices.Add(6);

            myMeshGeometry3D.TriangleIndices.Add(1);
            myMeshGeometry3D.TriangleIndices.Add(2);
            myMeshGeometry3D.TriangleIndices.Add(6);


            myMeshGeometry3D.TriangleIndices.Add(2);
            myMeshGeometry3D.TriangleIndices.Add(6);
            myMeshGeometry3D.TriangleIndices.Add(7);

            myMeshGeometry3D.TriangleIndices.Add(2);
            myMeshGeometry3D.TriangleIndices.Add(3);
            myMeshGeometry3D.TriangleIndices.Add(7);

            myMeshGeometry3D.TriangleIndices.Add(3);
            myMeshGeometry3D.TriangleIndices.Add(7);
            myMeshGeometry3D.TriangleIndices.Add(4);

            myMeshGeometry3D.TriangleIndices.Add(3);
            myMeshGeometry3D.TriangleIndices.Add(0);
            myMeshGeometry3D.TriangleIndices.Add(4);
            return myMeshGeometry3D;
        }

        public MeshGeometry3D ZAxisGeometry(double c)
        {
            MeshGeometry3D myMeshGeometry3D = new MeshGeometry3D();

            myMeshGeometry3D.Positions.Add(new Point3D(-c, -c, -100));
            myMeshGeometry3D.Positions.Add(new Point3D(-c, c, -100));
            myMeshGeometry3D.Positions.Add(new Point3D(c, c, -100));
            myMeshGeometry3D.Positions.Add(new Point3D(c, -c, -100));

            myMeshGeometry3D.Positions.Add(new Point3D(-c, -c, 100));
            myMeshGeometry3D.Positions.Add(new Point3D(-c, c, 100));
            myMeshGeometry3D.Positions.Add(new Point3D(c, c, 100));
            myMeshGeometry3D.Positions.Add(new Point3D(c, -c, 100));

            myMeshGeometry3D.TriangleIndices.Add(0);
            myMeshGeometry3D.TriangleIndices.Add(4);
            myMeshGeometry3D.TriangleIndices.Add(5);

            myMeshGeometry3D.TriangleIndices.Add(0);
            myMeshGeometry3D.TriangleIndices.Add(1);
            myMeshGeometry3D.TriangleIndices.Add(5);

            myMeshGeometry3D.TriangleIndices.Add(1);
            myMeshGeometry3D.TriangleIndices.Add(5);
            myMeshGeometry3D.TriangleIndices.Add(6);

            myMeshGeometry3D.TriangleIndices.Add(1);
            myMeshGeometry3D.TriangleIndices.Add(2);
            myMeshGeometry3D.TriangleIndices.Add(6);


            myMeshGeometry3D.TriangleIndices.Add(2);
            myMeshGeometry3D.TriangleIndices.Add(6);
            myMeshGeometry3D.TriangleIndices.Add(7);

            myMeshGeometry3D.TriangleIndices.Add(2);
            myMeshGeometry3D.TriangleIndices.Add(3);
            myMeshGeometry3D.TriangleIndices.Add(7);

            myMeshGeometry3D.TriangleIndices.Add(3);
            myMeshGeometry3D.TriangleIndices.Add(7);
            myMeshGeometry3D.TriangleIndices.Add(4);

            myMeshGeometry3D.TriangleIndices.Add(3);
            myMeshGeometry3D.TriangleIndices.Add(0);
            myMeshGeometry3D.TriangleIndices.Add(4);
            return myMeshGeometry3D;
        }

        public MeshGeometry3D XAxisGeometry(double c)
        {
            MeshGeometry3D myMeshGeometry3D = new MeshGeometry3D();

            myMeshGeometry3D.Positions.Add(new Point3D(-100, -c, -c));
            myMeshGeometry3D.Positions.Add(new Point3D(-100, -c, c));
            myMeshGeometry3D.Positions.Add(new Point3D(-100, c, c));
            myMeshGeometry3D.Positions.Add(new Point3D(-100, c, -c));

            myMeshGeometry3D.Positions.Add(new Point3D(100, -c, -c));
            myMeshGeometry3D.Positions.Add(new Point3D(100, -c, c));
            myMeshGeometry3D.Positions.Add(new Point3D(100, c, c));
            myMeshGeometry3D.Positions.Add(new Point3D(100, c, -c));

            myMeshGeometry3D.TriangleIndices.Add(0);
            myMeshGeometry3D.TriangleIndices.Add(4);
            myMeshGeometry3D.TriangleIndices.Add(5);

            myMeshGeometry3D.TriangleIndices.Add(0);
            myMeshGeometry3D.TriangleIndices.Add(1);
            myMeshGeometry3D.TriangleIndices.Add(5);

            myMeshGeometry3D.TriangleIndices.Add(1);
            myMeshGeometry3D.TriangleIndices.Add(5);
            myMeshGeometry3D.TriangleIndices.Add(6);

            myMeshGeometry3D.TriangleIndices.Add(1);
            myMeshGeometry3D.TriangleIndices.Add(2);
            myMeshGeometry3D.TriangleIndices.Add(6);


            myMeshGeometry3D.TriangleIndices.Add(2);
            myMeshGeometry3D.TriangleIndices.Add(6);
            myMeshGeometry3D.TriangleIndices.Add(7);

            myMeshGeometry3D.TriangleIndices.Add(2);
            myMeshGeometry3D.TriangleIndices.Add(3);
            myMeshGeometry3D.TriangleIndices.Add(7);

            myMeshGeometry3D.TriangleIndices.Add(3);
            myMeshGeometry3D.TriangleIndices.Add(7);
            myMeshGeometry3D.TriangleIndices.Add(4);

            myMeshGeometry3D.TriangleIndices.Add(3);
            myMeshGeometry3D.TriangleIndices.Add(0);
            myMeshGeometry3D.TriangleIndices.Add(4);
            return myMeshGeometry3D;
        }

        public MeshGeometry3D CubeGeometry(double c)
        {
            MeshGeometry3D myMeshGeometry3D = new MeshGeometry3D();
            myMeshGeometry3D.Positions.Add(new Point3D(-c, -100, -c));
            myMeshGeometry3D.Positions.Add(new Point3D(-c, -100, c));
            myMeshGeometry3D.Positions.Add(new Point3D(c, -100, c));
            myMeshGeometry3D.Positions.Add(new Point3D(c, -100, -c));

            myMeshGeometry3D.Positions.Add(new Point3D(-c, 100, -c));
            myMeshGeometry3D.Positions.Add(new Point3D(-c, 100, c));
            myMeshGeometry3D.Positions.Add(new Point3D(c, 100, c));
            myMeshGeometry3D.Positions.Add(new Point3D(c, 100, -c));

            myMeshGeometry3D.TriangleIndices.Add(0);
            myMeshGeometry3D.TriangleIndices.Add(4);
            myMeshGeometry3D.TriangleIndices.Add(5);

            myMeshGeometry3D.TriangleIndices.Add(0);
            myMeshGeometry3D.TriangleIndices.Add(1);
            myMeshGeometry3D.TriangleIndices.Add(5);

            myMeshGeometry3D.TriangleIndices.Add(1);
            myMeshGeometry3D.TriangleIndices.Add(5);
            myMeshGeometry3D.TriangleIndices.Add(6);

            myMeshGeometry3D.TriangleIndices.Add(1);
            myMeshGeometry3D.TriangleIndices.Add(2);
            myMeshGeometry3D.TriangleIndices.Add(6);

            myMeshGeometry3D.TriangleIndices.Add(2);
            myMeshGeometry3D.TriangleIndices.Add(6);
            myMeshGeometry3D.TriangleIndices.Add(7);

            myMeshGeometry3D.TriangleIndices.Add(2);
            myMeshGeometry3D.TriangleIndices.Add(3);
            myMeshGeometry3D.TriangleIndices.Add(7);

            myMeshGeometry3D.TriangleIndices.Add(3);
            myMeshGeometry3D.TriangleIndices.Add(7);
            myMeshGeometry3D.TriangleIndices.Add(4);

            myMeshGeometry3D.TriangleIndices.Add(3);
            myMeshGeometry3D.TriangleIndices.Add(0);
            myMeshGeometry3D.TriangleIndices.Add(4);

            myMeshGeometry3D.TriangleIndices.Add(0);
            myMeshGeometry3D.TriangleIndices.Add(1);
            myMeshGeometry3D.TriangleIndices.Add(2);

            myMeshGeometry3D.TriangleIndices.Add(0);
            myMeshGeometry3D.TriangleIndices.Add(3);
            myMeshGeometry3D.TriangleIndices.Add(2);

            myMeshGeometry3D.TriangleIndices.Add(4);
            myMeshGeometry3D.TriangleIndices.Add(5);
            myMeshGeometry3D.TriangleIndices.Add(6);

            myMeshGeometry3D.TriangleIndices.Add(4);
            myMeshGeometry3D.TriangleIndices.Add(7);
            myMeshGeometry3D.TriangleIndices.Add(6);


            return myMeshGeometry3D;
        }
        public UserControl1()
        {
            InitializeComponent();
            Model3DGroup myModel3DGroup = new Model3DGroup();
            GeometryModel3D NowFormula = new GeometryModel3D();
            ModelVisual3D myModelVisual3D = new ModelVisual3D();

            double cam_x = cam_radius * Math.Cos(cam_phi) * Math.Sin(cam_theta);
            double cam_y = cam_radius * Math.Sin(cam_phi);
            double cam_z = cam_radius * Math.Cos(cam_phi) * Math.Cos(cam_theta);
            myPCamera.Position = new Point3D(cam_x, cam_y, cam_z);
            myPCamera.LookDirection = new Vector3D(-cam_x, -cam_y, -cam_z);
            myPCamera.FieldOfView = 60;
            myViewport3d.Camera = myPCamera;

            DirectionalLight myDirectionalLight = new DirectionalLight();
            myDirectionalLight.Color = Colors.White;
            myDirectionalLight.Direction = new Vector3D(0, 0, -1);

            AmbientLight myAmbientLight = new AmbientLight();
            myAmbientLight.Color = Colors.White;

            myModel3DGroup.Children.Add(myAmbientLight);

            GeometryModel3D YAxis = new GeometryModel3D();
            YAxis.Geometry = YAxisGeometry(0.01);
            GeometryModel3D XAxis = new GeometryModel3D();
            XAxis.Geometry = XAxisGeometry(0.01);
            GeometryModel3D ZAxis = new GeometryModel3D();
            ZAxis.Geometry = ZAxisGeometry(0.01);
            GeometryModel3D SceneEdges = new GeometryModel3D();
            SceneEdges.Geometry = YAxisGeometry(100);
            SceneEdges.Geometry = CubeGeometry(100);
            int Steps = (int)((2 * 4) / 0.1) + 1;
            double[,] pos = new double[Steps, Steps];
            for (int z = 0; z < Steps; z++)
            {
                for (int x = 0; x < Steps; x++)
                {
                    double xPos = -4 + x * 0.1;
                    double zPos = -4 + z * 0.1;
                    pos[z, x] = xPos*xPos + zPos*zPos;
                }
            }
            NowFormula.Geometry = SetGeometry(pos, 4, 0, 0);

            DiffuseMaterial materialBlue = new DiffuseMaterial();
            SolidColorBrush brushBlue = new SolidColorBrush(Colors.Blue);
            materialBlue.Brush = brushBlue;
            YAxis.Material = materialBlue;
            YAxis.BackMaterial = materialBlue;
            DiffuseMaterial materialRed = new DiffuseMaterial();
            SolidColorBrush brushRed = new SolidColorBrush(Colors.Red);
            materialRed.Brush = brushRed;
            XAxis.Material = materialRed;
            XAxis.BackMaterial = materialRed;
            DiffuseMaterial materialGreen = new DiffuseMaterial();
            SolidColorBrush brushGreen = new SolidColorBrush(Colors.Green);
            materialGreen.Brush = brushGreen;
            ZAxis.Material = materialGreen;
            ZAxis.BackMaterial = materialGreen;
            DiffuseMaterial Invisiblematerial = new DiffuseMaterial();
            SolidColorBrush Invisible = new SolidColorBrush(Colors.Transparent);
            Invisiblematerial.Brush = Invisible;
            SceneEdges.Material = Invisiblematerial;
            SceneEdges.BackMaterial = Invisiblematerial;

            LinearGradientBrush myHorizontalGradient = new LinearGradientBrush();
            myHorizontalGradient.StartPoint = new Point(0, 0.5);
            myHorizontalGradient.EndPoint = new Point(1, 0.5);
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Yellow, 0.0));
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Red, 0.25));
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Blue, 0.75));
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.LimeGreen, 1.0));

            DiffuseMaterial myMaterial = new DiffuseMaterial(myHorizontalGradient);
            NowFormula.Material = myMaterial;
            NowFormula.BackMaterial = myMaterial;
            myModel3DGroup.Children.Add(SceneEdges);
            myModel3DGroup.Children.Add(NowFormula);
            myModel3DGroup.Children.Add(YAxis);
            myModel3DGroup.Children.Add(XAxis);
            myModel3DGroup.Children.Add(ZAxis);
            myModelVisual3D.Content = myModel3DGroup;
            myViewport3d.Children.Add(myModelVisual3D);
            this.Content = myViewport3d;
        }
        public UserControl1(double[,] pos)
        {
            InitializeComponent();
            Model3DGroup myModel3DGroup = new Model3DGroup();
            GeometryModel3D NowFormula = new GeometryModel3D();
            ModelVisual3D myModelVisual3D = new ModelVisual3D();

            double cam_x = cam_radius * Math.Cos(cam_phi) * Math.Sin(cam_theta);
            double cam_y = cam_radius * Math.Sin(cam_phi);
            double cam_z = cam_radius * Math.Cos(cam_phi) * Math.Cos(cam_theta);
            myPCamera.Position = new Point3D(cam_x, cam_y, cam_z);
            myPCamera.LookDirection = new Vector3D(-cam_x, -cam_y, -cam_z);
            myPCamera.FieldOfView = 60;
            myViewport3d.Camera = myPCamera;

            /*DirectionalLight myDirectionalLight = new DirectionalLight();
            myDirectionalLight.Color = Colors.White;
            myDirectionalLight.Direction = new Vector3D(0, 0, -1);*/

            AmbientLight myAmbientLight = new AmbientLight();
            myAmbientLight.Color = Colors.White;

            myModel3DGroup.Children.Add(myAmbientLight);

            GeometryModel3D YAxis = new GeometryModel3D();
            YAxis.Geometry = YAxisGeometry(0.01);
            GeometryModel3D XAxis = new GeometryModel3D();
            XAxis.Geometry = XAxisGeometry(0.01);
            GeometryModel3D ZAxis = new GeometryModel3D();
            ZAxis.Geometry = ZAxisGeometry(0.01);
            GeometryModel3D SceneEdges = new GeometryModel3D();
            SceneEdges.Geometry = CubeGeometry(100);
            NowFormula.Geometry = SetGeometry(pos, 4, 0, 0);

            DiffuseMaterial materialBlue = new DiffuseMaterial();
            SolidColorBrush brushBlue = new SolidColorBrush(Colors.Blue);
            materialBlue.Brush = brushBlue;
            YAxis.Material = materialBlue;
            YAxis.BackMaterial = materialBlue;
            DiffuseMaterial materialRed = new DiffuseMaterial();
            SolidColorBrush brushRed = new SolidColorBrush(Colors.Red);
            materialRed.Brush = brushRed;
            XAxis.Material = materialRed;
            XAxis.BackMaterial = materialRed;
            DiffuseMaterial materialGreen = new DiffuseMaterial();
            SolidColorBrush brushGreen = new SolidColorBrush(Colors.Green);
            materialGreen.Brush = brushGreen;
            ZAxis.Material = materialGreen;
            ZAxis.BackMaterial = materialGreen;
            DiffuseMaterial Invisiblematerial = new DiffuseMaterial();
            SolidColorBrush Invisible = new SolidColorBrush(Colors.LightGray);
            Invisiblematerial.Brush = Invisible;
            SceneEdges.Material = Invisiblematerial;
            SceneEdges.BackMaterial = Invisiblematerial;

            LinearGradientBrush myHorizontalGradient = new LinearGradientBrush();
            myHorizontalGradient.StartPoint = new Point(0, 0.5);
            myHorizontalGradient.EndPoint = new Point(1, 0.5);
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Yellow, 0.0));
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Red, 0.25));
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Blue, 0.75));
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.LimeGreen, 1.0));

            DiffuseMaterial myMaterial = new DiffuseMaterial(myHorizontalGradient);
            NowFormula.Material = myMaterial;
            NowFormula.BackMaterial = myMaterial;
            myModel3DGroup.Children.Add(SceneEdges);
            myModel3DGroup.Children.Add(NowFormula);
            myModel3DGroup.Children.Add(YAxis);
            myModel3DGroup.Children.Add(XAxis);
            myModel3DGroup.Children.Add(ZAxis);
            myModelVisual3D.Content = myModel3DGroup;
            myViewport3d.Children.Add(myModelVisual3D);
            this.Content = myViewport3d;
        }

        public UserControl1(double[,] pos1, double[,] pos2, double[,] pos, double f1X, double f2X)
        {
            InitializeComponent();
            Model3DGroup myModel3DGroup = new Model3DGroup();
            GeometryModel3D NowFormula1 = new GeometryModel3D();
            GeometryModel3D NowFormula2 = new GeometryModel3D();
            GeometryModel3D NowFormula3 = new GeometryModel3D();
            ModelVisual3D myModelVisual3D = new ModelVisual3D();

            double cam_x = cam_radius * Math.Cos(cam_phi) * Math.Sin(cam_theta);
            double cam_y = cam_radius * Math.Sin(cam_phi);
            double cam_z = cam_radius * Math.Cos(cam_phi) * Math.Cos(cam_theta);
            myPCamera.Position = new Point3D(cam_x, cam_y, cam_z);
            myPCamera.LookDirection = new Vector3D(-cam_x, -cam_y, -cam_z);
            myPCamera.FieldOfView = 60;
            myViewport3d.Camera = myPCamera;

            /*DirectionalLight myDirectionalLight = new DirectionalLight();
            myDirectionalLight.Color = Colors.White;
            myDirectionalLight.Direction = new Vector3D(0, 0, -1);*/

            AmbientLight myAmbientLight = new AmbientLight();
            myAmbientLight.Color = Colors.White;

            myModel3DGroup.Children.Add(myAmbientLight);

            GeometryModel3D YAxis = new GeometryModel3D();
            YAxis.Geometry = YAxisGeometry(0.01);
            GeometryModel3D XAxis = new GeometryModel3D();
            XAxis.Geometry = XAxisGeometry(0.01);
            GeometryModel3D ZAxis = new GeometryModel3D();
            ZAxis.Geometry = ZAxisGeometry(0.01);
            GeometryModel3D SceneEdges = new GeometryModel3D();
            SceneEdges.Geometry = CubeGeometry(100);
            func1Mesh = SetGeometry(pos1, 4, f1X, 15);
            func2Mesh = SetGeometry(pos2, 4, f2X, 15);
            NowFormula1.Geometry = func1Mesh;
            NowFormula2.Geometry = func2Mesh;
            NowFormula3.Geometry = SetGeometry(pos, 9, 0, -15);

            DiffuseMaterial materialBlue = new DiffuseMaterial();
            SolidColorBrush brushBlue = new SolidColorBrush(Colors.Blue);
            materialBlue.Brush = brushBlue;
            YAxis.Material = materialBlue;
            YAxis.BackMaterial = materialBlue;
            DiffuseMaterial materialRed = new DiffuseMaterial();
            SolidColorBrush brushRed = new SolidColorBrush(Colors.Red);
            materialRed.Brush = brushRed;
            XAxis.Material = materialRed;
            XAxis.BackMaterial = materialRed;
            DiffuseMaterial materialGreen = new DiffuseMaterial();
            SolidColorBrush brushGreen = new SolidColorBrush(Colors.Green);
            materialGreen.Brush = brushGreen;
            ZAxis.Material = materialGreen;
            ZAxis.BackMaterial = materialGreen;
            DiffuseMaterial Invisiblematerial = new DiffuseMaterial();
            SolidColorBrush Invisible = new SolidColorBrush(Colors.LightGray);
            Invisiblematerial.Brush = Invisible;
            SceneEdges.Material = Invisiblematerial;
            SceneEdges.BackMaterial = Invisiblematerial;

            LinearGradientBrush myHorizontalGradient = new LinearGradientBrush();
            myHorizontalGradient.StartPoint = new Point(0, 0.5);
            myHorizontalGradient.EndPoint = new Point(1, 0.5);
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Yellow, 0.0));
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Red, 0.25));
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Blue, 0.75));
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.LimeGreen, 1.0));

            DiffuseMaterial myMaterial = new DiffuseMaterial(myHorizontalGradient);
            NowFormula1.Material = myMaterial;
            NowFormula1.BackMaterial = myMaterial;
            NowFormula2.Material = myMaterial;
            NowFormula2.BackMaterial = myMaterial;
            NowFormula3.Material = myMaterial;
            NowFormula3.BackMaterial = myMaterial;
            myModel3DGroup.Children.Add(SceneEdges);
            myModel3DGroup.Children.Add(NowFormula1);
            myModel3DGroup.Children.Add(NowFormula2);
            myModel3DGroup.Children.Add(NowFormula3);
            myModel3DGroup.Children.Add(YAxis);
            myModel3DGroup.Children.Add(XAxis);
            myModel3DGroup.Children.Add(ZAxis);
            myModelVisual3D.Content = myModel3DGroup;
            myViewport3d.Children.Add(myModelVisual3D);
            this.Content = myViewport3d;
        }
    }
}

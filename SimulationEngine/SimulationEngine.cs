using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SimulationEngine
{
    public class SurfacePoint
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Height { get; set; }
        public List<SurfacePoint> Neightbours { get; set; }
        public SurfacePoint(int x, int y, float height)
        {
            X = x;
            Y = y;
            Height = height;
        }

        public void AddNeightbours(List<SurfacePoint> neightbours)
        {
            Neightbours = neightbours;
        }
    }

    public class Surface
    {
        public float WaveLen { get; set; }
        //
        public int Resolution { get; set; }

        public bool SummerWaves { get; set; }
        //dafult value is true. if you set it to false, then no nice waves will be generated
        public List<List<SurfacePoint>> Grid { get; set; }
        private List<SurfacePoint> Sources { get; set; }

        float GetNewHeight(SurfacePoint point, double deltaTime)
        {
            float average = 0;
            foreach (var item in point.Neightbours)
            {
                average += item.Height * (float)deltaTime / 1000;
            }

            average /= point.Neightbours.Count();

            float difference = average - point.Height;

            return point.Height + difference * 0.8f;
        }


        public float Distance(SurfacePoint a, SurfacePoint b)
        {
            return (float)Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2));
        }

        private long m_iter;
        public float DampenFactor { get; set; }
        //default value is 0.8f. The higher you set it, the smaller the waves will get
        public float SimualtionSpeed { get; set; }
        //default value is 1. If you set it lower, the simulation will be computed in smaller timesteps
        public List<List<SurfacePoint>> Recalculate(double deltaTime)
        {


            List<List<float>> newHeights = new List<List<float>>();

            for (int i = 0; i < Resolution; i++)
            {
                List<float> temp = new List<float>();
                for (int j = 0; j < Resolution; j++)
                {

                    temp.Add(0);

                }
                newHeights.Add(temp);
            }

            for (int i = 0; i < Resolution; i++)
            {
                for (int j = 0; j < Resolution; j++)
                {

                    if (SummerWaves)
                    {
                        float height = 0;
                        foreach (var source in Sources)
                        {
                            float dist = Distance(source, Grid[i][j]);
                            height += source.Height / (float)Math.Pow(dist, DampenFactor) * (float)Math.Cos(dist/WaveLen - m_iter / (60/SimualtionSpeed) * 2f * Math.PI % (4 * Math.PI));
                        }
                        newHeights[i][j] = height;
                    }
                    else
                    {
                        newHeights[i][j] = GetNewHeight(Grid[i][j], deltaTime);
                    }
                }
            }


            SetNewHeights(newHeights);
            m_iter++;
            return Grid;
        }

        private void GeneratePoints()
        {
            Grid = new List<List<SurfacePoint>>();
            for (int i = 0; i < Resolution; i++)
            {
                List<SurfacePoint> temp = new List<SurfacePoint>();
                for (int j = 0; j < Resolution; j++)
                {

                    temp.Add(new SurfacePoint(i, j, 0));

                }
                Grid.Add(temp);
            }

            for (int i = 0; i < Resolution; i++)
            {
                for (int j = 0; j < Resolution; j++)
                {
                    List<SurfacePoint> neightbours = new List<SurfacePoint>();
                    // that's our point m_surface[i][j]
                    //  these are the neightbours
                    // *   *   *   *   *
                    // *   N   N   N   *
                    // *   N   P   N   *
                    // *   N   N   N   *
                    // *   *   *   *   *
                    // P has coords 0,0
                    List<Tuple<int, int>> neightCoord = new List<Tuple<int, int>>();
                    neightCoord.Add(new Tuple<int, int>(-1, -1));
                    neightCoord.Add(new Tuple<int, int>(-1, 0));
                    neightCoord.Add(new Tuple<int, int>(-1, 1));
                    neightCoord.Add(new Tuple<int, int>(0, -1));
                    // neightCoord.Add(new Tuple<int, int>(0, 0)); that's the point xd
                    neightCoord.Add(new Tuple<int, int>(0, 1));
                    neightCoord.Add(new Tuple<int, int>(1, -1));
                    neightCoord.Add(new Tuple<int, int>(1, 0));
                    neightCoord.Add(new Tuple<int, int>(1, 1));

                    foreach (var coords in neightCoord)
                    {
                        if ((i + coords.Item1 >= 0) && (i + coords.Item1 < Resolution)
                            && (j + coords.Item2 >= 0) && (j + coords.Item2 < Resolution))
                        {
                            neightbours.Add(Grid[i + coords.Item1][j + coords.Item2]);
                        }
                    }
                    (Grid[i][j]).AddNeightbours(neightbours);
                }
            }
        }

        public void SetNewHeights(List<List<float>> heights)
        {
            for (int i = 0; i < Resolution; i++)
            {
                for (int j = 0; j < Resolution; j++)
                {
                    Grid[i][j].Height = heights[i][j];
                }
            }
        }

        public Surface( int resolution)
        {
            WaveLen = 4;
            Resolution = resolution;
            GeneratePoints();
            SummerWaves = true;
            Sources = new List<SurfacePoint>();
            m_iter = 0;
            DampenFactor = 1f;
            SimualtionSpeed = 1f;
        }

        public void AddSource(SurfacePoint source)
        {
            Sources.Add(source);
        }

    }

    public class SimulationEngine
    {
        private readonly string args;

        public SimulationEngine(string args)
        {
            this.args = args ?? throw new ArgumentNullException(nameof(args));
        }
    }
}

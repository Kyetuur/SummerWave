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
        private double m_reynoldsNum;
        public SurfacePoint(int x, int y, double reynoldsNum)
        {
            X = x;
            Y = y;
            Height = 0;
            m_reynoldsNum = reynoldsNum;
        }

        public void AddNeightbours(List<SurfacePoint> neightbours)
        {
            Neightbours = neightbours;
        }
    }

    public class Surface
    {
        // https://en.wikipedia.org/wiki/Reynolds_number
        // So, basically, we won't use all the properties of reyNum, but we'll use it
        // to "dampen" the fluid surface
        private readonly float WaveLen;
        //
        public int m_resolution { get; set; }

        public bool SummerWaves { get; set; }
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


        private float Distance(SurfacePoint a, SurfacePoint b)
        {
            return (float)Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2));
        }

        private long m_iter;
        public float DampenFactor { get; set; }
        public float SimualtionSpeed { get; set; }
        public List<List<SurfacePoint>> Recalculate(double deltaTime)
        {


            List<List<float>> newHeights = new List<List<float>>();

            for (int i = 0; i < m_resolution; i++)
            {
                List<float> temp = new List<float>();
                for (int j = 0; j < m_resolution; j++)
                {

                    temp.Add(0);

                }
                newHeights.Add(temp);
            }

            for (int i = 0; i < m_resolution; i++)
            {
                for (int j = 0; j < m_resolution; j++)
                {

                    if (SummerWaves)
                    {
                        float height = 0;
                        foreach (var source in Sources)
                        {
                            float dist = Distance(source, Grid[i][j]);
                            height += 100f / (float)Math.Pow(dist, DampenFactor) * (float)Math.Sin(dist * WaveLen + m_iter / SimualtionSpeed * 2f * Math.PI % (4 * Math.PI));
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
            for (int i = 0; i < m_resolution; i++)
            {
                List<SurfacePoint> temp = new List<SurfacePoint>();
                for (int j = 0; j < m_resolution; j++)
                {

                    temp.Add(new SurfacePoint(i, j, WaveLen));

                }
                Grid.Add(temp);
            }

            for (int i = 0; i < m_resolution; i++)
            {
                for (int j = 0; j < m_resolution; j++)
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
                        if ((i + coords.Item1 >= 0) && (i + coords.Item1 < m_resolution)
                            && (j + coords.Item2 >= 0) && (j + coords.Item2 < m_resolution))
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
            for (int i = 0; i < m_resolution; i++)
            {
                for (int j = 0; j < m_resolution; j++)
                {
                    Grid[i][j].Height = heights[i][j];
                }
            }
        }

        public Surface(float waveLen, int resolution)
        {
            WaveLen = waveLen;
            m_resolution = resolution;
            GeneratePoints();
            SummerWaves = false;
            Sources = new List<SurfacePoint>();
            m_iter = 0;
            DampenFactor = 0.8f;
            SimualtionSpeed = 120;
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

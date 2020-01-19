using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SimulationEngine
{
    public class SurfacePoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double Height { get; set; }
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
        private readonly double m_reynoldsNum;
        //
        private readonly int m_resolution;

        public List<List<SurfacePoint>> grid { get; set; }

        double GetNewHeight(SurfacePoint point)
        {
            double average = 0;
            foreach (var item in point.Neightbours)
            {
                average += item.Height;
            }

            average /= point.Neightbours.Count();

            double difference = average - point.Height;

            return point.Height + difference * 0.8;
        }

        public long Recalculate(double timestep)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            List<List<double>> newHeights = new List<List<double>>();
            for (int i = 0; i < m_resolution; i++)
            {
                List<double> temp = new List<double>();
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
                    newHeights[i][j] = GetNewHeight(grid[i][j]);
                }
            }

            SetNewHeights(newHeights);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private void GeneratePoints()
        {
            grid = new List<List<SurfacePoint>>();
            for (int i = 0; i < m_resolution; i++)
            {
                List<SurfacePoint> temp = new List<SurfacePoint>();
                for (int j = 0; j < m_resolution; j++)
                {

                    temp.Add(new SurfacePoint(i, j, m_reynoldsNum));

                }
                grid.Add(temp);
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
                            neightbours.Add(grid[i + coords.Item1][j + coords.Item2]);
                        }
                    }
                    (grid[i][j]).AddNeightbours(neightbours);
                }
            }
        }

        public void SetNewHeights(List<List<double>> heights)
        {
            for (int i = 0; i < m_resolution; i++)
            {
                for (int j = 0; j < m_resolution; j++)
                {
                    grid[i][j].Height = heights[i][j];
                }
            }
        }

        public Surface(double reynoldsNum, int resolution)
        {
            m_reynoldsNum = reynoldsNum;
            m_resolution = resolution;
            GeneratePoints();
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

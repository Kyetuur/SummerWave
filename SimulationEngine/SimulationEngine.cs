using System;

namespace SimulationEngine
{
    public class SimulationEngine
    {
        private readonly string args;

        public SimulationEngine(string args)
        {
            this.args = args ?? throw new ArgumentNullException(nameof(args));
        }
    }
}

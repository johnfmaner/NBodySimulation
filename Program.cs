using System.Numerics;

namespace NBodySimulation
{
    class Program
    {
        public static void Main(string[] args)
        {
            float t = 0; // simulation time  
            float tSimulation = 10; // total time to run this simulation 
            float timeStep; // time differential 
            float n; // number of steps to take in simulation 

            n = 100; 
            timeStep = tSimulation / n;

            //initalize a new body 
            Body planet = new Body(new Vector3(0, 0, 0), new Vector3(1, 0, 0), 100, timeStep);

            // perform the simulation 
            while (t <= tSimulation)
            {
                Console.WriteLine("{0},{1},{2},{3}",t,planet.r.X,planet.r.Y,planet.r.Z);
                planet.EulerStep();
                t += timeStep;  
            }
        }
    }

    class Body 
    {
        public float m; // mass 
        public float dt; //time step 
        public Vector3 r; // position vector 
        public Vector3 v; // velocity vector 

        public Body(Vector3 r, Vector3 v, float m, float dt)
        {
            this.m = m;
            this.r = r; 
            this.v = v; 
            this.dt = dt;
        }

        public void Accelerate()
        {
            // to do: update velocity 
        }

        public void EulerStep()
        {
            Accelerate(); // update velocity by applying acceleration 
            r += v * dt; // update position vector by multiplying new velocity by dt 
        }



    }
}
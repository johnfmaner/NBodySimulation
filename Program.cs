using System.Numerics;

namespace NBodySimulation
{
    class Simulation
    {
        public static void Main(string[] args)
        {
            float t = 0; // simulation time  
            float tSimulation = 100; // total time to run this simulation 
            float timeStep; // time differential 
            int tResolution; // number of steps to take in simulation 
            int n; // number of bodies, not including the sun 
            bool hasSun; // indicate whether a sun object at 0,0,0 with mass = 1000 should be created 
            List<Body> Bodies = new List<Body>(); // store all bodies in the simulation 

            // configure simulation parameters 
            // to do: move to command line arguments 
            n = 1;
            hasSun = true;
            tResolution = 1000;
            timeStep = tSimulation / tResolution;


            // create a sun with id 0, if requested 
            if (hasSun)
            {
                Bodies.Add(new Body(new Vector3(0, 0, 0), new Vector3(0, 0, 0), 1000, timeStep, 0));
            }

            //initalize a new body with position, velocity, mass, and dt 
            /*Body planet = new Body(new Vector3(0, 0, 0), new Vector3(0, 0, 0), 1000, timeStep, 1) ;
            Body rock = new Body(new Vector3(-12, 0, 0), new Vector3(0, 10, 0), 1, timeStep, 2);
            Body anotherOne = new Body(new Vector3(-13, 0, 0), new Vector3(0, 10.5f, 0), 1, timeStep, 3);
            */

            // initial conditions for an orbit with GravityConst = 1
            //Body planet = new Body(new Vector3(0, 0, 0), new Vector3(0, 0, 0), 1000, timeStep);
            //Body rock = new Body(new Vector3(-10, 0, 0), new Vector3(0, 10, 0), 1, timeStep);

            // initalize n bodies with random parameters
            /*for (int i = 1; i <= n; i++)
            {
                Random random = new Random();
                Bodies.Add(new Body(new Vector3(random.Next(-100, 100), random.Next(-100, 100), 0), new Vector3(random.Next(-10, 10), random.Next(-10, 10), 0), 1, timeStep, i));
            }*/

            Bodies.Add(new Body(new Vector3(-10f, 0, 0), new Vector3(0, 10f, 0), 1f, timeStep, 1));
            //Bodies.Add(new Body(new Vector3(-20, 0, 0), new Vector3(0, 5, 0), 1, timeStep, 2));

            // perform the simulation 
            while (t <= tSimulation)
            {
                foreach (Body b in Bodies)
                {
                    Console.WriteLine("{0},{1},{2},{3}", t, b.id, b.r.X, b.r.Y);
                    b.EulerStep(Bodies);
                }
                t += timeStep;  
            }
            // keep console open 
            Console.ReadKey();
        }
    }
}
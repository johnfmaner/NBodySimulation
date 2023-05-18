using System.Numerics;

namespace NBodySimulation
{
    class Simulation
    {
        public static void Main(string[] args)
        {
            // simulation parameters 
            float simulationTime = 0; // simulation time  
            float simulationDuration = 10; // total time to run this simulation 
            float timeStep; // time differential 
            int simulationTimeSteps; // number of steps to take in simulation 
            int simulationBodyCount; // number of bodies, not including the sun 
            bool simulationHasSun; // indicate whether a sun object at 0,0,0 with mass = 1000 should be created 
            List<Body> simulationBodies = new List<Body>(); // store all bodies in the simulation 

            // sun parameters 
            Vector3 sunPosition = new Vector3(0, 0, 0);
            Vector3 sunVelocity = new Vector3(0, 0, 0);
            float sunMass = 1000;
            int sunBodyId = 0; //always initalize as 0th bodyId  

            // configure simulation parameters 
            // to do: move to command line arguments 
            simulationBodyCount = 1;
            simulationHasSun = true;
            simulationTimeSteps = 1000;
            timeStep = simulationDuration / simulationTimeSteps;  

            // create a sun with id 0, if requested 
            if (simulationHasSun)
            {
                simulationBodies.Add(new Body(sunPosition, sunVelocity, sunMass, timeStep, sunBodyId));
            }

            // initalize n bodies with random parameters
            /*for (int i = 1; i <= n; i++)
            {
                Random random = new Random();
                Bodies.Add(new Body(new Vector3(random.Next(-100, 100), random.Next(-100, 100), 0), new Vector3(random.Next(-10, 10), random.Next(-10, 10), 0), 1, timeStep, i));
            }*/

            simulationBodies.Add(new Body(new Vector3(-10f, 0, 0), new Vector3(0, 10f, 0), 1f, timeStep, 1));

            // perform the simulation 
            while (simulationTime <= simulationDuration)
            {
                foreach (Body workingBody in simulationBodies)
                {
                    Console.WriteLine("{0},{1},{2},{3}", simulationTime, workingBody.id, workingBody.positionVector.X, workingBody.positionVector.Y);
                    workingBody.EulerStep(simulationBodies);
                }
                simulationTime += timeStep;  
            }

            // keep console open 
            Console.ReadKey();
        }
    }
}
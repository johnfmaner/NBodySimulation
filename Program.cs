using System.Numerics;

namespace NBodySimulation
{
    class Simulation
    {
        public static void Main(string[] args)
        {
            // simulation parameters 
            float simulationTime = 0; // simulation time  
            float simulationDuration = 500; // total time to run this simulation 
            float timeStep; // time differential 
            int simulationTimeSteps; // number of steps to take in simulation 
            int simulationBodyCount; // number of bodies, including the sun 
            bool simulationHasSun; // indicate whether a sun object at 0,0,0 with mass = 1000 should be created 
            List<Body> simulationBodies = new List<Body>(); // store all bodies in the simulation 

            // sun parameters 
            Vector3 sunPosition = new Vector3(0, 0, 0);
            Vector3 sunVelocity = new Vector3(0, 0, 0);
            float sunMass = 1000;
            int sunBodyId = 1; //always initalize as 1st bodyId  
            float sunRadius = 2; 

            // configure simulation parameters 
            // to do: move to command line arguments 
            simulationBodyCount = 10;
            simulationHasSun = true;
            simulationTimeSteps = 1000;
            timeStep = simulationDuration / simulationTimeSteps;  

            // create a sun with id 0, if requested 
            if (simulationHasSun)
            {
                simulationBodies.Add(new Body(sunPosition, sunVelocity, sunMass, timeStep, sunBodyId, sunRadius));
            }

            Random rnd = new Random();
            for (int i = 2; i < simulationBodyCount + 2; i++)
            {
                float randomX = (float)rnd.Next(-20, 20);
                float randomY = (float)rnd.Next(-20, 20);
                float randomVx = (float)rnd.Next(-5, 5);
                float randomVy = (float)rnd.Next(-5, 5);
                float randomMass = (float)rnd.Next(1, 5);

                simulationBodies.Add(new Body(
                        new Vector3(randomX, randomY, 0),
                        new Vector3(randomVx, randomVy, 0),
                        randomMass,
                        timeStep,
                        i,
                        1));

            }

            // initalize more bodies 
            //simulationBodies.Add(new Body(new Vector3(-10f,10f, 0), new Vector3(10f, -10f, 0), 1f, timeStep, 2, 1));
            //simulationBodies.Add(new Body(new Vector3(-10f, -10f, 0), new Vector3(10f, 10f, 0), 1f, timeStep, 3, 1));
            //simulationBodies.Add(new Body(new Vector3(-2.5f, 0, 0), new Vector3(0, 15f, 0), 1f, timeStep, 3, 1));
            //simulationBodies.Add(new Body(new Vector3(-20f, -10f, 0), new Vector3(1f, 2f, 0), 0.01f, timeStep, 4, 1));

            // remove bodies which are going to collide on the first time step 
            List<Body> earlyCollisionBodiesToRemove = new List<Body>();

            foreach (Body body in simulationBodies)
            {
                body.CollisionCheck(simulationBodies);

                if (body.isCollided && body.id != sunBodyId) // don't delete the sun at t=0, but delete  bodies spawning inside the sun 
                {
                    earlyCollisionBodiesToRemove.Add(body); // tag the colliding body for deletion 
                }
            }

            simulationBodies.RemoveAll(x => earlyCollisionBodiesToRemove.Contains(x)); //remove any bodies which collided 
            simulationBodyCount = simulationBodies.Count(); // update count of simulation bodies 

            // perform the simulation 
            while (simulationTime <= simulationDuration && simulationBodyCount > 0)
            {
                List<Body> collidingBodiesToRemove = new List<Body>();

                foreach (Body workingBody in simulationBodies.ToList())
                {
                    Console.WriteLine(
                        "{0},{1},{2},{3},{4}",
                        simulationTime,
                        workingBody.id,
                        workingBody.positionVector.X,
                        workingBody.positionVector.Y,
                        workingBody.mass);

                    workingBody.EulerStep(simulationBodies); // move the body using an Euler step 

                    if (workingBody.isCollided) // check for collisions 
                    {
                        Body collidingBody = simulationBodies.Find(x => x.id == workingBody.collidedWithId); // find the colliding body 
                        //Console.WriteLine(collidingBody.id); 
                        workingBody.PerfectInelasticCollision(collidingBody); // collide them 
                        collidingBodiesToRemove.Add(collidingBody); // tag the colliding body for deletion 
                        workingBody.isCollided = false; // now, treat the working body as a new body 
                    }

                    simulationBodies.RemoveAll(x => collidingBodiesToRemove.Contains(x)); //remove any bodies which collided 
                    collidingBodiesToRemove.Clear(); 
                    simulationBodyCount = simulationBodies.Count(); // update count of simulation bodies 
                }
                simulationTime += timeStep; // advance time 
            }

            // keep console open 
            Console.ReadKey();
        }
    }
}
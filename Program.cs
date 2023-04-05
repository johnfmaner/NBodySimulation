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

            n = 1000; 
            timeStep = tSimulation / n;

            //initalize a new body with position, velocity, mass, and dt 
            Body planet = new Body(new Vector3(0, 0, 0), new Vector3(0, 0, 0), 1000, timeStep);
            Body rock = new Body(new Vector3(-10, 0, 0), new Vector3(0, 10, 0), 1, timeStep);

            // initial conditions for an orbit with GravityConst = 1
            //Body planet = new Body(new Vector3(0, 0, 0), new Vector3(0, 0, 0), 1000, timeStep);
            //Body rock = new Body(new Vector3(-10, 0, 0), new Vector3(0, 10, 0), 1, timeStep);

            // perform the simulation 
            while (t <= tSimulation)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}",t,rock.r.X,rock.r.Y,planet.r.X,planet.r.Y);
                planet.EulerStep(rock);
                rock.EulerStep(planet);
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

        public void Accelerate(Body externalBody) 
        {
            // accept a Body within this Method, externalBody

            // F= M/A -> A = F/M
            // - g*M1*M2/(r12)^2 
            // a = V/dt = f/m -> v = f*dt/m

            const float GravityConst = 1 ;

            Vector3 r21 =  r - externalBody.r;
            Vector3 r21Hat = Vector3.Normalize(r21);
            float r21LengthSquared = r21.LengthSquared();

            v += (dt*(-1f*GravityConst*externalBody.m)/ r21LengthSquared) * r21Hat; 
        }

        public void EulerStep(Body externalBody)
        {
            Accelerate(externalBody); // update velocity by applying acceleration 
            r += v * dt; // update position vector by multiplying new velocity by dt 
        }
    }
}
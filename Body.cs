using System.Numerics;

namespace NBodySimulation
{
    class Body
    {
        public float m; // mass 
        public float dt; //time step 
        public Vector3 r; // position vector 
        public Vector3 v; // velocity vector 
        public int id; // id for the body  
        public float kE; 

        public Body(Vector3 r, Vector3 v, float m, float dt, int id)
        {
            this.m = m;
            this.r = r;
            this.v = v;
            this.dt = dt;
            this.id = id;
        }

        public void Accelerate(List<Body> Bodies)
        {
            // accept a list of Body within this Method, get the net acceleration caused by all bodies in the simulation 

            // F= M/A -> A = F/M
            // - g*M1*M2/(r12)^2 
            // a = V/dt = f/m -> v = f*dt/m

            const float GravityConst = 1; // to do: move this to simulation class 
            Vector3 netForce = new Vector3(0, 0, 0);

            foreach (Body b in Bodies)
            {
                if (id != b.id)
                {
                    // to do: add an object radius and collisions 

                    Vector3 r21 = r - b.r;
                    netForce += -GravityConst * b.m * m / r21.LengthSquared() * Vector3.Normalize(r21);
                }
            }
            v += dt * netForce / m;
        }

        public void EulerStep(List<Body> Bodies)
        {
            Accelerate(Bodies); // update velocity by applying acceleration 
            r += v * dt; // update position vector by multiplying new velocity by dt 
        }

        public void KineticEnergy()
        {
            kE = m * v.LengthSquared(); 
        }
    }
}
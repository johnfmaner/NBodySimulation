using System.Numerics;

namespace NBodySimulation
{
    class Body
    {
        public float mass;
        public float timeStep;
        public Vector3 positionVector;
        public Vector3 velocityVector;
        public int id;
        public float kineticEnergy;
        public float radius;
        public bool isCollided;
        public int collidedWithId;

        public Body(Vector3 positionVector, Vector3 velocityVector, float mass, float timeStep, int id, float radius)
        {
            this.mass = mass;
            this.positionVector = positionVector;
            this.velocityVector = velocityVector;
            this.timeStep = timeStep;
            this.id = id;
            this.radius = radius;
        }

        public void Accelerate(List<Body> bodies)
        {
            // accept a list of Body's within this method, get the net acceleration on a Body caused by all other Body's in the simulation 

            const float GravityConst = 1; // to do: move this to simulation class 
            Vector3 netForce = new Vector3(0, 0, 0);

            foreach (Body workingBody in bodies)
            {
                if (id != workingBody.id)
                {
                    Vector3 positionVectorFromBodyToWorkingBody = positionVector - workingBody.positionVector;
                    Vector3 unitVectorFromBodyToWorkingBody = Vector3.Normalize(positionVectorFromBodyToWorkingBody);
                    float distanceSquaredFromBodyToWorkingBody = positionVectorFromBodyToWorkingBody.LengthSquared();

                    netForce += -(GravityConst * workingBody.mass * mass / distanceSquaredFromBodyToWorkingBody) * unitVectorFromBodyToWorkingBody;
                }
            }
            Vector3 velocityIncrement = timeStep * netForce / mass;

            velocityVector += velocityIncrement;
        }

        public void EulerStep(List<Body> bodies)
        {
            Accelerate(bodies); // update velocity 
            Vector3 positionIncrement = velocityVector * timeStep; // find new position increment with new velocity 
            positionVector += positionIncrement; // update position 
            CollisionCheck(bodies); // check if bodies are colliding 
        }

        public void CollisionCheck(List<Body> bodies)
        {
            foreach (Body workingBody in bodies)
            {
                Vector3 positionVectorFromBodyToWorkingBody = positionVector - workingBody.positionVector;
                float distanceFromBodyToWorkingBody = positionVectorFromBodyToWorkingBody.Length();

                if (distanceFromBodyToWorkingBody <= radius && id != workingBody.id && !workingBody.isCollided)
                {
                    collidedWithId = workingBody.id;
                    isCollided = true;
                }
            }
        }

        public void PerfectInelasticCollision(Body collidingBody)
        {
            // m1 * v1 + m2 * v2 = (m1 + m2) * v' 
            Vector3 momentum = mass * velocityVector;
            Vector3 collidingBodyMomentum = collidingBody.mass * collidingBody.velocityVector;
            float netMass = mass + collidingBody.mass;
            Vector3 newVelocityVector = (momentum + collidingBodyMomentum) / netMass;

            mass = netMass; 
            velocityVector = newVelocityVector; 
        }

        public void KineticEnergy()
        {
            float velocitySquared = velocityVector.LengthSquared();
            kineticEnergy = 0.5f * mass * velocitySquared; 
        }
    }
}
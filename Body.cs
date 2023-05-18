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

        public Body(Vector3 positionVector, Vector3 velocityVector, float mass, float timeStep, int id)
        {
            this.mass = mass;
            this.positionVector = positionVector;
            this.velocityVector = velocityVector;
            this.timeStep = timeStep;
            this.id = id;
        }

        public void Accelerate(List<Body> Bodies)
        {
            // accept a list of Body's within this method, get the net acceleration on a Body caused by all other Body's in the simulation 

            const float GravityConst = 1; // to do: move this to simulation class 
            Vector3 netForce = new Vector3(0, 0, 0);

            foreach (Body workingBody in Bodies)
            {
                if (id != workingBody.id)
                {
                    // to do: add an object radius and collisions 

                    Vector3 positionVectorFromBodyToWorkingBody = positionVector - workingBody.positionVector;
                    Vector3 unitVectorFromBodyToWorkingBody = Vector3.Normalize(positionVectorFromBodyToWorkingBody);
                    float distanceSquaredFromBodyToWorkingBody = positionVectorFromBodyToWorkingBody.LengthSquared(); 

                    netForce += -(GravityConst * workingBody.mass * mass / distanceSquaredFromBodyToWorkingBody) * unitVectorFromBodyToWorkingBody;
                }
            }
            Vector3 velocityIncrement = timeStep * netForce / mass; 

            velocityVector += velocityIncrement;
        }

        public void EulerStep(List<Body> Bodies)
        {
            Accelerate(Bodies);
            Vector3 positionIncrement = velocityVector * timeStep; 
            positionVector += positionIncrement; 
        }


        public void KineticEnergy()
        {
            float velocitySquared = velocityVector.LengthSquared();

            kineticEnergy = 0.5f * mass * velocitySquared; 
        }
    }
}
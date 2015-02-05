#define EXPORT_API __declspec(dllexport) // Visual Studio needs annotating exported functions with this
#include <cmath>
#include "rigid_body.h"

extern "C"
{
const EXPORT_API void createCube(Rigid_Body*& cube,float* cube_states)
{
    float* x=&cube_states[0];
    float* q=&cube_states[3];
    vec3 position(x[0],x[1],x[2]);
    vec3 velocity(-10.,0.,1.);
    //vec3 velocity(0.,0.,0.);
    Quaternion rotation(q[0],q[1],q[2],q[3]);
    vec3 angular_velocity(0.,0.,0.);
    cube=new Rigid_Body(position,velocity,rotation,angular_velocity);
    cube->Set_Cube_Inertia(1,1);
}

const EXPORT_API void destroyCube(Rigid_Body*& cube)
{
    delete cube;
}

const EXPORT_API void advanceCube(Rigid_Body*& cube,float* cube_states,const float dt)
{
    cube->Apply_Gravity(dt);
    cube->Advance(dt);
    cube->Collide_Cube_Ground(1.);

    cube_states[0]=cube->position[0];
    cube_states[1]=cube->position[1];
    cube_states[2]=cube->position[2];
    cube_states[3]=cube->rotation.x();
    cube_states[4]=cube->rotation.y();
    cube_states[5]=cube->rotation.z();
    cube_states[6]=cube->rotation.w();
}
} // end of export C block

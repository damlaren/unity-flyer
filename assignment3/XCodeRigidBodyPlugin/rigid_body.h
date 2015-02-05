#ifndef RIGID_BODY_H
#define RIGID_BODY_H

#include "quaternion.h"

using namespace matvec;

class Rigid_Body
{
public:
    float mass;
    vec3 position;
    vec3 velocity;
    mat33 inertia;
    mat33 inertia_inverse;
    Quaternion rotation;
    vec3 angular_velocity;
    vec3 force_accumulator;
    float coefficient_of_restitution;

    Rigid_Body(vec3 _position, vec3 _velocity,Quaternion _rotation,vec3 _angular_velocity,float _coefficient_of_restitution=0.5);
    void Set_Cube_Inertia(float _mass,float _side);
    void Apply_Impulse(vec3 impulse, vec3 location);
    void Apply_Gravity(float dt);
    void Advance(float dt);
    void Collide_Cube_Ground(float l);
    mat33 World_Space_Inertia_Tensor_Inverse(const Quaternion& orientation);
    mat33 Cross_Product_Matrix(const vec3& v);
    mat33 Impulse_Factor(const vec3& location);
};

#endif //RIGID_BODY_H

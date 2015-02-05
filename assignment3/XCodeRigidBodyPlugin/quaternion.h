#ifndef QUATERNION_H
#define QUATERNION_H

#include <iostream>
#include "matvec.h"
using namespace matvec;
class Quaternion {
  double mData[4];
  
 public:
  
    Quaternion() {
      mData[0] = mData[1] = mData[2] = 0;
      mData[3] = 1;
    }

    Quaternion(const vec3 v, double w) {
      mData[0] = v[0];
      mData[1] = v[1];
      mData[2] = v[2];
      mData[3] = w;
    }
	 
	Quaternion(double angle, vec3 axis) {
	   mData[0]=sin(angle/2.)*axis[0];
	   mData[1]=sin(angle/2.)*axis[1];
	   mData[2]=sin(angle/2.)*axis[2];
       mData[3]=cos(angle/2.);
    }

    Quaternion(const vec4& v) {
      mData[0] = v[0];
      mData[1] = v[1];
      mData[2] = v[2];
      mData[3] = v[3];
    }

    Quaternion(double x, double y, double z, double w) {
      mData[0] = x;
      mData[1] = y;
      mData[2] = z;
      mData[3] = w;
    }

    double x() const { return mData[0]; }
    double y() const { return mData[1]; }
    double z() const { return mData[2]; }
    double w() const { return real(); }

    vec3 complex() const { return vec3(mData); }


    double real() const { return mData[3]; }
    void real(double r) { mData[3] = r; }

    Quaternion conjugate(void) const {
      return Quaternion(-complex(), real());
    }

    /** 
     * @brief Computes the inverse of this quaternion.
     *
     * @note This is a general inverse.  If you know a priori
     * that you're using a unit quaternion (i.e., norm() == 1),
     * it will be significantly faster to use conjugate() instead.
     * 
     * @return The quaternion q such that q * (*this) == (*this) * q
     * == [ 0 0 0 1 ]<sup>T</sup>.
     */
    Quaternion inverse(void) const {
      return conjugate() / norm();
    }


    /** 
     * @brief Computes the product of this quaternion with the
     * quaternion 'rhs'.
     *
     * @param rhs The right-hand-side of the product operation.
     *
     * @return The quaternion product (*this) x @p rhs.
     */
    Quaternion product(const Quaternion& rhs) const {
      return Quaternion(y()*rhs.z() - z()*rhs.y() + x()*rhs.w() + w()*rhs.x(),
                        z()*rhs.x() - x()*rhs.z() + y()*rhs.w() + w()*rhs.y(),
                        x()*rhs.y() - y()*rhs.x() + z()*rhs.w() + w()*rhs.z(),
                        w()*rhs.w() - x()*rhs.x() - y()*rhs.y() - z()*rhs.z());
    }

    /**
     * @brief Quaternion product operator.
     *
     * The result is a quaternion such that:
     *
     * result.real() = (*this).real() * rhs.real() -
     * (*this).complex().dot(rhs.complex());
     *
     * and:
     *
     * result.complex() = rhs.complex() * (*this).real
     * + (*this).complex() * rhs.real()
     * - (*this).complex().cross(rhs.complex());
     *
     * @return The quaternion product (*this) x rhs.
     */
    Quaternion operator*(const Quaternion& rhs) const {
      return product(rhs);
    }

    /**
     * @brief Quaternion scalar product operator.
     * @param s A scalar by which to multiply all components
     * of this quaternion.
     * @return The quaternion (*this) * s.
     */
    Quaternion operator*(double s) const {
      return Quaternion(complex()*s, real()*s);
    }

    /**
     * @brief Produces the sum of this quaternion and rhs.
     */
    Quaternion operator+(const Quaternion& rhs) const {
      return Quaternion(x()+rhs.x(), y()+rhs.y(), z()+rhs.z(), w()+rhs.w());
    }

    /**
     * @brief Produces the difference of this quaternion and rhs.
     */
    Quaternion operator-(const Quaternion& rhs) const {
      return Quaternion(x()-rhs.x(), y()-rhs.y(), z()-rhs.z(), w()-rhs.w());
    }

    /**
     * @brief Unary negation.
     */
    Quaternion operator-() const {
      return Quaternion(-x(), -y(), -z(), -w());
    }

    /**
     * @brief Quaternion scalar division operator.
     * @param s A scalar by which to divide all components
     * of this quaternion.
     * @return The quaternion (*this) / s.
     */
    Quaternion operator/(double s) const {
      if (s == 0) std::clog << "Dividing quaternion by 0." << std::endl;
      return Quaternion(complex()/s, real()/s);
    }

    /**
     * @brief Returns a matrix representation of this
     * quaternion.
     *
     * Specifically this is the matrix such that:
     *
     * this->matrix() * q.vector() = (*this) * q for any quaternion q.
     *
     * Note that this is @e NOT the rotation matrix that may be
     * represented by a unit quaternion.
     */
    mat44 matrix() const {
      return mat44(w(), -z(),  y(), x(),
         z(),  w(), -x(), y(),
        -y(),  x(),  w(), z(),
        -x(), -y(), -z(), w());
    }

    /**
     * @brief Returns a matrix representation of this
     * quaternion for right multiplication.
     *
     * Specifically this is the matrix such that:
     *
     * q.vector().transpose() * this->matrix() = (q *
     * (*this)).vector().transpose() for any quaternion q.
     *
     * Note that this is @e NOT the rotation matrix that may be
     * represented by a unit quaternion.
     */
    /**
     * @brief Returns the norm ("magnitude") of the quaternion.
     * @return The 2-norm of [ w(), x(), y(), z() ]<sup>T</sup>.
     */
    double norm() const { return sqrt(mData[0]*mData[0]+mData[1]*mData[1]+
                                      mData[2]*mData[2]+mData[3]*mData[3]); }

    /**
     * @brief Computes the rotation matrix represented by a unit
     * quaternion.
     *
     * @note This does not check that this quaternion is normalized.
     * It formulaically returns the matrix, which will not be a
     * rotation if the quaternion is non-unit.
     */
    mat33 rotationMatrix() const {
      return mat33(
        1-2*y()*y()-2*z()*z(), 2*x()*y() - 2*z()*w(), 2*x()*z() + 2*y()*w(),
        2*x()*y() + 2*z()*w(), 1-2*x()*x()-2*z()*z(), 2*y()*z() - 2*x()*w(),
        2*x()*z() - 2*y()*w(), 2*y()*z() + 2*x()*w(), 1-2*x()*x()-2*y()*y()
      );
    }
    /**
     * @brief Returns a vector rotated by this quaternion.
     *
     * Functionally equivalent to:  (rotationMatrix() * v)
     * or (q * Quaternion(0, v) * q.inverse()).
     *
     * @warning conjugate() is used instead of inverse() for better
     * performance, when this quaternion must be normalized.
     */
    vec3 rotatedVector(const vec3& v) const {
      return (((*this) * Quaternion(v, 0)) * conjugate()).complex();
    }
};
#endif /* QUATERNION_H */

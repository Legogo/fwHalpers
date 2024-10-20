﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fwp.halpers
{

    static public class HalperMath
    {
        static public Vector2 solvePointProjection(Vector2 p, Vector2 a, Vector2 b)
        {
            Vector2 ap = p - a;
            Vector2 ab = b - a;
            return a + Vector2.Dot(ap, ab) / Vector2.Dot(ab, ab) * ab;
        }

        /// <summary>
        /// clamped to [a,b] ?
        /// </summary>
        static public Vector3 solvePointProjectionSegment(Vector3 p, Vector3 a, Vector3 b)
        {
            Vector3 ab = b - a;
            float absq = Vector3.Dot(ab, ab);

            if (absq == 0) return a;

            Vector3 ap = p - a;
            float t = Vector3.Dot(ap, ab) / absq;
            if (t < 0f) return a;
            else if (t > 1f) return b;

            return a + (t * ab);
        }

        //https://forum.unity.com/threads/line-intersection.17384/
        public static bool LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 intersection)
        {
            float Ax, Bx, Cx, Ay, By, Cy, d, e, f, num, offset;
            float x1lo, x1hi, y1lo, y1hi;

            Ax = p2.x - p1.x;
            Bx = p3.x - p4.x;

            // X bound box test/
            if (Ax < 0)
            {
                x1lo = p2.x; x1hi = p1.x;
            }
            else
            {
                x1hi = p2.x; x1lo = p1.x;
            }

            if (Bx > 0)
            {
                if (x1hi < p4.x || p3.x < x1lo) return false;
            }
            else
            {
                if (x1hi < p3.x || p4.x < x1lo) return false;
            }

            Ay = p2.y - p1.y;
            By = p3.y - p4.y;

            // Y bound box test//
            if (Ay < 0)
            {
                y1lo = p2.y; y1hi = p1.y;
            }
            else
            {
                y1hi = p2.y; y1lo = p1.y;
            }

            if (By > 0)
            {
                if (y1hi < p4.y || p3.y < y1lo) return false;
            }
            else
            {
                if (y1hi < p3.y || p4.y < y1lo) return false;
            }

            Cx = p1.x - p3.x;
            Cy = p1.y - p3.y;
            d = By * Cx - Bx * Cy;  // alpha numerator//
            f = Ay * Bx - Ax * By;  // both denominator//

            // alpha tests//
            if (f > 0)
            {
                if (d < 0 || d > f) return false;
            }
            else
            {
                if (d > 0 || d < f) return false;
            }

            e = Ax * Cy - Ay * Cx;  // beta numerator//

            // beta tests //
            if (f > 0)
            {
                if (e < 0 || e > f) return false;
            }
            else
            {
                if (e > 0 || e < f) return false;
            }

            // check if they are parallel
            if (f == 0) return false;

            // compute intersection coordinates //
            num = d * Ax; // numerator //
            offset = same_sign(num, f) ? f * 0.5f : -f * 0.5f;   // round direction //
            intersection.x = p1.x + (num + offset) / f;

            num = d * Ay;
            offset = same_sign(num, f) ? f * 0.5f : -f * 0.5f;
            intersection.y = p1.y + (num + offset) / f;

            return true;
        }

        private static bool same_sign(float a, float b)
        {
            return ((a * b) >= 0f);
        }


        public static bool RayLineSegmentIntersection(Vector2 rayStart, Vector2 rayDir, Vector2 linePointA, Vector2 linePointB, ref Vector2 intersection)
        {
            intersection = GetIntersectionPointCoordinates(rayStart, rayStart + rayDir * Mathf.Infinity, linePointA, linePointB);
            if (intersection == Vector2.zero) return false;
            float t = (intersection - linePointA).magnitude / (linePointB - linePointA).magnitude;
            return t >= 0 && t <= 1;
        }

        public static bool LineLineIntersection(out Vector3 intersection, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
        {
            Vector3 lineVec3 = linePoint2 - linePoint1;
            Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
            Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);

            float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

            //is coplanar, and not parrallel
            if (Mathf.Abs(planarFactor) < 0.0001f && crossVec1and2.sqrMagnitude > 0.0001f)
            {
                float s = Vector3.Dot(crossVec3and2, crossVec1and2) / crossVec1and2.sqrMagnitude;
                intersection = linePoint1 + (lineVec1 * s);
                return true;
            }
            else
            {
                intersection = Vector3.zero;
                return false;
            }
        }

        /// <summary>
        /// https://blog.dakwamine.fr/?p=1943
        /// Gets the coordinates of the intersection point of two lines.
        /// </summary>
        /// <param name="A1">A point on the first line.</param>
        /// <param name="A2">Another point on the first line.</param>
        /// <param name="B1">A point on the second line.</param>
        /// <param name="B2">Another point on the second line.</param>
        /// <param name="found">Is set to false of there are no solution. true otherwise.</param>
        /// <returns>The intersection point coordinates. Returns Vector2.zero if there is no solution.</returns>
        static public Vector2 GetIntersectionPointCoordinates(Vector2 A1, Vector2 A2, Vector2 B1, Vector2 B2)
        {
            float tmp = (B2.x - B1.x) * (A2.y - A1.y) - (B2.y - B1.y) * (A2.x - A1.x);

            if (tmp == 0)
            {
                // No solution!
                //found = false;
                return Vector2.zero;
            }

            float mu = ((A1.x - B1.x) * (A2.y - A1.y) - (A1.y - B1.y) * (A2.x - A1.x)) / tmp;

            //found = true;

            return new Vector2(
                B1.x + (B2.x - B1.x) * mu,
                B1.y + (B2.y - B1.y) * mu
            );
        }


        static public bool LineSegmentsIntersection(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2, ref Vector2 intersection)
        {
            intersection = GetIntersectionPointCoordinates(a1, a2, b1, b2);
            if (intersection == Vector2.zero) return false;

            // check if point lies on segments
            Vector2 a1ta2 = a2 - a1;
            Vector2 b1tb2 = b2 - b1;
            float ta = Vector2.Dot(intersection - a1, a1ta2) / a1ta2.sqrMagnitude;
            float tb = Vector2.Dot(intersection - b1, b1tb2) / b1tb2.sqrMagnitude;
            return ta >= 0 && ta <= 1 && tb >= 0 && tb <= 1;
        }


        static public Vector3 compareMinMax(Vector3 a, Vector3 b, bool checkMin = false)
        {

            if (checkMin)
            {
                a.x = Mathf.Min(a.x, b.x);
                a.y = Mathf.Min(a.y, b.y);
                a.z = Mathf.Min(a.z, b.z);
            }
            else
            {
                a.x = Mathf.Max(a.x, b.x);
                a.y = Mathf.Max(a.y, b.y);
                a.z = Mathf.Max(a.z, b.z);
            }
            return a;
        }

        static public bool cmpVec2(this Vector2 value, Vector2 b, float epsilon = 0f)
        {
            if (epsilon == 0f) epsilon = Mathf.Epsilon;
            if (b.x < value.x - epsilon || b.x > value.x + epsilon) return false;
            if (b.y < value.y - epsilon || b.y > value.y + epsilon) return false;
            return true;
        }
        static public bool cmpVec3(this Vector3 value, Vector3 b, float epsilon = 0f)
        {
            if (epsilon == 0f) epsilon = Mathf.Epsilon;
            if (b.x < value.x - epsilon || b.x > value.x + epsilon) return false;
            if (b.y < value.y - epsilon || b.y > value.y + epsilon) return false;
            if (b.z < value.z - epsilon || b.z > value.z + epsilon) return false;
            return true;
        }

        static public void logVec2(this Vector2 value) => Debug.Log(value.x + "x" + value.y);
        static public void logVec3(this Vector3 value) => Debug.Log(value.x + "x" + value.y + "x" + value.z);
    }

}
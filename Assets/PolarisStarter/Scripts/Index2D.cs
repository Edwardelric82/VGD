using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pinwheel.PolarisStarter
{
    public struct Index2D
    {
        private int x;
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        private int z;
        public int Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }

        public Index2D(int x, int z)
        {
            this.x = x;
            this.z = z;
        }
    }
}
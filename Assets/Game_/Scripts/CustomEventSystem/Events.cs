using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public static class Events
    {
        public static readonly Event<IStackable> OnObstacleCollision = new Event<IStackable>();
    }

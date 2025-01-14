﻿using System;
using System.Collections;
using UnityEngine;

namespace Item.Ammo
{
    public class BoxingAmmo : AmmoBase
    {
        public ObjectPool effectPool;                   // 特效池
        [HideInInspector]
        public bool needTurnBack;                       // 请求弹簧拳返回（碰到物体了）

        private HealthManager targetHealth;             // 目标血量

        /// <summary>
        /// 弹簧拳是金刚不坏拳，不会坏的
        /// </summary>
        protected void Start()
        {
            IsIndestructible = true;
        }

        protected override void OnCollision(Collider other)
        {
            needTurnBack = true;
            effectPool.GetNextObject(transform);
            targetHealth = other.GetComponentInParent<HealthManager>();
            if (targetHealth != null)
                targetHealth.SetHealthAmount(-damage, launcher);
        }

    }
}
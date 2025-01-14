﻿using UnityEngine;

namespace CameraRig
{
    public class IntervalOffsetCameraRig : MonoBehaviour
    {
        public new Camera camera;           // 相机
        public Vector3 startPosition;       // 起始位置
        public Vector3 offset;              // 每次偏移量
        public int index;                   // 当前索引
        public float smoothTime = 0.3f;     // 平滑时间

        private Vector3 velocity;           // 当前平滑移动速度

        /// <summary>
        /// 如果开启平滑移动，自动移动
        /// </summary>
        private void Update()
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, startPosition + (index * offset), ref velocity, smoothTime);
        }

        /// <summary>
        /// 跟随目标索引
        /// </summary>
        /// <param name="index">索引值</param>
        public void SetTarget(int index)
        {
            this.index = index;
        }

        /// <summary>
        /// 立即移动目标到指定位置
        /// </summary>
        /// <param name="index">索引值</param>
        public void SetTargetImmediately(int index)
        {
            this.index = index;
            transform.localPosition = startPosition + (index * offset);
        }

        /// <summary>
        /// 设置目标为当前坦克索引
        /// </summary>
        public void SetTargetByCurrentTankIndex()
        {
            index = AllCustomTankManager.Instance.CurrentIndex;
        }
    }
}

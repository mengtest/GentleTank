﻿using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;
using UnityEngine.Events;
using System.Collections;

public class SprialTrackCameraRig : MonoBehaviour 
{
    static public SprialTrackCameraRig Instance { get; private set; }

    public PlayableDirector director;
    public CinemachineVirtualCamera virtualCamera;
    public Transform cameraTrackTransfrom;
    public bool playeOnEnable = true;
    public UnityEvent endOfAnimationEvent;

    public Transform Target { get { return virtualCamera.LookAt; } set { virtualCamera.LookAt = value; } }

    private bool isPlaying = false;

    /// <summary>
    /// 初始单例
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 如果选择了激活时启动相机动画，那就启动啊
    /// </summary>
    private void OnEnable()
    {
        if (playeOnEnable)
            Play();
    }

    /// <summary>
    /// 设置相机目标
    /// </summary>
    /// <param name="target">相机目标</param>
    public void SetTarget(Transform target)
    {
        Target = target;
    }

    /// <summary>
    /// 更新相机位置
    /// </summary>
    public void Play()
    {
        if (Target == null || isPlaying)
            return;
        director.Play();
        isPlaying = true;
        StartCoroutine(UpdateTrackPosition());
    }

    /// <summary>
    /// 更新轨道位置，以及结束后调用响应事件
    /// </summary>
    private IEnumerator UpdateTrackPosition()
    {
        while (director.state != PlayState.Paused)
        {
            GameMathf.CopyPositionAndRotation(virtualCamera.LookAt, transform);
            yield return null;
        }
        isPlaying = false;
        endOfAnimationEvent.Invoke();
        yield return null;
    }
}

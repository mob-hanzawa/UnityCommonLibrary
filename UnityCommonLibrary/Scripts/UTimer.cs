﻿using System;
using UnityCommonLibrary.Utilities;
using UnityEngine;

namespace UnityCommonLibrary.Time {
    public abstract class UTimeTool {
        public State state { get; protected set; }
        public TimeMode mode { get; protected set; }
        public float startTime { get; protected set; }
        public float totalPauseTime { get; protected set; }
        public float lastPauseTime { get; protected set; }
        public float value { get; protected set; }
        public TimeSpan span {
            get {
                return TimeSpan.FromSeconds(value);
            }
        }

        public UTimeTool() {
            Reset();
        }

        #region Controls
        public virtual void Start() {
            if(state == State.Stopped) {
                startTime = TimeUtility.GetCurrentTime(mode);
                state = State.Running;
            }
        }

        public virtual void Pause() {
            if(state == State.Running) {
                state = State.Paused;
                lastPauseTime = TimeUtility.GetCurrentTime(mode);
            }
        }

        public virtual void Resume() {
            if(state == State.Paused) {
                state = State.Running;
                totalPauseTime += TimeUtility.GetCurrentTime(mode) - lastPauseTime;
            }
        }

        public virtual void Stop() {
            if(state != State.Stopped) {
                state = State.Stopped;
            }
        }

        public virtual void Reset() {
            state = State.Stopped;
            startTime = 0f;
            totalPauseTime = 0f;
        }

        public void Restart() {
            Reset();
            Start();
        }
        #endregion

        public bool Tick() {
            if(state == State.Running) {
                return InternalTick();
            }
            return false;
        }

        protected abstract bool InternalTick();

        public override string ToString() {
            return span.ToString();
        }
    }

    public sealed class UStopwatch : UTimeTool {
        protected override bool InternalTick() {
            value = TimeUtility.GetCurrentTime(mode) - startTime;
            value -= totalPauseTime;
            return false;
        }
    }

    public sealed class UTimer : UTimeTool {
        public float duration { get; set; }

        #region Controls
        public override void Reset() {
            base.Reset();
            value = duration;
        }
        #endregion

        protected override bool InternalTick() {
            value = duration - (TimeUtility.GetCurrentTime(mode) - startTime);
            value += totalPauseTime;
            return value <= 0f;
        }
    }

    public enum State {
        Stopped,
        Running,
        Paused
    }

    public enum TimeMode {
        Time,
        UnscaledTime,
        RealtimeSinceStartup,
        FixedTime
    }
}
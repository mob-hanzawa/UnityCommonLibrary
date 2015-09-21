﻿namespace UnityCommonLibrary {
    [System.Serializable]
    public abstract class AbstractSaveData {
        public SaveBool writeable = new SaveBool(true);
        public SaveInt slot = new SaveInt(byte.MaxValue);
    }
}
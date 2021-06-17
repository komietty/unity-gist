using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace kmty.editor {

    [Serializable]
    public class ToggleCtrls<T> where T : UnityEngine.Object {
        [SerializeField] protected List<ToggleCtrl<T>> objs;
        public List<T> Unwrap() => objs.Where(o => o.Flag).Select(o => o.Obj).ToList();
    }

    [Serializable]
    public class ToggleCtrl<T> where T : UnityEngine.Object {
        [SerializeField] protected T obj;
        [SerializeField] protected bool flag;
        public T Obj { get => obj; set { obj = value; } }
        public bool Flag { get => flag; set { flag = value; } }
    }
}

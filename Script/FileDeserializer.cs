using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace kmty.gist {
    public static class JsonUtil {
        public static string LoadData(string filename, string dir = default) {
            if (dir == default) dir = Application.streamingAssetsPath;
            var path = Path.Combine(dir, filename);
            var text = "";
            if (File.Exists(path)) text = File.ReadAllText(path);
            else Debug.LogWarning(path + " is not found");
            return text;
        }
    }

    public static class CsvUtil {
        public static string[][] LoadData(string filename) {
            var f = (TextAsset)Resources.Load(filename);
            var r = new StringReader(f.text);
            var d = new List<string[]>();
            while (r.Peek() != -1) { d.Add(r.ReadLine().Split(',')); }
            return d.ToArray();

        }
    }
}

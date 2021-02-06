using UnityEngine;
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
}

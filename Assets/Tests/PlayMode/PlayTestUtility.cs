#if UNITY_EDITOR && UNITY_INCLUDE_TESTS
namespace UtilityManager.Test {
    using UnityEngine;
    using UnityEngine.Experimental.Rendering.Universal;
    using UnityEngine.Rendering.Universal;

    public class PlayTestUtility
    {
        public static Camera CreateCamera()
        {
            var obj = new GameObject();
            obj.name = "Camera@Test";
            obj.transform.position = Vector3.back * 10f;
            var camera = obj.AddComponent<Camera>();
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = Color.cyan;
            camera.orthographic = true;
            camera.orthographicSize = 5f;
            camera.tag = "MainCamera";
            return camera;
        }

        public static void DestroyCamera(Camera camera)
        {
            Object.DestroyImmediate(camera.gameObject);
        }


        public static Light2D CreateLight()
        {
            var obj = new GameObject();
            obj.name = "Light@Global";
            obj.transform.position = Vector3.zero;
            var light = obj.AddComponent<Light2D>();
            light.lightType = Light2D.LightType.Global;
            return light;
        }

        public static void DestroyLight(Light2D light)
        {
            Object.DestroyImmediate(light.gameObject);
        }

    }
}
#endif
using UnityEditor;
using UnityEngine;

public class AndroidBuildScript : EditorWindow
{
    private static int BUILD_NUMBER;
    private static string VERSION_NAME;
    private static string PRODUCT_NAME;

    [MenuItem("Build/Release/Android/Build APK")]
    public static void BuildAPK()
    {
        LoadVersionInfo();

        // ビルド番号をインクリメント
        BUILD_NUMBER++;
        SaveVersionInfo();

        // バージョン文字列を更新
        UpdateVersionString();


        // Android ビルド
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/Main.unity" }; // ビルドするシーン
        buildPlayerOptions.locationPathName = $"Builds/Release/Android/{PRODUCT_NAME}_{VERSION_NAME}_{BUILD_NUMBER}.apk";
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;

        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }

    private static void LoadVersionInfo()
    {
        VERSION_NAME = PlayerSettings.bundleVersion;
        BUILD_NUMBER = PlayerSettings.Android.bundleVersionCode;
        PRODUCT_NAME = Application.productName;
    }

    private static void SaveVersionInfo()
    {
        PlayerSettings.Android.bundleVersionCode = BUILD_NUMBER;
    }

    private static void UpdateVersionString()
    {
        // versionNameを更新する必要がある場合。
        // 例：Patchを上げる
        // string[] versionParts = VERSION_NAME.Split('.');
        // if (versionParts.Length == 3)
        // {
        //     int patch = int.Parse(versionParts[2]);
        //     patch++;
        //     VERSION_NAME = $"{versionParts[0]}.{versionParts[1]}.{patch}";
        //     PlayerSettings.bundleVersion = VERSION_NAME;
        // }

    }

    [MenuItem("Build/Release/Android/Increment Patch Version")]
    public static void IncrementPatchVersion()
    {
        LoadVersionInfo();
        // バージョン番号を更新 (例: パッチ番号をインクリメント)
        string[] versionParts = VERSION_NAME.Split('.');
        if (versionParts.Length == 3)
        {
            int patch = int.Parse(versionParts[2]);
            patch++;
            VERSION_NAME = $"{versionParts[0]}.{versionParts[1]}.{patch}";
            PlayerSettings.bundleVersion = VERSION_NAME;
            Debug.Log("Version(Patch) Incremented: " + VERSION_NAME);
        }
        else
        {
            Debug.LogError("Invalid version format.  Please use MAJOR.MINOR.PATCH");
        }
    }

    [MenuItem("Build/Release/Android/Increment Minor Version")]
    public static void IncrementMinorVersion()
    {
        LoadVersionInfo();
        // バージョン番号を更新 (例: マイナー番号をインクリメント)
        string[] versionParts = VERSION_NAME.Split('.');
        if (versionParts.Length == 3)
        {
            int minor = int.Parse(versionParts[1]);
            minor++;
            VERSION_NAME = $"{versionParts[0]}.{minor}.0";
            PlayerSettings.bundleVersion = VERSION_NAME;
            Debug.Log("Version(Minor) Incremented: " + VERSION_NAME);
        }
        else
        {
            Debug.LogError("Invalid version format.  Please use MAJOR.MINOR.PATCH");
        }
    }

    [MenuItem("Build/Release/Android/Increment Major Version")]
    public static void IncrementMajorVersion()
    {
        LoadVersionInfo();

        // バージョン番号を更新 (例: メジャー番号をインクリメント)
        string[] versionParts = VERSION_NAME.Split('.');
        if (versionParts.Length == 3)
        {
            int major = int.Parse(versionParts[0]);
            major++;
            VERSION_NAME = $"{major}.0.0";
            PlayerSettings.bundleVersion = VERSION_NAME;
            Debug.Log("Version(Major) Incremented: " + VERSION_NAME);
        }
        else
        {
            Debug.LogError("Invalid version format.  Please use MAJOR.MINOR.PATCH");
        }
    }
}
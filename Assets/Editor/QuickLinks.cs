using UnityEditor;
using UnityEngine;

public static class QuickLinks
{
    [MenuItem("@Links/Readme|Roadmap")]
    private static void OpenRepoReadme()
    {
        OpenURL("https://github.com/artrex-st/Unity-BluGra-Shop/blob/main/README.md");
    }

    [MenuItem("@Links/Git Repository")]
    private static void OpenRepo()
    {
        OpenURL("https://github.com/artrex-st/Unity-BluGra-Shop");
    }

    private static void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}

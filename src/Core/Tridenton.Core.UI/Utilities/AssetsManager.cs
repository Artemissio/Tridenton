namespace Tridenton.Core.UI.Utilities;

public readonly struct AssetsManager
{
    private const string AssetsDirectory = "_content/Tridenton.Core.UI/assets/";
    
    public static string GetAssetPath(string assetName)
    {
        return Path.Combine(AssetsDirectory, assetName);
    }
}
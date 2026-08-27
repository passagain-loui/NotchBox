namespace NotchBox.Core
{
    public enum AppState
    {
        Idle,
        Expanded,
        HoldingItems,
        GhostPending,
        Downloading
    }

    public enum DataCategory
    {
        FileReference,
        TempFile,
        Url,
        ColorHex,
        TextSnippet
    }
}

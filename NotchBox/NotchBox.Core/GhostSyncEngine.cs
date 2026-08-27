using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace NotchBox.Core
{
    public class GhostMetadata
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Sender { get; set; } = Environment.MachineName;
        public string FileName { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        public string PayloadPath { get; set; } = string.Empty;
        public string Status { get; set; } = "available";
    }

    public class GhostSyncEngine
    {
        private FileSystemWatcher? _watcher;
        public event Action<GhostMetadata>? OnGhostItemReceived;
        public event Action<string>? OnGhostItemRemoved;

        public void InitializeWatcher(string sharedFolderPath)
        {
            if (!Directory.Exists(sharedFolderPath))
            {
                Directory.CreateDirectory(sharedFolderPath);
            }

            _watcher = new FileSystemWatcher(sharedFolderPath, "*.json")
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
            };

            _watcher.Created += OnFileCreated;
            _watcher.Deleted += OnFileDeleted;
            _watcher.EnableRaisingEvents = true;
        }

        private async void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                await Task.Delay(100);
                string jsonContent = await File.ReadAllTextAsync(e.FullPath);
                var metadata = JsonSerializer.Deserialize<GhostMetadata>(jsonContent);
                if (metadata != null && metadata.Sender != Environment.MachineName)
                {
                    OnGhostItemReceived?.Invoke(metadata);
                }
            }
            catch { }
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            OnGhostItemRemoved?.Invoke(e.Name ?? string.Empty);
        }

        public async Task HydratePayloadAsync(GhostMetadata metadata, string destinationPath)
        {
            if (File.Exists(metadata.PayloadPath))
            {
                await using var sourceStream = File.OpenRead(metadata.PayloadPath);
                await using var destinationStream = File.Create(destinationPath);
                await sourceStream.CopyToAsync(destinationStream);
            }
            else
            {
                throw new FileNotFoundException("Source payload is unavailable on shared network path.");
            }
        }
    }
}

using System;
using System.IO;
using System.Text.Json;

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
}

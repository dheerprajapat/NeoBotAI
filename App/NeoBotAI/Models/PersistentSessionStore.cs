using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NeoBotAI.Models;

public class PersistentSessionStore
{
    [JsonPropertyName("SavedSessions")]
    public Dictionary<string, UserSession> SavedSessions { get; set; } = new Dictionary<string, UserSession>();
}

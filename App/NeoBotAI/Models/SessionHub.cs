using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NeoBotAI.Models;

public class SessionHub
{
    private static SessionHub? instance;

    public static SessionHub Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SessionHub();
                instance.PersistentSessionStore = Load();
            }
            return instance;
        }
    }


    public PersistentSessionStore PersistentSessionStore { get; private set; } = new PersistentSessionStore();
    private SessionHub()
    {
    }


    private static readonly string FilePath = Path.Join(FileSystem.Current.AppDataDirectory, nameof(NeoBotAI), "sessions.json");


    public static PersistentSessionStore Load()
    {
        try
        {
            if (!File.Exists(FilePath))
            {
                return new PersistentSessionStore();
            }

            var json = File.ReadAllText(FilePath);
            var res =  JsonSerializer.Deserialize<PersistentSessionStore>(json);

            if(res == null)
                return new PersistentSessionStore();
            return res;
        }
        catch
        {
            return new PersistentSessionStore();
        }
    }

    public static void Save()
    {
        if (Instance == null)
            return;
        try
        {
            var dir = Path.GetDirectoryName(FilePath)!;
            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var json = JsonSerializer.Serialize(Instance.PersistentSessionStore);
            File.WriteAllText(FilePath, json);
        }
        catch(Exception ex) 
        {

        }
    }

    public void AddOrUpdateSession(UserSession session)
    {

        if (!PersistentSessionStore.SavedSessions.ContainsKey(session.Id))
        {
            PersistentSessionStore.SavedSessions.Add(session.Id,session);
        }
        else
        {
            PersistentSessionStore.SavedSessions[session.Id] = session;
        }


        Save();
    }

    public void ChangeSessionId(string previousId,UserSession newSession)
    {
        if(PersistentSessionStore.SavedSessions.ContainsKey(previousId))
        {
            PersistentSessionStore.SavedSessions.Remove(previousId);

            AddOrUpdateSession(newSession);
        }
            
    }

}

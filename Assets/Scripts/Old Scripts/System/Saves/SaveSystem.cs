using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    
    #region SavePlayer
    public static void SavePlayer(SystemUpdate variables)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.wolf";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerProgress data = new PlayerProgress(variables);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    #endregion
    #region LoadPlayer
    public static PlayerProgress LoadPlayer() {
        string path = Application.persistentDataPath + "/player.wolf";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerProgress variables = formatter.Deserialize(stream) as PlayerProgress;
            stream.Close();

            return variables;
        } else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    #endregion
}
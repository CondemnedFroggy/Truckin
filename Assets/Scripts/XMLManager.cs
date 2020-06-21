using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;


public class _GameSaveLoad
{ 
    // An example where the encoding can be found is at 
    // http://www.eggheadcafe.com/articles/system.xml.xmlserialization.asp 
    // We will just use the KISS method and cheat a little and use 
    // the examples from the web page since they are fully described 

    string _FileLocation = Application.dataPath;
    string _FileName = "leaderboard.xml";

    // When the EGO is instansiated the Start will trigger 
    // so we setup our initial values for our local members 
    void Start()
    {
    }

    void Update() { }

    /* The following metods came from the referenced URL */
    string UTF8ByteArrayToString(byte[] characters)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        string constructedString = encoding.GetString(characters);
        return (constructedString);
    }

    byte[] StringToUTF8ByteArray(string pXmlString)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] byteArray = encoding.GetBytes(pXmlString);
        return byteArray;
    }

    // Here we serialize our UserData object of myData 
    public string SerializeObject(object pObject)
    {
        string XmlizedString = null;
        MemoryStream memoryStream = new MemoryStream();
        XmlSerializer xs = new XmlSerializer(typeof(List<PlayerInfo>));
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        xs.Serialize(xmlTextWriter, pObject);
        memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
        XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
        return XmlizedString;
    }

    // Here we deserialize it back into its original form 
    public object DeserializeObject(string pXmlizedString)
    {
        XmlSerializer xs = new XmlSerializer(typeof(List<PlayerInfo>));
        MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        return xs.Deserialize(memoryStream);
    }

    public void CreateXML(string data)
    {
        StreamWriter writer;
        FileInfo t = new FileInfo(_FileLocation + "\\" + _FileName);
        if (!t.Exists)
        {
            writer = t.CreateText();
        }
        else
        {
            t.Delete();
            writer = t.CreateText();
        }
        writer.Write(data);
        writer.Close();
        Debug.Log("File written.");
    }

    public string LoadXML()
    {
        StreamReader r = File.OpenText(_FileLocation + "\\" + _FileName);
        string _info = r.ReadToEnd();
        r.Close();
        return _info;
        Debug.Log("File Read");
    }
}

// UserData is our custom class that holds our defined objects we want to store in XML format 

public struct PlayerInfo
{
    public string name;
    public float score;
    public float totalScore;
    public float timeRemaining;
    public int windowsSmashed;

    public PlayerInfo(string name, float score, float totalScore, float timeRemaining, int windowsSmashed)
    {
        this.name = name;
        this.score = score;
        this.totalScore = totalScore;
        this.timeRemaining = timeRemaining;
        this.windowsSmashed = windowsSmashed;
    }
}
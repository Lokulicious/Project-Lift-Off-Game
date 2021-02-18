using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


    class SaveHighscore : GameObject
    {

    int currentScore = 0;
    int highScore;

    public SaveHighscore(int scoreInt)
    {
        currentScore = scoreInt;
        Console.WriteLine("Constructor");
        /*        highScore = highscoreInt;*/
        SaveFile();
        LoadFile();
    }


    void Start()
    {

    }


    void Update()
    {
        string destination = @"C:\Users\userdir\mygames\Nudino\save.dat";
        Console.WriteLine(destination);

        Console.WriteLine("update");
    }



    public void SaveFile()
    {
        string destination = @"C:\Program Files\Nudino\save.dat";
        Directory.CreateDirectory(Path.GetDirectoryName(destination));

        Console.WriteLine(destination);
        FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
        }
        else
        {
            file = File.Create(destination);
        }

        GameData data = new GameData(currentScore, highScore);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }


    public void LoadFile()
    {
        string destination = @"C:\Program Files\Nudino\save.dat";
        FileStream file;


        if (File.Exists(destination))
        {
            file = File.OpenRead(destination);
        }
        else
        {
            Console.WriteLine("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        currentScore = data.score;
        highScore = data.highScore;


        Console.WriteLine("Score : " + currentScore);
        Console.WriteLine("HighScore : " + highScore);

    }

    }

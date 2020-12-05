using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Code
{
    /// <inheritdoc><cref></cref>
    /// </inheritdoc>
    /// <summary>
    /// Save/Load manager. Handles serialization/deserialization of all of the game's "stuff."
    /// </summary>
    /// 
    public class SaveLoadThis
    {
        public Ball _b;

        public int score;
        
        private const string PathExt = "/save";
        private readonly string _path;

        //public SaveData start_scene;

        public SaveLoadThis () {
            _path = Application.persistentDataPath + PathExt;
            
        }

        /// <summary>
        /// Manually serialize out all of the GameData class and its information
        /// </summary>
        public void Save () {
            Debug.Log("Saving file: " + _path);

            using (var file = File.Create(_path)) {
                var data = new SaveData();
                data.saved_score = score;
                var writer = new XmlSerializer(typeof(SaveData));
                writer.Serialize(file, data);
                file.Close();
                Debug.Log("Saved");

            }
        }

        public void Load () {
            Debug.Log("Loading file: " + _path);
            if (!File.Exists(_path)) { return; } // how can our load be real if our file isn't real
            using (var file = File.Open(_path, FileMode.Open)) {
                var reader = new XmlSerializer(typeof(SaveData));
                var data = reader.Deserialize(file) as SaveData;
                file.Close();
                data.LoadData();
                score = data.saved_score;
            }
        }

        // public void GetStartScene()
        // {
        //     start_scene = new SaveData(score);
        // }

        [System.Serializable]
        public class SaveData
        {
            
            public BallsData Balls;

            public BricksData Bricks;

            public PaddleData p;

            public int saved_score;

            //public BricksData Bricks;
            
            public SaveData ()
            {

                /////////////Saving the balls///////////////////
                //temp_list = GameObject.FindGameObjectsWithTag("Ball");
                Bricks = new BricksData();
                Balls = new BallsData();
                p = new PaddleData();
            }
            public void LoadData()
            {
                Debug.Log("Loading Data");
                //GameObject[] b_list = GameObject.FindGameObjectsWithTag("Ball");
                GameObject[] brick_list = GameObject.FindGameObjectsWithTag("Brick"); 
                p.LoadThis();
                Balls.LoadThis();
                Bricks.LoadThis();

            }
         }
        
    }
    [System.Serializable]
    public abstract class GameData { }

    // public interface ISaveLoad
    // {
    //     GameData OnSave ();
    //     void OnLoad (GameData data);
    // }
}
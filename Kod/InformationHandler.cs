using Newtonsoft.Json;
using System.Reflection;

namespace Memoji
{
    interface IInformationHandler<in T1, out T2>
    {
        void SaveInfo(T1 t, string filepath);
        T2 GetInfo(string filepath);
    }
    class Tracker<T> : IInformationHandler<T, List<T>>
    {
        public void SaveInfo(T t, string filepath)
        {
            List<T> TList = this.GetInfo(filepath);
            TList.Add(t);
            string TList_json = JsonConvert.SerializeObject(TList);
            File.WriteAllText(filepath, TList_json);
        }

        public List<T> GetInfo(string filepath)
        {

            if (!File.Exists(filepath))
            {
                using (FileStream fs = File.Create(filepath)) { }
            }

            var TList_json = File.ReadAllText(filepath);
            var TList = JsonConvert.DeserializeObject<List<T>>(TList_json);
            if (TList is not null)
                return TList;

            return new List<T>();
        }
    }

    class Score
    {
        public string gamemodeName = "";
        public string difficultyName = "";
        public string playerName = "";
        public int moves;

        [JsonConstructor]
        public Score() { }

        public Score(GameMode game)
        {
            this.gamemodeName = game.GameModeName;
            this.difficultyName = game.Deck.DifficultyName;
            this.playerName = game.Player.name;
            this.moves = game.Player.moves;

        }

    }

}
using System.Runtime.Serialization;
using System.Text.Json;
using TypeAheadApi.Data.Interfaces;

namespace TypeAheadApi.Data
{
    public class Trie : ITrie
    {
        public TrieNode Root => _root; //for testing purposes
        private TrieNode _root;
        private readonly int _suggestionNumber;
        private readonly ILogger<Trie> _logger;
        public Trie(int suggestionNumber, ILogger<Trie> log)
        {
            _root = new TrieNode(' ', null);
            _suggestionNumber = suggestionNumber;
            _logger = log;
        }

        public void InsertWord(string word, int popularity)
        {
            TrieNode node = _root;
            string lowercase_word = word.ToLower();

            // i have no idea why this code suddenly stopped working
            // foreach (char letter in lowercase_word)
            // {
            //     if (!node.Children.ContainsKey(letter))
            //         node.Children.Add(letter, new TrieNode(letter, null));

            //     node = node.Children[letter];
            // }

            // var newWordData = node.WordData;
            // newWordData = new WordData(word, popularity);
            // node.WordData = newWordData;//new WordData(word, popularity);

            for (int i = 0; i < lowercase_word.Length; i++)
            {
                var letter = lowercase_word[i];

                if (!node.Children.ContainsKey(letter))
                {
                    if (i == lowercase_word.Length - 1)
                        node.Children.Add(letter, new TrieNode(letter, new WordData(word, popularity)));
                    else
                        node.Children.Add(letter, new TrieNode(letter, null));
                }

                node = node.Children[letter];
            }


            return;
        }

        public WordData IncreasePopularity(string word)
        {
            TrieNode node = _root;
            string lowercase_word = word.ToLower();

            foreach (char letter in lowercase_word)
            {
                if (!node.Children.ContainsKey(letter))
                {
                    _logger.LogError($"Word '{word}' does not exist.");
                    throw new Exception("Word does not exist"); // create our own exception here
                }

                node = node.Children[letter];
            }

            node.IncreasePopularity();


            return node.WordData!;
        }

        public List<WordData> GetTypeaheadWords(string prefix)
        {
            var node = _root;
            prefix = prefix.ToLower();

            foreach (char c in prefix)
            {
                if (node.Children.ContainsKey(c))
                    node = node.Children[c];
                else
                    return new List<WordData>();
            }

            _logger.LogInformation($"[1] node: {node.Letter}, {(node.WordData is null ? "" : node.WordData.Word)}");

            var wordsWithSamePrefix = new List<WordData>();
            var prefixWordData = node.WordData != null ? node.WordData.Clone() : null;

            this.GetWordsWithSamePrefix(node, wordsWithSamePrefix);

            _logger.LogInformation($"[2] wordsWithSamePrefi: {wordsWithSamePrefix.Count}");

            //order by popularity desc and then by word asc
            wordsWithSamePrefix.Sort((wordData1, wordData2) =>
            {
                int cmp = wordData2.Popularity.CompareTo(wordData1.Popularity);
                if (cmp == 0)
                {
                    return wordData1.Word.CompareTo(wordData2.Word);
                }
                return cmp;
            });

            //insert word that match prefix at first position
            if (prefixWordData != null)
                wordsWithSamePrefix.Insert(0, prefixWordData.Clone());

            //return only SUGGESTION_NUMBER items
            return wordsWithSamePrefix.Take(_suggestionNumber).ToList();
        }

        private void GetWordsWithSamePrefix(TrieNode prefixNode, List<WordData> resultVec)
        {
            foreach (TrieNode childNode in prefixNode.Children.Values)
            {
                if (childNode.WordData != null)
                {
                    resultVec.Add(childNode.WordData.Clone());
                }

                GetWordsWithSamePrefix(childNode, resultVec);
            }
        }
    }

    public struct TrieNode
    {
        public Dictionary<char, TrieNode> Children;
        public char Letter;
        public WordData? WordData;

        public TrieNode(char letter, WordData? wordData)
        {
            this.Children = new Dictionary<char, TrieNode>();
            this.Letter = letter;
            this.WordData = wordData;
        }

        public void IncreasePopularity()
        {
            if (this.WordData == null)
            {
                throw new Exception("Word does not exist.");
            }
            this.WordData.Popularity += 1;
        }
    }

    // [Serializable]
    public class WordData
    {
        public string Word { get; set; }
        public int Popularity { get; set; }

        public WordData(string word, int popularity)
        {
            this.Word = word;
            this.Popularity = popularity;
        }

        //deep copy
        public WordData Clone()
        {
            return new WordData(this.Word, this.Popularity);
        }

        public static bool operator ==(WordData? wd1, WordData? wd2)
        {
            if (wd1 is null && wd2 is not null)
                return false;
            else if (wd1 is not null && wd2 is null)
                return false;
            else if (wd1 is null && wd2 is null)
                return true;
            else
                return wd1!.Word.Equals(wd2!.Word) && wd1.Popularity.Equals(wd2.Popularity);

        }

        public static bool operator !=(WordData? wd1, WordData? wd2)
        {
            if (wd1 is null && wd2 is not null)
                return true;
            else if (wd1 is not null && wd2 is null)
                return true;
            else if (wd1 is null && wd2 is null)
                return false;
            else
                return !(wd1!.Word == wd2!.Word) || !wd1.Popularity.Equals(wd2.Popularity);
        }

        public override bool Equals(object? obj)
        {
            var other = obj as WordData;

            if (other == null)
                return false;

            return this.Word == other.Word && this.Popularity == other.Popularity;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}



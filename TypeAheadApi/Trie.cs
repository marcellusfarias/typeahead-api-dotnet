namespace TrieNamespace
{
    public class Trie
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TrieNode Root;
        public int SuggestionNumber;

        public Trie(int suggestionNumber)
        {
            this.Root = new TrieNode(' ', null);
            this.SuggestionNumber = suggestionNumber;
        }

        public void Initialize(string fileContent)
        {
            //load from file content
            Dictionary<string, int> values = new Dictionary<string, int>();

            foreach (var (word, popularity) in values)
            {
                this.InsertWord(word, popularity);
            }

            return;
        }

        internal void InsertWord(string word, int popularity)
        {
            if (string.IsNullOrEmpty(word))
                throw new Exception("No empty words.");

            TrieNode node = this.Root;
            string lowercase_word = word.ToLower();

            foreach (char letter in lowercase_word)
            {
                if (node.Children.ContainsKey(letter))
                    node.Children.Add(letter, new TrieNode(letter, null));

                node = node.Children[letter];
            }

            node.WordData = new WordData(word, popularity);

            return;
        }

        internal WordData IncreasePopularity(string word)
        {
            TrieNode node = this.Root;
            string lowercase_word = word.ToLower();

            foreach (char letter in lowercase_word)
            {
                try
                {
                    node = node.Children[letter];
                }
                catch (Exception)
                {
                    log.Error("Word does not exist.");
                }
            }

            if (node.WordData == null)
            {
                log.Error("Word does not exist.");
                throw new Exception("Word does not exist.");
            }

            WordData updatedWordData = new WordData(node.WordData.Word, node.WordData.Popularity + 1);
            node.WordData = updatedWordData;

            return node.WordData;
        }

        public List<WordData> GetTypeaheadWords(string prefix)
        {
            var node = this.Root;
            prefix = prefix.ToLower();

            foreach (char c in prefix)
            {
                if (node.Children.ContainsKey(c))
                {
                    node = node.Children[c];
                }
                else
                {
                    return new List<WordData>();
                }
            }

            var wordsWithSamePrefix = new List<WordData>();
            var prefixWordData = node.WordData.Clone();

            GetWordsWithSamePrefix(node, wordsWithSamePrefix);

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
            {
                wordsWithSamePrefix.Insert(0, prefixWordData.Clone());
            }

            //return only SUGGESTION_NUMBER items
            wordsWithSamePrefix.RemoveRange(this.SuggestionNumber, wordsWithSamePrefix.Count - this.SuggestionNumber);

            return wordsWithSamePrefix;
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

    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children;
        public char Letter;
        public WordData WordData;

        public TrieNode(char letter, WordData wordData)
        {
            this.Children = new Dictionary<char, TrieNode>();
            this.Letter = letter;
            this.WordData = wordData;
        }
    }

    public class WordData
    {
        public string Word;
        public int Popularity;

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
    }
}



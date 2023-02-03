using System.Text.Json;
using TypeAheadApi.Data.Interfaces;
using TypeAheadApi.Utils.Exceptions;

namespace TypeAheadApi.Data
{
    public class TrieFactory : ITrieFactory
    {
        private readonly ILogger<TrieFactory> _logger;
        private readonly ILogger<Trie> _loggerTrie;
        private readonly int _suggestionNumber;
        public TrieFactory(int suggestionNumber, ILogger<TrieFactory> logger, ILogger<Trie> loggerTrie)
        {
            _suggestionNumber = suggestionNumber;
            _logger = logger;
            _loggerTrie = loggerTrie;
        }
        public ITrie Initialize(string fileContent)
        {
            try
            {
                Trie trie = new Trie(_suggestionNumber, _loggerTrie);

                Dictionary<string, int>? values = JsonSerializer.Deserialize<Dictionary<string, int>>(fileContent);

                if (values == null)
                    throw new ArgumentNullException("Invalid file");

                foreach (var (word, popularity) in values)
                {
                    trie.InsertWord(word, popularity);
                }

                return trie;
            }
            catch (JsonException)
            {
                _logger.LogError("Invalid file.");
                throw new InvalidFileException();
            }
            catch (Exception)
            {
                _logger.LogError("Unexpected error.");
                throw;
            }
        }
    }
}
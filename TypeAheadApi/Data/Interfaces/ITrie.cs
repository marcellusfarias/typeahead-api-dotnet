
namespace TypeAheadApi.Data.Interfaces
{
    public interface ITrie
    {
        TrieNode Root { get; }
        void InsertWord(string word, int popularity);
        WordData IncreasePopularity(string word);
        List<WordData> GetTypeaheadWords(string prefix);
    }
}
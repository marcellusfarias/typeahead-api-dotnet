
namespace TypeAheadApi.Data.Interfaces
{
    public interface ITrie
    {
        void Initialize(string fileContent);
        void InsertWord(string word, int popularity);
        WordData IncreasePopularity(string word);
        List<WordData> GetTypeaheadWords(string prefix);
    }
}
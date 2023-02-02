using TypeAheadApi.Data.Interfaces;

public interface ITrieFactory
{
    ITrie Initialize(string fileContent);
}
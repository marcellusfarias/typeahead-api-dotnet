using TrieNamespace;
using System.Text.Json;

namespace TypeAheadApi.Tests;

[TestFixture]
public class Tests
{
    #region Testing Tries
    private Trie InitializeTestingTrie()
    {
        var expectedTrie = new Trie(10);

        TrieNode node = expectedTrie.Root;

        // (A) first level
        node.Children.Add('a', new TrieNode('a', null));

        // (A) second level
        node = node.Children['a'];
        node.Children.Add('a', new TrieNode('a', null));
        node.Children.Add('b', new TrieNode('b', null));
        node.Children.Add('-', new TrieNode('-', null));

        // (A) third level
        node = node.Children['a'];
        node.Children.Add('r', new TrieNode('r', new WordData("Aar", 361)));

        // (A) fourth level
        node = node.Children['r'];
        node.Children.Add('i', new TrieNode('i', new WordData("Aari", 151)));

        // (A) third level
        node = expectedTrie.Root;
        node = node.Children['a'];
        node = node.Children['b'];
        node.Children.Add('a', new TrieNode('a', new WordData("Aba", 608)));

        // (A) fourth level
        node = node.Children['a'];
        node.Children.Add('g', new TrieNode('g', new WordData("Abag", 704)));

        // (A) third level
        node = expectedTrie.Root;
        node = node.Children['a'];
        node = node.Children['b'];
        node.Children.Add('e', new TrieNode('e', new WordData("Abe", 300)));

        // (A) third level
        node = expectedTrie.Root;
        node = node.Children['a'];
        node = node.Children['-'];
        node.Children.Add('b', new TrieNode('b', new WordData("A-b", 23)));

        // (B) first level
        node = expectedTrie.Root;
        node.Children.Add('b', new TrieNode('b', null));

        // (B) second level
        node = node.Children['b'];
        node.Children.Add('a', new TrieNode('a', new WordData("Ba", 5)));
        node.Children.Add('e', new TrieNode('e', new WordData("Be", 50)));
        node.Children.Add('c', new TrieNode('c', new WordData("Bc", 50)));

        // (B) third level
        node = node.Children['a'];
        node.Children.Add('h', new TrieNode('h', new WordData("Bah", 5)));

        return expectedTrie;
    }
    private Trie InsertWordTestingTrie()
    {
        var expectedTrie = new Trie(10);
        TrieNode node = expectedTrie.Root;
        // (A) first level
        node.Children.Add('a', new TrieNode('a', null));

        // (A) second level
        node = node.Children['a'];
        node.Children.Add('a', new TrieNode('a', null));
        node.Children.Add('b', new TrieNode('b', null));
        node.Children.Add('-', new TrieNode('-', null));

        // (A) third level
        node = node.Children['a'];
        node.Children.Add('r', new TrieNode('r', new WordData("Aar", 361)));

        // (A) fourth level
        node = node.Children['r'];
        node.Children.Add('i', new TrieNode('i', new WordData("Aari", 151)));

        // (A) third level
        node = expectedTrie.Root.Children['a'].Children['b'];
        node.Children.Add('a', new TrieNode('a', new WordData("Aba", 608)));

        // (A) fourth level
        node = node.Children['a'];
        node.Children.Add('g', new TrieNode('g', new WordData("Abag", 704)));

        // (A) third level
        node = expectedTrie.Root.Children['a'].Children['b'];
        node.Children.Add('e', new TrieNode('e', new WordData("Abe", 300)));

        // (A) third level
        node = expectedTrie.Root.Children['a'].Children['-'];
        node.Children.Add('b', new TrieNode('b', new WordData("A-b", 23)));

        // (B) first level
        node = expectedTrie.Root;
        node.Children.Add('b', new TrieNode('b', null));

        // (B) second level
        node = node.Children['b'];
        node.Children.Add('a', new TrieNode('a', new WordData("Ba", 5)));
        node.Children.Add('e', new TrieNode('e', new WordData("Be", 50)));
        node.Children.Add('c', new TrieNode('c', new WordData("Bc", 50)));

        // (B) third level
        node = node.Children['a'];
        node.Children.Add('h', new TrieNode('h', new WordData("Bah", 5)));

        // (C) first level
        node = expectedTrie.Root;
        node.Children.Add('c', new TrieNode('c', null));

        // (C) second level
        node = node.Children['c'];
        node.Children.Add('a', new TrieNode('a', new WordData("Ca", 150)));

        return expectedTrie;
    }
    private Trie IncreasePopularityTestingTrie()
    {
        Trie expectedTrie = new Trie(10);
        TrieNode node = expectedTrie.Root;
        // (A) first level
        node.Children.Add('a', new TrieNode('a', null));

        // (A) second level
        node = node.Children['a'];
        node.Children.Add('a', new TrieNode('a', null));
        node.Children.Add('b', new TrieNode('b', null));

        // (A) third level
        node = node.Children['a'];
        node.Children.Add('r', new TrieNode('r', new WordData("Aar", 361)));

        // (A) fourth level
        node = node.Children['r'];
        node.Children.Add('i', new TrieNode('i', new WordData("Aari", 151)));

        // (A) third level
        node = expectedTrie.Root.Children['a'].Children['b'];
        node.Children.Add('a', new TrieNode('a', new WordData("Aba", 608)));

        // (A) fourth level
        node = node.Children['a'];
        node.Children.Add('g', new TrieNode('g', new WordData("Abag", 704)));

        // (A) third level
        node = expectedTrie.Root.Children['a'].Children['b'];
        node.Children.Add('e', new TrieNode('e', new WordData("Abe", 301)));

        // (B) first level
        node = expectedTrie.Root;
        node.Children.Add('b', new TrieNode('b', null));

        // (B) second level
        node = node.Children['b'];
        node.Children.Add('a', new TrieNode('a', new WordData("Ba", 5)));
        node.Children.Add('e', new TrieNode('e', new WordData("Be", 50)));
        node.Children.Add('c', new TrieNode('c', new WordData("Bc", 50)));

        // (B) third level
        node = node.Children['a'];
        node.Children.Add('h', new TrieNode('h', new WordData("Bah", 5)));

        return expectedTrie;
    }
    #endregion

    private void PrintTrie(TrieNode node, int i)
    {
        if (node.WordData == null)
            Console.WriteLine("[{0}] {1}", i, node.Letter);
        else
            Console.WriteLine("[{0}] {1}-{2},{3}", i, node.Letter, node.WordData.Word, node.WordData.Popularity);

        i = i + 1;
        foreach (var child in node.Children)
        {
            PrintTrie(child.Value, i);
        }
    }
    private bool CompareTries(TrieNode rootA, TrieNode rootB)
    {
        //two comparisons because order can change.
        Console.WriteLine("Comparing b to a");
        var isBEqualA = RecursivelyCompareTries(rootA, rootB);

        Console.WriteLine("Comparing a to b");
        var isAEqualB = RecursivelyCompareTries(rootB, rootA);

        return isBEqualA && isAEqualB;
    }
    private bool RecursivelyCompareTries(TrieNode nodeA, TrieNode nodeB)
    {
        var returnValue = true;

        if (nodeA.Letter != nodeB.Letter || nodeA.WordData != nodeB.WordData)
        {
            Console.WriteLine("nodeA: {0}, nodeB: {1}", JsonSerializer.Serialize(nodeA), JsonSerializer.Serialize(nodeB));
            return false;
        }

        foreach (var childA in nodeA.Children)
        {
            var childB = nodeB.Children[childA.Key];

            if (childB == null || returnValue == false)
            {
                Console.WriteLine("childA: {0}, childB: {1}", JsonSerializer.Serialize(childA), JsonSerializer.Serialize(childB));
                return false;
            }
            else
            {
                returnValue = RecursivelyCompareTries(childA.Value, childB);
            }
        }

        return returnValue;
    }

    [Test]
    public void TInitializeValidFileContent()
    {
        string fileContent = "{\"A-b\": 23, \"Aar\":361,\"Aari\":151,\"Aba\":608,\"Abag\":704, \"Abe\": 300, \"Ba\": 5, \"Bah\": 5, \"Be\": 50, \"Bc\": 50}";
        Trie trie = new Trie(10);
        trie.Initialize(fileContent);

        var expectedTrie = InitializeTestingTrie();

        Console.WriteLine("Printing expected trie");
        PrintTrie(expectedTrie.Root, 0);

        Console.WriteLine("Printing trie");
        PrintTrie(trie.Root, 0);

        Assert.IsTrue(CompareTries(trie.Root, expectedTrie.Root));
    }

    [Test]
    public void TInitializeInvalidFileContent()
    {
        string fileContent = string.Empty;
        Trie trie = new Trie(10);

        Assert.Throws<JsonException>(() => trie.Initialize(fileContent));
    }

    [Test]
    public void TInsertWordOk()
    {
        Trie trie = InitializeTestingTrie();
        string word = "Ca";
        int popularity = 150;

        trie.InsertWord(word, popularity);

        var expectedTrie = InsertWordTestingTrie();

        Console.WriteLine("Printing expected trie");
        PrintTrie(expectedTrie.Root, 0);

        Console.WriteLine("Printing trie");
        PrintTrie(trie.Root, 0);

        Assert.IsTrue(CompareTries(trie.Root, expectedTrie.Root));
    }

    [Test]
    public void T_IncreasePopularityWordExists()
    {
        string fileContent = "{\"Aar\":361,\"Aari\":151,\"Aba\":608,\"Abag\":704, \"Abe\": 300, \"Ba\": 5, \"Bah\": 5, \"Be\": 50, \"Bc\": 50}";
        Trie trie = new Trie(10);
        trie.Initialize(fileContent);
        trie.IncreasePopularity("Abe");

        Trie expectedTrie = IncreasePopularityTestingTrie();

        Assert.IsTrue(CompareTries(trie.Root, expectedTrie.Root));
    }

    [Test]
    public void T_IncreasePopularityWordDoesNotExist()
    {
        string fileContent = "{\"Aar\":361,\"Aari\":151,\"Aba\":608,\"Abag\":704, \"Abe\": 300, \"Ba\": 5, \"Bah\": 5, \"Be\": 50, \"Bc\": 50}";
        Trie trie = new Trie(10);
        trie.Initialize(fileContent);

        Assert.Throws<Exception>(() => trie.IncreasePopularity("Abcd"), "Word does not exist");
    }
}
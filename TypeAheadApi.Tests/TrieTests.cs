using TypeAheadApi.Data;
using System.Text.Json;
using TypeAheadApi.Data.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using TypeAheadApi.Utils.Exceptions;

namespace TypeAheadApi.Tests;

[TestFixture]
public class Tests
{
    #region Testing Tries

    private Trie InitializeTestingTrie(int suggestionNumber = 10)
    {
        var expectedTrie = new Trie(suggestionNumber, GetLogger());

        TrieNode node = expectedTrie.Root;

        #region Setting root

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

        #endregion

        return expectedTrie;
    }
    private Trie InsertWordTestingTrie()
    {
        var expectedTrie = new Trie(10, this.GetLogger());
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
        Trie expectedTrie = new Trie(10, this.GetLogger());
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

    #region Helpful methods
    private ILogger<Trie> GetLogger()
    {
        var mock = new Mock<ILogger<Trie>>();
        return mock.Object;
    }
    private ILogger<TrieFactory> GetLoggerFactory()
    {
        var mock = new Mock<ILogger<TrieFactory>>();
        return mock.Object;
    }

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

        if (!isBEqualA)
            return false;

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
            // var childB = nodeB.Children[childA.Key];
            var doesNodeBContainsChildAKey = nodeB.Children.ContainsKey(childA.Key);

            if (doesNodeBContainsChildAKey == false || returnValue == false)
            {
                Console.WriteLine("No child element on Tree B for node: {0}", JsonSerializer.Serialize(childA));
                return false;
            }
            else
            {
                returnValue = RecursivelyCompareTries(childA.Value, nodeB.Children[childA.Key]);
            }
        }

        return returnValue;
    }

    #endregion

    [Test]
    public void T_Initialize_ValidFileContent()
    {
        string fileContent = "{\"A-b\": 23, \"Aar\":361,\"Aari\":151,\"Aba\":608,\"Abag\":704, \"Abe\": 300, \"Ba\": 5, \"Bah\": 5, \"Be\": 50, \"Bc\": 50}";
        ITrie trie = new TrieFactory(10, GetLoggerFactory(), GetLogger()).Initialize(fileContent);
        Console.WriteLine("Printing trie");
        PrintTrie(trie.Root, 0);

        var expectedTrie = InitializeTestingTrie();
        Console.WriteLine("Printing expected trie");
        PrintTrie(expectedTrie.Root, 0);

        Assert.IsTrue(CompareTries(trie.Root, expectedTrie.Root));
    }

    [Test]
    public void T_Initialize_InvalidFileContent()
    {
        string fileContent = string.Empty;

        Assert.Throws<InvalidFileException>(() => new TrieFactory(10, GetLoggerFactory(), GetLogger()).Initialize(fileContent));
    }

    [Test]
    public void T_InsertWord_Ok()
    {
        Trie trie = InitializeTestingTrie();

        string word = "Ca";
        int popularity = 150;

        trie.InsertWord(word, popularity);
        Console.WriteLine("Printing trie");
        PrintTrie(trie.Root, 0);

        var expectedTrie = InsertWordTestingTrie();
        Console.WriteLine("Printing expected trie");
        PrintTrie(expectedTrie.Root, 0);

        Assert.IsTrue(CompareTries(trie.Root, expectedTrie.Root));
    }

    [Test]
    public void T_IncreasePopularity_WordExists()
    {
        string fileContent = "{\"Aar\":361,\"Aari\":151,\"Aba\":608,\"Abag\":704, \"Abe\": 300, \"Ba\": 5, \"Bah\": 5, \"Be\": 50, \"Bc\": 50}";
        ITrie trie = new TrieFactory(10, GetLoggerFactory(), GetLogger()).Initialize(fileContent);
        trie.IncreasePopularity("Abe");
        Console.WriteLine("Printing trie");
        PrintTrie(trie.Root, 0);

        Trie expectedTrie = IncreasePopularityTestingTrie();
        Console.WriteLine("Printing expected trie");
        PrintTrie(expectedTrie.Root, 0);

        Assert.IsTrue(CompareTries(trie.Root, expectedTrie.Root));
    }

    [Test]
    public void T_IncreasePopularity_WordDoesNotExist()
    {
        string fileContent = "{\"Aar\":361,\"Aari\":151,\"Aba\":608,\"Abag\":704, \"Abe\": 300, \"Ba\": 5, \"Bah\": 5, \"Be\": 50, \"Bc\": 50}";
        ITrie trie = new TrieFactory(10, GetLoggerFactory(), GetLogger()).Initialize(fileContent);

        Assert.Throws<WordDoesNotExistException>(() => trie.IncreasePopularity("Abcd"), "Word Abcd does not exist");
    }


    [Test]
    public void T_GetTypeaheadWords_PrefixNotIncluded()
    {
        Trie trie = InitializeTestingTrie();
        List<WordData> words = trie.GetTypeaheadWords("Ab").ToList();

        List<WordData> expectedWords = new List<WordData>() {
            new WordData("Abag", 704),
            new WordData("Aba", 608),
            new WordData("Abe", 300)
        };

        CollectionAssert.AreEqual(expectedWords, words);
    }

    [Test]
    public void T_GetTypeaheadWords_ExactMatchPrefix()
    {
        Trie trie = InitializeTestingTrie();

        List<WordData> words = trie.GetTypeaheadWords("Aba").ToList();

        List<WordData> expectedWords = new List<WordData>() {
            new WordData("Aba", 608),
            new WordData("Abag", 704)
        };

        CollectionAssert.AreEqual(expectedWords, words);
    }

    [Test]
    public void T_GetTypeaheadWords_MoreWordsThanSuggestionNumber()
    {
        var trie = InitializeTestingTrie(2);

        var words = trie.GetTypeaheadWords("Ab").ToList();

        var expectedWords = new List<WordData> {
            new WordData("Abag", 704),
            new WordData("Aba", 608)
        };

        CollectionAssert.AreEqual(expectedWords, words);
    }

    [Test]
    public void T_GetTypeaheadWords_LessWordsThanSuggestionNumber()
    {
        var trie = InitializeTestingTrie();
        var words = trie.GetTypeaheadWords("Ab").ToList();

        var expectedWords = new List<WordData> {
            new WordData("Abag", 704),
            new WordData("Aba", 608),
            new WordData("Abe", 300)
        };

        CollectionAssert.AreEqual(expectedWords, words);
    }

    [Test]
    public void T_GetTypeaheadWords_EmptyPrefix()
    {
        Trie trie = InitializeTestingTrie(3);
        var words = trie.GetTypeaheadWords("");
        var expectedWords = new List<WordData>
        {
            new WordData("Abag", 704),
            new WordData("Aba", 608),
            new WordData("Aar", 361)
        };

        CollectionAssert.AreEqual(expectedWords, words);
    }

    [Test]
    public void T_GetTypeaheadWords_SamePopularityWords()
    {
        Trie trie = InitializeTestingTrie(2);
        var words = trie.GetTypeaheadWords("B");

        var expectedWords = new List<WordData>
        {
            new WordData("Bc", 50),
            new WordData("Be", 50)
        };

        CollectionAssert.AreEqual(expectedWords, words);
    }

    [Test]
    public void T_GetTypeaheadWords_CaseInsensitive()
    {
        var trie = InitializeTestingTrie(2);
        var words = trie.GetTypeaheadWords("b");
        var expectedWords = new List<WordData>
        {
            new WordData("Bc", 50),
            new WordData("Be", 50),
        };

        CollectionAssert.AreEqual(expectedWords, words);

        words = trie.GetTypeaheadWords("AA");
        expectedWords = new List<WordData>
        {
            new WordData("Aar", 361),
            new WordData("Aari", 151),
        };

        CollectionAssert.AreEqual(expectedWords, words);
    }

    [Test]
    public void T_GetTypeaheadWords_TestingOrdering()
    {
        var trie = InitializeTestingTrie(4);
        var words = trie.GetTypeaheadWords("b");
        var expectedWords = new List<WordData>
        {
            new WordData("Bc", 50),
            new WordData("Be", 50),
            new WordData("Ba", 5),
            new WordData("Bah", 5),
        };

        CollectionAssert.AreEqual(expectedWords, words);
    }

    [Test]
    public void T_GetTypeaheadWords_NoWordMatchesPrefix()
    {
        var trie = InitializeTestingTrie(4);
        var words = trie.GetTypeaheadWords("Brazil");
        var expectedWords = new List<WordData>();

        CollectionAssert.AreEqual(expectedWords, words);
    }

    [Test]
    public void T_GetTypeaheadWords_ReturnOnlyPrefix()
    {
        var trie = InitializeTestingTrie(4);
        var words = trie.GetTypeaheadWords("Bah");

        var expectedWords = new List<WordData> { new WordData("Bah", 5) };

        CollectionAssert.AreEqual(expectedWords, words);
    }

    [Test]
    public void T_GetTypeaheadWords_PrefixWithSpecialCharacters()
    {
        Trie trie = InitializeTestingTrie(4);
        var words = trie.GetTypeaheadWords("A-".ToString());
        List<WordData> expected_words = new List<WordData>
        {
            new WordData("A-b", 23)
        };

        CollectionAssert.AreEqual(expected_words, words);
    }


}
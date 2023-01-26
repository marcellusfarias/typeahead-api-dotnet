using TrieNamespace;

namespace TypeAheadApi.Tests;

[TestFixture]
public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

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

    private void PrintTrie(TrieNode node, int i)
    {
        Console.WriteLine("[{0}] {1}-{2}", i, node.Letter, node.WordData);
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
            Console.WriteLine("nodeA: {0}, nodeB: {1}", nodeA, nodeB);
            return false;
        }

        foreach (var childA in nodeA.Children)
        {
            var childB = nodeB.Children.GetValueOrDefault(childA.Key);

            if (childB == null || returnValue == false)
            {
                Console.WriteLine("childA: {0}, childB: {1}", childA, childB);
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

        PrintTrie(expectedTrie.Root, 0);
        PrintTrie(trie.Root, 0);

        Assert.IsTrue(CompareTries(trie.Root, expectedTrie.Root));
    }
}
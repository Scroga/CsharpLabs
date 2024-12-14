using BenchmarkDotNet;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftAntimalwareEngine;
using System.Threading;
using System.Globalization;

namespace IncrementWordCountInDictionary;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Scenario1
{
    private Dictionary<string, int>? dict;

    static void IncrementWordCount_V1(IDictionary<string, int> wordToCountDictionary, string word)
    {
        try
        {
            wordToCountDictionary[word]++;
        }
        catch (KeyNotFoundException)
        {
            wordToCountDictionary[word] = 1;
        }
    }

    static void IncrementWordCount_V2(IDictionary<string, int> wordToCountDictionary, string word)
    {
        if (wordToCountDictionary.ContainsKey(word))
        {
            wordToCountDictionary[word]++;
        }
        else
        {
            wordToCountDictionary[word] = 1;
        }
    }

    static void IncrementWordCount_V3(IDictionary<string, int> wordToCountDictionary, string word)
    {
        _ = wordToCountDictionary.TryGetValue(word, out int value);     // If not found, value == default(int) == 0
        value++;
        wordToCountDictionary[word] = value;
    }

    [Benchmark]
    public void RunIncrementWordCount_V1()
    {
        dict = new Dictionary<string, int>();
        IncrementWordCount_V1(dict, "one");
        IncrementWordCount_V1(dict, "one");
    }

    [Benchmark]
    public void RunIncrementWordCount_V2()
    {
        dict = new Dictionary<string, int>();
        IncrementWordCount_V2(dict, "two");
        IncrementWordCount_V2(dict, "two");
    }

    [Benchmark]
    public void RunIncrementWordCount_V3()
    {
        dict = new Dictionary<string, int>();
        IncrementWordCount_V3(dict, "three");
        IncrementWordCount_V3(dict, "three");
    }
}

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Scenario2
{
    private string[] _inputWords = 
        { "If", "a", "train", "station", "is", "where", "the", "train", "stops,", "what", "is", "a", "work", "station"};
    private bool _printResult;

    public Scenario2(bool printResult = false) 
    {
        _printResult = printResult;
    }

    [Benchmark]
    public void SortedList()
    {
        if (_printResult) Console.WriteLine("\nSorted List:");
        var sortedList = new SortedList<string, int>();

        foreach (var word in _inputWords)
        {
            _ = sortedList.TryGetValue(word, out int value);
            value++;
            sortedList[word] = value;
        }

        foreach (var item in sortedList)
        {
            if(_printResult) Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }

    [Benchmark]
    public void SortedDictionary()
    {
        if (_printResult) Console.WriteLine("\nSorted Dictionary:");
        var sortedDict = new SortedDictionary<string, int>();

        foreach(var word in _inputWords) 
        {
            _ = sortedDict.TryGetValue(word, out int value);
            value++;
            sortedDict[word] = value;
        }

        foreach (var item in sortedDict)
        {
            if (_printResult) Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }

    [Benchmark]
    public void SimpleDictionary()
    {
        if (_printResult) Console.WriteLine("\nSimle Dictionary:");
        var simpleDict = new Dictionary<string, int>();

        foreach (var word in _inputWords)
        {
            _ = simpleDict.TryGetValue(word, out int value);
            value++;
            simpleDict[word] = value;
        }

        var keys = new List<string>(simpleDict.Keys);
        keys.Sort();

        foreach (var key in keys)
        {
            if (_printResult) Console.WriteLine($"{key}: {simpleDict[key]}");
        }
    }

}

internal class Program
{
    static void Main(string[] args)
    {
        /**/
        BenchmarkRunner.Run<Scenario1>();
        BenchmarkRunner.Run<Scenario2>();
        /*/
        Scenario2 s2 = new Scenario2(true);
        s2.SimpleDictionary();
        s2.SortedDictionary();
        s2.SortedList();
        /**/
    }
}
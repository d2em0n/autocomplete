using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete;

internal class AutocompleteTask
{
    /// <returns>
    /// Возвращает первую фразу словаря, начинающуюся с prefix.
    /// </returns>
    /// <remarks>
    /// Эта функция уже реализована, она заработает, 
    /// как только вы выполните задачу в файле LeftBorderTask
    /// </remarks>
    public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
    {
        var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
            return phrases[index];

        return null;
    }

    /// <returns>
    /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
    /// элементов словаря, начинающихся с prefix.
    /// </returns>
    /// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
    public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
    {
        var leftBorder = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        var rightBorder = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);

        var length = (rightBorder - leftBorder) >= count ? count : rightBorder - leftBorder;
        var topByPrefix = new string[length];
        for (var i = 0; i < length; i++)
        {
            topByPrefix[i] = (phrases[i + leftBorder]);
        }
        // тут стоит использовать написанный ранее класс LeftBorderTask
        return topByPrefix;
    }

    /// <returns>
    /// Возвращает количество фраз, начинающихся с заданного префикса
    /// </returns>
    public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
    {
        // тут стоит использовать написанные ранее классы LeftBorderTask и RightBorderTask
        var rightBorder = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
        var leftBorder = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
        return rightBorder - leftBorder - 1;
    }
}

[TestFixture]
public class AutocompleteTests
{
    [Test]
    public void TopByPrefix_IsEmpty_WhenNoPhrases()
    {
        CollectionAssert.IsEmpty(AutocompleteTask.GetTopByPrefix(new string[] { "ab", "ab", "ab", "ab" }, "aa", 0));
    }

    [Test]
    public void FirstFailedTestByUlearn()
    {
        Assert.AreEqual("aa", AutocompleteTask.GetTopByPrefix(new string[] { "aa", "ab", "ac", "bc" }, "a", 2)[0]);
    }

    [Test]
    public void SecondFailedTestByUlearn()
    {
        Assert.AreEqual(1, AutocompleteTask.GetTopByPrefix(new string[] { "aa" }, "a", 2).Length);
    }

    [Test]
    public void ThirdFailedTestByUlearn()
    {
        var stringArray = new string[] { "a", "b", "c", "c", "d", "e" };
        Assert.AreEqual(2, AutocompleteTask.GetTopByPrefix(stringArray, "c", 10).Length);
    }

    [Test]
    public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
    {
        Assert.AreEqual(4, AutocompleteTask.GetCountByPrefix(new string[] { "ab", "ab", "ab", "ab" }, string.Empty));
    }
}
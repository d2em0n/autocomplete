using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Autocomplete;

public class RightBorderTask
{
    /// <returns>
    /// Возвращает индекс правой границы. 
    /// То есть индекс минимального элемента, который не начинается с prefix и большего prefix.
    /// Если такого нет, то возвращает items.Length
    /// </returns>
    /// <remarks>
    /// Функция должна быть НЕ рекурсивной
    /// и работать за O(log(items.Length)*L), где L — ограничение сверху на длину фразы
    /// </remarks>
    public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
    {
        while (left < right)
        {
            var m = left + (right - left) / 2;
            if (m < 0) break;
            if ( !phrases[m].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase) && string.Compare(prefix, phrases[m], StringComparison.InvariantCultureIgnoreCase) <= 0 )
                right = m;
            else left = m + 1;
        }
        return right;
    }
}

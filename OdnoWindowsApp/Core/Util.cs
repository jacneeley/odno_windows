using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OdnoWindowsApp.Core
{
    public class Util
    {
        public sealed class TrackNameComparer : IComparer<string>
        {
            public int Compare(string? left, string? right)
            {
                if (left == right) return 0;
                if (left is null) return -1;
                if (right is null) return 1;

                var reg = new Regex(@"\d+"); //match number

                var leftMatch = reg.Match(left);
                var rightMatch = reg.Match(right);

                if (leftMatch.Success || rightMatch.Success) {
                    return int.Parse(leftMatch.Captures[0].Value)
                        .CompareTo(int.Parse(rightMatch.Captures[0].Value));
                }

                return string.Compare(left, right);
            }
        }
    }
}

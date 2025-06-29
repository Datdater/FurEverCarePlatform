using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Utils;
public static class UtilityHelper
{
    public static string GenerateRandomCode(int length = 5)
    {
        const string chars = "0123456789";
        var random = new Random();
        var code = new string(
            Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray()
        );
        return code;
    }
}

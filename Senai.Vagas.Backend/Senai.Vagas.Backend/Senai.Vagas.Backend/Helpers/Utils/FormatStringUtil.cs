using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Utils
{
    public static class FormatStringUtil
    {
        public static string ClearString(string text)
        {
            StringBuilder stringRetunr = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                {
                    stringRetunr.Append(letter);
                }
            }
            return stringRetunr.ToString().ToLower();
        }

        public static string CaracterClear(string text)
        {
            return text.Trim().Replace("\\", "").Replace("/", "").Replace("-","").Replace(".", "");
        }
    }
}

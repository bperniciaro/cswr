using System;
using System.Globalization;
using System.Linq;

namespace BP.CheatSheetWarRoom.UI
{
  public static class StringHelpersCS
  {
    public static string TruncateAtWord(this string input, int length)
    {
      if (input == null || input.Length < length)
        return input;
      int iNextSpace = input.LastIndexOf(" ", length);
      return string.Format("{0}", input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim());
    }

    public static string AddNumberSuffix(this string number)
    {
      // Get day modulo...
      int digit = int.Parse(number);

      var dayModulo = digit % 10;

      // Convert day to string...
      var numberWithSuffix = number.ToString(CultureInfo.InvariantCulture);

      // Combine day with correct suffix...
      numberWithSuffix += (digit == 11 || digit == 12 || digit == 13) ? "th" :
          (dayModulo == 1) ? "st" :
          (dayModulo == 2) ? "nd" :
          (dayModulo == 3) ? "rd" :
          "th";

      // Return result...
      return numberWithSuffix;
    }


    /// <summary>
    /// Converts the first letter of a word to upper-case
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string FirstCharToUpper(this string input)
    {
      if (String.IsNullOrEmpty(input))
        throw new ArgumentException("ARGH!");
      return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
    }


    /// <summary>
    /// Gets the last 'x' characters of a string, where 'x' is
    /// the desired tail length 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="tailLength"></param>
    /// <returns></returns>
    public static string GetStringTail(this string source, int tailLength)
    {
      if (tailLength >= source.Length)
        return source;
      return source.Substring(source.Length - tailLength);
    }


    public static string GetPossessive(this string source)
    {
      if (source.Reverse().Take(1).ToString() == "s")
      {
        return source + "'";
      }
      return source +"'s";
    }



  }
}
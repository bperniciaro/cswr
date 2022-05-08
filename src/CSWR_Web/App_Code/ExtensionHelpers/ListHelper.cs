using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace BP.CheatSheetWarRoom.UI
{
  public static class ListHelper
  {


    public static List<T> GroupRandomize<T>(this IList<T> sourceList, int groupSize)
    {
      List<T> shuffledList = new List<T>();
      List<T> tempList = new List<T>();
      int addCounter = 0;
      for (int i = 0; i < sourceList.Count; i++)
      {
        tempList.Add(sourceList[i]);
        // if we've built a full group, or we're done processing the entire list
        if ((addCounter == groupSize - 1) || (i == sourceList.Count - 1))
        {
          tempList.Shuffle();
          shuffledList.AddRange(tempList);
          tempList.Clear();
          addCounter = 0;
        }
        else
        {
          addCounter++;
        }
      }
      return shuffledList;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourceList"></param>
    /// <param name="groupSize"></param>
    /// <param name="lockCount">A count of the top items to ignore from the randomization process</param>
    /// <returns></returns>
    public static List<T> GroupRandomize<T>(this IList<T> sourceList, int groupSize, int lockCount)
    {
      List<T> shuffledList = new List<T>();
      List<T> tempList = new List<T>();
      int addCounter = 0;

      // add the range of items which not be included in the randomization process
      shuffledList.AddRange(sourceList.Take(lockCount));

      // randomize items within groups, starting after the lockCount
      for (int i = lockCount; i < sourceList.Count; i++)
      {
        tempList.Add(sourceList[i]);
        // if we've built a full group, or we're done processing the entire list
        if ((addCounter == groupSize - 1) || (i == sourceList.Count - 1))
        {
          tempList.Shuffle();
          shuffledList.AddRange(tempList);
          tempList.Clear();
          addCounter = 0;
        }
        else
        {
          addCounter++;
        }
      }
      return shuffledList;
    }


    public static void Shuffle<T>(this IList<T> list)
    {
      RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
      int n = list.Count;
      while (n > 1)
      {
        byte[] box = new byte[1];
        do provider.GetBytes(box);
        while (!(box[0] < n * (Byte.MaxValue / n)));
        int k = (box[0] % n);
        n--;
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }
    }

  }
}
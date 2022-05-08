using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;

/// <summary>
/// Summary description for BizObject
/// </summary>

namespace BP.CheatSheetWarRoom.BLL
{
  [Serializable()]
  public abstract class BizObject
  {
    protected const int MAXROWS = int.MaxValue;

    //Get a reference to the current context's cache
    protected static Cache Cache
    {
      get { return HttpContext.Current.Cache; }
    }

    //Get the reference to the current context's user
    protected static IPrincipal CurrentUser
    {
      get { return HttpContext.Current.User; }  
    }

    //Get the name of the current user
    protected static string CurrentUserName
    {
      get
      {
        string userName = "";
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
          userName = HttpContext.Current.User.Identity.Name.Trim();
        }
          return userName;
      }
    }

    //Get a referernce to the current user's ip address
    protected static string CurrentUserIP
    {
      get { return HttpContext.Current.Request.UserHostAddress; }
    }

    //Not sure what this does
    protected static int GetPageIndex(int startRowIndex, int maximumRows)
    {
      if (maximumRows <= 0)
      {
        return 0;
      }
      else
      {
        return (int)Math.Floor((double)startRowIndex / (double)maximumRows);
      }
    }

    //Encode html strings
    protected static string EncodeText(string content)
    {
      content = HttpUtility.HtmlEncode(content);
      content = content.Replace("  ", "&nbsp;&nbsp;").Replace("\n", "<br>");
      return content;
    }

    //Convert a null to an empty string
    protected static string ConvertNullToEmptyString(string input)
    {
      return (input == null ? " " : input);
    }

    //Purge cache items starting with a certain prefix.
    protected static void PurgeCacheItems(string prefix)
    {
      prefix = prefix.ToLower();
      List<string> itemsToRemove = new List<string>();

      IDictionaryEnumerator enumerator = BizObject.Cache.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if(enumerator.Key.ToString().ToLower().StartsWith(prefix)) 
        {
          itemsToRemove.Add(enumerator.Key.ToString());
        }
      }
      foreach(string itemToRemove in itemsToRemove) 
      {
        BizObject.Cache.Remove(itemToRemove);
      }


    }

  }
}
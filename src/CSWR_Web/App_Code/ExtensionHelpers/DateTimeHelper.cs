using System;

/// <summary>
/// Summary description for StringHelper
/// </summary>
namespace BP.CheatSheetWarRoom.UI
{
  public static class DateTimeHelper
  {


    public static DateTime ToCentralStandardDateTime(this DateTime input)
    {
      TimeZoneInfo timeZoneInfo;
      //Set the time zone information to US Mountain Standard Time 
      timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
      //Get date and time in US Mountain Standard Time 
      return TimeZoneInfo.ConvertTime(input, timeZoneInfo);
    }
  
  }
}
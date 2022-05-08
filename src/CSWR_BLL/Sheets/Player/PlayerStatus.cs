using System;

namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  [Serializable()]
  class PlayerStatus
  {
    public PlayerStatus(string sportCode, string statusCode, string statusName, string statusTitle, string description, 
                          bool suppInfoRequired, string suppInfoLabel, bool dynamic)
    {
      this.SportCode = sportCode;
      this.StatusCode = statusCode;
      this.StatusName = statusName;
      this.StatusTitle = StatusTitle;
      this.Description = description;
      this.SuppInfoRequired = suppInfoRequired;
      this.SuppInfoLabel = suppInfoLabel;
      this.Dynamic = dynamic;
    }

    public string SportCode { get; set; }
    public string StatusCode {get;set;}
    public string StatusName { get; set; }
    public string StatusTitle { get; set; }
    public string Description {get;set;}
    public bool SuppInfoRequired { get; set; }
    /// <summary>
    /// Label to help users know what to enter for supplemental information
    /// </summary>
    public string SuppInfoLabel { get; set; }
    public bool Dynamic { get; set; }

  }


}

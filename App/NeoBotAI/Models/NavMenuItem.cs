using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBotAI.Models;

public class NavMenuItem
{
    public NavMenuItem(string tabName, string tabIcon, string tabLink)
    {
        TabName = tabName;
        TabIcon = tabIcon;
        TabLink = tabLink;
    }
    public string TabName { get; set; }
    public string TabIcon { get; set; }
    public string TabLink { get; set; } 
    public string TabId => $"{TabName}-page";
}

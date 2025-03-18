using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBotAI.Models;

public class AppSettings
{
    private static AppSettings? instance;

    public static AppSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AppSettings();
            }
            return instance;
        }
    }

    public SettingItem Temperature => SettingItems.First(x => x.SettingType == SettingType.Temperature);
    public SettingItem UserName => SettingItems.First(x => x.SettingType == SettingType.Username);
    public List<SettingItem> SettingItems { get; set; }
    public delegate void ValueChangeHandler();
    public event ValueChangeHandler ValueChanged;

    public AppSettings()
    {
        SettingItems = new List<SettingItem>
        {
            new SettingItem("Guest", nameof(SettingType.Username), SettingType.Username),
            new SettingItem(0.5, nameof(SettingType.Temperature) ,SettingType.Temperature)
        };

        foreach (var item in SettingItems)
        {
            item.ValueChanged += Item_ValueChanged;
        }
    }

    ~AppSettings()
    {
        foreach (var item in SettingItems)
        {
            item.ValueChanged -= Item_ValueChanged;
        }
    }

    private void Item_ValueChanged()
    {
        ValueChanged?.Invoke();
    }
}

    


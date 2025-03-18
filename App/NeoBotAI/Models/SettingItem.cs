using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBotAI.Models;

public record SettingItem
{
    private object value;

    public object DefaultValue { get; init; }
    public string Text { get; init; }
    public SettingType SettingType { get; init; }
    public object Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
            SaveValues();
        }
    }

    public SettingItem(object defaultValue, string text, SettingType settingType)
    {
        DefaultValue = defaultValue;
        Text = text;
        SettingType = settingType;

        Value = Preferences.Default.Get(text, defaultValue);
    }

    public void SaveValues()
    {
        if(Value is string)
            Preferences.Default.Set(Text, (string)value);
    }

    public delegate void ValueChangeHandler();
    public event ValueChangeHandler ValueChanged;
}

public enum SettingType
{
    Username,
    Temperature
}

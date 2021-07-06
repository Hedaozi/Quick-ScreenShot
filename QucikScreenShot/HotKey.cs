using System;
using System.Windows;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QucikScreenShot
{
    public partial class MainWindow : Window
    {
        static class HotKey
        {
            public static readonly Dictionary<string, Keys> ModifierKeys =
                new Dictionary<string, Keys>()
                {
                    { "Ctrl", Keys.Control },
                    { "Alt", Keys.Alt },
                    { "Shift", Keys.Shift }
                };

            public static readonly Dictionary<string, Keys> FunctionKeys =
                new Dictionary<string, Keys>()
                {
                    { "F1", Keys.F1 },
                    { "F2", Keys.F2 },
                    { "F3", Keys.F3 },
                    { "F4", Keys.F4 },
                    { "F5", Keys.F5 },
                    { "F6", Keys.F6 },
                    { "F7", Keys.F7 },
                    { "F8", Keys.F8 },
                    { "F9", Keys.F9 },
                    { "F10", Keys.F10 },
                    { "F11", Keys.F11 },
                    { "F12", Keys.F12 },
                    { "Esc", Keys.Escape },
                    { "Print Screen", Keys.PrintScreen },
                    { "Scroll Lock", Keys.Scroll },
                    { "Pause", Keys.Pause },
                    { "Insert", Keys.Insert },
                    { "Home", Keys.Home },
                    { "End", Keys.End },
                    { "Delete", Keys.Delete },
                    { "Page Up", Keys.PageUp },
                    { "Page Down", Keys.PageDown },
                    { "Num Lock", Keys.NumLock },
                    { "Caps Lock", Keys.CapsLock },
                    { "Tab", Keys.Tab }
                };

            public static readonly Dictionary<string, Keys> LetterKeys =
                new Dictionary<string, Keys>()
                {
                    { "Q", Keys.Q },
                    { "W", Keys.W },
                    { "E", Keys.E },
                    { "R", Keys.R },
                    { "T", Keys.T },
                    { "Y", Keys.Y },
                    { "U", Keys.U },
                    { "I", Keys.I },
                    { "O", Keys.O },
                    { "P", Keys.P },
                    { "A", Keys.A },
                    { "S", Keys.S },
                    { "D", Keys.D },
                    { "F", Keys.F },
                    { "G", Keys.G },
                    { "H", Keys.H },
                    { "J", Keys.J },
                    { "K", Keys.K },
                    { "L", Keys.L },
                    { "Z", Keys.Z },
                    { "X", Keys.X },
                    { "C", Keys.C },
                    { "V", Keys.V },
                    { "B", Keys.B },
                    { "N", Keys.N },
                    { "M", Keys.M }
                };

            public static readonly Dictionary<string, Keys> NumberKeys =
                new Dictionary<string, Keys>()
                {
                    { "1", Keys.D1 },
                    { "2", Keys.D2 },
                    { "3", Keys.D3 },
                    { "4", Keys.D4 },
                    { "5", Keys.D5 },
                    { "6", Keys.D6 },
                    { "7", Keys.D7 },
                    { "8", Keys.D8 },
                    { "9", Keys.D9 },
                    { "0", Keys.D0 }
                };

            public static readonly Dictionary<string, Keys> SymbolKeys =
                new Dictionary<string, Keys>()
                {
                    { "`", Keys.Oemtilde },
                    //{ "-", Keys.Subtract }, // cannot
                    //{ "=", Keys.Add }, // cannot
                    { "{", Keys.OemOpenBrackets },
                    { "}", Keys.OemCloseBrackets },
                    //{ "\\", Keys.OemBackslash }, // cannot
                    { ";", Keys.OemSemicolon },
                    { "'", Keys.OemQuotes },
                    { ",", Keys.Oemcomma },
                    { ".", Keys.OemPeriod }//,
                    //{ "/", Keys.D8 } // cannot
                };

            public static readonly Dictionary<string, Keys> SpecialKeys =
                new Dictionary<string, Keys>()
                {
                    { "BackSpace", Keys.Back },
                    { "Enter", Keys.Enter },
                    { "SpaceBar", Keys.Space }
                };

            public static Keys StringToKeys(string keyName)
            {
                if (ModifierKeys.ContainsKey(keyName))
                {
                    return ModifierKeys[keyName];
                }
                else if (FunctionKeys.ContainsKey(keyName))
                {
                    return FunctionKeys[keyName];
                }
                else if (LetterKeys.ContainsKey(keyName))
                {
                    return LetterKeys[keyName];
                }
                else if (NumberKeys.ContainsKey(keyName))
                {
                    return NumberKeys[keyName];
                }
                else if (SymbolKeys.ContainsKey(keyName))
                {
                    return SymbolKeys[keyName];
                }
                else
                {
                    return SpecialKeys[keyName];
                }
            }

            public static List<string> AllKeys()
            {
                List<string> allKeys = new List<string>(ModifierKeys.Keys);
                allKeys.AddRange(FunctionKeys.Keys);
                allKeys.AddRange(LetterKeys.Keys);
                allKeys.AddRange(NumberKeys.Keys);
                allKeys.AddRange(SymbolKeys.Keys);
                allKeys.AddRange(SpecialKeys.Keys);
                return allKeys;
            }

            public static List<string> SubKeys(string key, bool allowModifier)
            {
                List<string> subKeys = new List<string>();
                if (allowModifier && ModifierKeys.ContainsKey(key))
                {
                    subKeys.AddRange(ModifierKeys.Keys);
                    subKeys.Remove(key);
                }
                subKeys.AddRange(FunctionKeys.Keys);
                subKeys.AddRange(LetterKeys.Keys);
                subKeys.AddRange(NumberKeys.Keys);
                subKeys.AddRange(SymbolKeys.Keys);
                subKeys.AddRange(SpecialKeys.Keys);
                return subKeys;
            }

            public static bool IsModifierKeys(string key)
            {
                return ModifierKeys.ContainsKey(key);
            }
        }
    }
}

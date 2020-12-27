using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeDieuHanh
{
    public partial class Main : Form
    {
        private IKeyboardMouseEvents m_Events;
        private List<Language> languages;
        private List<String> dictionary;
        private string textEnglish = "";
        private List<String> dictionarySearch;
        private bool boolSearch = true;
        [StructLayout(LayoutKind.Sequential)]
        public struct CURSORINFO
        {
            public Int32 cbSize;        // Specifies the size, in bytes, of the structure.
            public Int32 flags;         // Specifies the cursor state.
            public IntPtr hCursor;      // Handle to the cursor.
            public Point point; // Should already marshal correctly.
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]

        public static extern bool GetCursorInfo(ref CURSORINFO pci);

        public Main()
        {
            InitializeComponent();
            readLanguage(@"C:\Users\trai18\source\repos\HeDieuHanh\language.txt");
            readDictionary(@"C:\Users\trai18\source\repos\HeDieuHanh\HeDieuHanh\en_vi.txt");
            Init();
            SubscribeGlobal();
            FormClosing += Main_Closing;
        }

        private void readLanguage(String file)
        {
            languages = new List<Language>();
            string[] lines = File.ReadAllLines(file);
            char[] delimiter = new char[] { '\t' };
            foreach (string s in lines)
            {
                string[] language = s.Split(delimiter);
                languages.Add(new Language(language[1], language[0]));
            };
        }

        private void readDictionary(String file)
        {
            dictionary = new List<String>();
            string[] lines = File.ReadAllLines(file);
            foreach (string s in lines)
            {
                dictionary.Add(s);
            };
        }

        private void Init()
        {
            dictionarySearch = new List<String>();
            cbb_language.DataSource = languages;
        }

        private void Main_Closing(object sender, CancelEventArgs e)
        {
            Unsubscribe();
        }

        private void SubscribeGlobal()
        {
            Unsubscribe();
            Subscribe(Hook.GlobalEvents());
        }

        private void Subscribe(IKeyboardMouseEvents events)
        {
            m_Events = events;
            m_Events.MouseDoubleClick += OnMouseDoubleClick;
            m_Events.MouseDragFinished += OnMouseDragFinished;
            m_Events.KeyDown += OnKeyDown;
            m_Events.MouseDownExt += GlobalHookMouseDownExt;
        }

        private void Unsubscribe()
        {
            if (m_Events == null) return;
            m_Events.MouseDoubleClick -= OnMouseDoubleClick;
            m_Events.MouseDragFinished -= OnMouseDragFinished;
            m_Events.KeyDown -= OnKeyDown;
            m_Events.MouseDownExt -= GlobalHookMouseDownExt;
            m_Events.Dispose();
            m_Events = null;
        }


        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            
            if (e.Button == MouseButtons.Left)
            {
                this.textEnglish = "";
            }
        }


        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                textEnglish = "";
            }
            else if (e.KeyCode.ToString().ToUpper().Contains("SPACE"))
            {
                textEnglish = "";
            }
            else if (e.KeyCode.ToString().ToUpper().Contains("BACK") && textEnglish.Length > 0)
            {
                if (textEnglish.Length == 1)
                {
                    textEnglish = "";
                }
                else
                    textEnglish = textEnglish.Substring(0, textEnglish.Length - 1);
            }
            else
            {
                if (e.KeyCode.ToString().Length == 1)
                {
                    textEnglish += e.KeyCode.ToString();
                }
            }

            if (textEnglish != "")
            {
                this.search();
            }
            else
            {
                search_context.Items.Clear();
                search_context.Hide();
            };
        }

        private void search()
        {
            this.dictionarySearch = this.dictionary.Where(x => String.IsNullOrEmpty(textEnglish) || x.Split(' ')[0].ToString().ToUpper().StartsWith(textEnglish)).ToList();
            if(this.dictionarySearch.Count > 0)
            {
                int i = 0;
                search_context.Items.Clear();
                foreach (string e in this.dictionarySearch)
                {
                    search_context.Items.Add(e);
                    i++;
                    if (i > 6) break;
                }
                var ci = new CURSORINFO();
                ci.cbSize = Marshal.SizeOf(ci);
                GetCursorInfo(ref ci).ToString();
                var point = new Point
                {
                    X = ci.point.X,
                    Y = ci.point.Y
                };
                search_context.Show(this, point.X + 15 - this.Location.X, point.Y - 20 - this.Location.Y);
            }
            else
            {
                search_context.Items.Clear();
                search_context.Hide();
            }
        }


        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            int x_position = e.X + 15 - this.Location.X;
            int y_position = e.Y - 20 - this.Location.Y;
            Translate(x_position, y_position);
        }


        private void OnMouseDragFinished(object sender, MouseEventArgs e)
        {
            int x_position = e.X - this.Location.X;
            int y_position = e.Y - 20 - this.Location.Y;
            Translate(x_position, y_position);
        }

        public async void Translate(int x, int y)
        {
            string rs = null;
            try
            {
                Clipboard.Clear();
                SendKeys.SendWait("^(c)");
                async Task Puttaskdelay()
                {
                    await Task.Delay(50);
                }
                await Puttaskdelay();
                string input = Clipboard.GetText();
                if(input != null || input == "")
                {
                    rs = callTranslator(input);
                    trans_result.Items.Clear();
                    trans_result.Items.Add(rs);
                    trans_result.Show(this, x, y);
                }
            }
            catch (Exception ex)
            {
                if(rs != null)
                {
                    trans_result.Items.Clear();
                    trans_result.Items.Add(rs);
                    trans_result.Show(this, x, y);
                }
            }
        }

        private string callTranslator(string text)
        {
            Language locale = (Language)cbb_language.SelectedItem;
            TranslatorService.LanguageServiceClient client = new TranslatorService.LanguageServiceClient();
            client = new TranslatorService.LanguageServiceClient();
            return client.Translate("6CE9C85A41571C050C379F60DA173D286384E0F2", text, "", locale.code);
        }

        private void search_context_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }
    }
}

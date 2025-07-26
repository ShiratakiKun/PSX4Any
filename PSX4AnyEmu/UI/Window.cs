using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using NAudio.Wave;
using ProjectPSX.Devices.Input;
using ProjectPSX.Host;
using PSX4Any.PSX4AnyEmu.Interop.Gdi32;
using PSX4Any.PSX4AnyEmu.UI.Components;
using PSX4Any.PSX4AnyEmu.Util;
using PSX4AnyCommon;
using Gdi32 = PSX4Any.PSX4AnyEmu.Interop.Gdi32.NativeMethods;

namespace PSX4Any.PSX4AnyEmu.UI
{

    public class Window : Form, IHostWindow
    {

        private const int PSX_MHZ = 33868800;
        private const int SYNC_CYCLES = 100;
        private const int MIPS_UNDERCLOCK = 3;

        private const int cyclesPerFrame = PSX_MHZ / 60;
        private const int syncLoops = (cyclesPerFrame / (SYNC_CYCLES * MIPS_UNDERCLOCK)) + 1;
        private const int cycles = syncLoops * SYNC_CYCLES;

        private Size vramSize = new Size(1024, 512);
        private Size _640x480 = new Size(640, 480);
        private readonly DoubleBufferedPanel screen = new DoubleBufferedPanel();

        private GdiBitmap display = new GdiBitmap(1024, 512);

        private ProjectPSX.ProjectPSX psx;
        private int fps;
        private bool isVramViewer;

        private int horizontalRes;
        private int verticalRes;

        private int displayVRAMXStart;
        private int displayVRAMYStart;

        private bool is24BitDepth;

        private int displayX1;
        private int displayX2;
        private int displayY1;
        private int displayY2;

        private long cpuCyclesCounter;

        Dictionary<Keys, GamepadInputsEnum> _gamepadKeyMap;

        private WaveOutEvent waveOutEvent = new WaveOutEvent();
        private BufferedWaveProvider bufferedWaveProvider = new BufferedWaveProvider(new WaveFormat());

        private readonly string settingsPath = "./settings.ini";

        public Window()
        {
            InitializeComponent();
            UIHelper.EnableDoubleBuffered(this);
            AppSettings.SetIni(new IniFile(this.settingsPath));

            AddMenuItems();

            KeyUp += vramViewerToggle;
            SizeChanged += OnSizeChanged;

            screen.Size = _640x480;
            screen.Margin = new Padding(0);
            screen.MouseDoubleClick += new MouseEventHandler(toggleDebug);

            Controls.Add(screen);
            // Debug will crash if not:
            CheckForIllegalCrossThreadCalls = false;

            KeyDown += new KeyEventHandler(handleJoyPadDown);
            KeyUp += new KeyEventHandler(handleJoyPadUp);

            _gamepadKeyMap = new Dictionary<Keys, GamepadInputsEnum>() {
                { Keys.Space, GamepadInputsEnum.Space},
                { Keys.Z , GamepadInputsEnum.Z },
                { Keys.C , GamepadInputsEnum.C },
                { Keys.Enter , GamepadInputsEnum.Enter },
                { Keys.Up , GamepadInputsEnum.Up },
                { Keys.Right , GamepadInputsEnum.Right },
                { Keys.Down , GamepadInputsEnum.Down },
                { Keys.Left , GamepadInputsEnum.Left },
                { Keys.D1 , GamepadInputsEnum.D1 },
                { Keys.D3 , GamepadInputsEnum.D3 },
                { Keys.Q , GamepadInputsEnum.Q },
                { Keys.E , GamepadInputsEnum.E },
                { Keys.W , GamepadInputsEnum.W },
                { Keys.D , GamepadInputsEnum.D },
                { Keys.S , GamepadInputsEnum.S },
                { Keys.A , GamepadInputsEnum.A },
            };
        }

        /**
         * Open File / Images
         */

        private string GetDiskFilename()
        {
            var cla = Environment.GetCommandLineArgs();
            if (cla.Length != 0 ? cla[0].EndsWith(".exe") : false)
            {
                //Show the user a dialog so they can pick the bin they want to load.
                var fileDialog = new OpenFileDialog();

                fileDialog.Filter = "BIN/CUE files or PSXEXEs(*.bin, *.cue, *.exe)|*.bin;*.cue;*.exe";
                
                var result = fileDialog.ShowDialog();

                string file = fileDialog.FileName;

                if (result == DialogResult.OK)
                    Debug.WriteLine("Open File: " + file);
                return file;
            }
            else
            {
                if (cla.Any(s => s.EndsWith(".bin") || s.EndsWith(".cue") || s.EndsWith(".exe")))
                {
                    Debug.WriteLine("CMD Args: " + cla[0]);
                    string filename = cla.First(s => s.EndsWith(".bin") || s.EndsWith(".cue") || s.EndsWith(".exe"));
                    return filename;
                }
                Debug.WriteLine("FileDialog/CommandLine not available.");
                return null;
            }
        }

        /**
         * Control / KeyEvents
         */

        private void handleJoyPadUp(object sender, KeyEventArgs e)
        {
            GamepadInputsEnum? button = GetGamepadButton(e.KeyCode);
            if (button != null)
                psx.JoyPadUp(button.Value);
        }

        private GamepadInputsEnum? GetGamepadButton(Keys keyCode)
        {
            if (_gamepadKeyMap.TryGetValue(keyCode, out GamepadInputsEnum gamepadButtonValue))
                return gamepadButtonValue;
            return null;
        }

        private void handleJoyPadDown(object sender, KeyEventArgs e)
        {
            GamepadInputsEnum? button = GetGamepadButton(e.KeyCode);
            if (button != null)
                psx.JoyPadDown(button.Value);
        }

        /**
         * Debug
         */

        private void toggleDebug(object sender, MouseEventArgs e) => psx.toggleDebug();

        /**
         * Video / VRAM
         */

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Render(int[] vram)
        {
            //Console.WriteLine($"x1 {displayX1} x2 {displayX2} y1 {displayY1} y2 {displayY2}");

            int horizontalEnd = horizontalRes;
            int verticalEnd = verticalRes;

            if (isVramViewer)
            {
                horizontalEnd = 1024;
                verticalEnd = 512;

                Marshal.Copy(vram, 0, display.BitmapData, 0x80000);
            }
            else if (is24BitDepth)
            {
                blit24bpp(vram);
            }
            else
            {
                blit16bpp(vram);
            }

            fps++;

            var deviceContext = new GdiDeviceContext(screen.Handle);
            Gdi32.SetStretchBltMode(display.DeviceContext, BltMode.STRETCH_DELETESCANS);
            Gdi32.StretchBlt(deviceContext, 0, 0, screen.Width, screen.Height,
                display.DeviceContext, 0, 0, horizontalEnd, verticalEnd, RasterOp.SRCCOPY);
            deviceContext.Dispose();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe void blit24bpp(int[] vramBits)
        {
            int yRangeOffset = (240 - (displayY2 - displayY1)) >> (verticalRes == 480 ? 0 : 1);
            if (yRangeOffset < 0)
                yRangeOffset = 0;

            var display = new Span<int>(this.display.BitmapData.ToPointer(), 0x80000);
            Span<int> scanLine = stackalloc int[horizontalRes];

            for (int y = yRangeOffset; y < verticalRes - yRangeOffset; y++)
            {
                int offset = 0;
                var startXYPosition = displayVRAMXStart + ((y - yRangeOffset + displayVRAMYStart) * 1024);
                for (int x = 0; x < horizontalRes; x += 2)
                {
                    int p0rgb = vramBits[startXYPosition + offset++];
                    int p1rgb = vramBits[startXYPosition + offset++];
                    int p2rgb = vramBits[startXYPosition + offset++];

                    ushort p0bgr555 = GetPixelBGR555(p0rgb);
                    ushort p1bgr555 = GetPixelBGR555(p1rgb);
                    ushort p2bgr555 = GetPixelBGR555(p2rgb);

                    //[(G0R0][R1)(B0][B1G1)]
                    //   RG    B - R   GB

                    int p0R = p0bgr555 & 0xFF;
                    int p0G = (p0bgr555 >> 8) & 0xFF;
                    int p0B = p1bgr555 & 0xFF;
                    int p1R = (p1bgr555 >> 8) & 0xFF;
                    int p1G = p2bgr555 & 0xFF;
                    int p1B = (p2bgr555 >> 8) & 0xFF;

                    int p0rgb24bpp = p0R << 16 | p0G << 8 | p0B;
                    int p1rgb24bpp = p1R << 16 | p1G << 8 | p1B;

                    scanLine[x] = p0rgb24bpp;
                    scanLine[x + 1] = p1rgb24bpp;
                }
                scanLine.CopyTo(display.Slice(y * 1024));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe void blit16bpp(int[] vramBits)
        {
            //Console.WriteLine($"x1 {displayX1} x2 {displayX2} y1 {displayY1} y2 {displayY2}");
            //Console.WriteLine($"Display Height {display.Height}  Width {display.Width}");
            int yRangeOffset = (240 - (displayY2 - displayY1)) >> (verticalRes == 480 ? 0 : 1);
            if (yRangeOffset < 0)
                yRangeOffset = 0;

            var vram = new Span<int>(vramBits);
            var display = new Span<int>(this.display.BitmapData.ToPointer(), 0x80000);

            for (int y = yRangeOffset; y < verticalRes - yRangeOffset; y++)
            {
                var from = vram.Slice(displayVRAMXStart + ((y - yRangeOffset + displayVRAMYStart) * 1024), horizontalRes);
                var to = display.Slice(y * 1024);
                from.CopyTo(to);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ushort GetPixelBGR555(int color)
        {
            byte m = (byte)((color & 0xFF000000) >> 24);
            byte r = (byte)((color & 0x00FF0000) >> 16 + 3);
            byte g = (byte)((color & 0x0000FF00) >> 8 + 3);
            byte b = (byte)((color & 0x000000FF) >> 3);

            return (ushort)(m << 15 | b << 10 | g << 5 | r);
        }

        public int GetVPS()
        {
            int currentFps = fps;
            fps = 0;
            return currentFps;
        }

        public void SetDisplayMode(int horizontalRes, int verticalRes, bool is24BitDepth)
        {
            this.is24BitDepth = is24BitDepth;

            if (horizontalRes != this.horizontalRes || verticalRes != this.verticalRes)
            {
                this.horizontalRes = horizontalRes;
                this.verticalRes = verticalRes;

                clearDisplay();
                //Console.WriteLine($"setDisplayMode {horizontalRes} {verticalRes} {is24BitDepth}");
            }

        }

        public void SetVRAMStart(ushort displayVRAMXStart, ushort displayVRAMYStart)
        {
            this.displayVRAMXStart = displayVRAMXStart;
            this.displayVRAMYStart = displayVRAMYStart;

            //Console.WriteLine($"Vram Start {displayVRAMXStart} {displayVRAMYStart}");
        }

        public void SetVerticalRange(ushort displayY1, ushort displayY2)
        {
            this.displayY1 = displayY1;
            this.displayY2 = displayY2;

            //Console.WriteLine($"Vertical Range {displayY1} {displayY2}");
        }

        public void SetHorizontalRange(ushort displayX1, ushort displayX2)
        {
            this.displayX1 = displayX1;
            this.displayX2 = displayX2;

            //Console.WriteLine($"Horizontal Range {displayX1} {displayX2}");
        }

        private void vramViewerToggle(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (!isVramViewer)
                    screen.Size = vramSize;
                else
                    screen.Size = _640x480;
                isVramViewer = !isVramViewer;
                clearDisplay();
            }
        }

        private unsafe void clearDisplay()
        {
            Span<uint> span = new Span<uint>(display.BitmapData.ToPointer(), 0x80000);
            span.Clear();
        }

        /**
         * Audio
         */

        public void Play(byte[] samples)
        {
            if (CommonSettings.EnableSound)
            {
                bufferedWaveProvider.AddSamples(samples, 0, samples.Length);

                if (waveOutEvent.PlaybackState != PlaybackState.Playing)
                    waveOutEvent.Play();
            }
            else
            {
                if (waveOutEvent.PlaybackState != PlaybackState.Stopped)
                    waveOutEvent.Stop();
            }
        }

        /**
         * Emulation Core / Run
         */

        private void RunPSX()
        {
            string diskFilename = GetDiskFilename();
            if (string.IsNullOrWhiteSpace(diskFilename))
                return;

            var loaded = new System.Timers.Timer(1000);
            loaded.Elapsed += OnLoaded;
            loaded.AutoReset = false;
            loaded.Start();

            bufferedWaveProvider.DiscardOnBufferOverflow = true;
            bufferedWaveProvider.BufferDuration = new TimeSpan(0, 0, 0, 0, 300);
            waveOutEvent.Init(bufferedWaveProvider);

            psx = new ProjectPSX.ProjectPSX(this, diskFilename);

            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;

            RunUncapped();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Text = $"PSX4Any | cpu: { (int)((float)cpuCyclesCounter / (PSX_MHZ / MIPS_UNDERCLOCK) * SYNC_CYCLES) }% | vps: { GetVPS() }";
            cpuCyclesCounter = 0;
        }

        public void RunUncapped()
        {
            Task t = Task.Factory.StartNew(EXECUTE, TaskCreationOptions.LongRunning);
        }

        private void EXECUTE()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            try
            {
                while (psx != null)
                {
                    psx.RunFrame();
                    cpuCyclesCounter += cycles;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void OnLoaded(object sender, ElapsedEventArgs e)
        {
            Activate();
            Debug.WriteLine("Load Complete.");
        }

        /**
         * Fix
         */

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            psx = null;
            Application.Exit();
            Debug.WriteLine("Closed.");
        }

        /**
         * for Main Form/Window
         */

        private void OnSizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                if (this.Width > screen.Width + 16 && this.Height > screen.Height + 16)
                {
                    screen.Location = new Point(
                        this.Width / 2 - screen.Width / 2,
                        this.Height / 2 - screen.Height / 2);
                }
            }
            else
            {
                if (this.WindowState == FormWindowState.Minimized)
                    return;
                screen.Location = Point.Empty;
            }
        }

        private MenuStrip TopMenu;
        private ToolStripMenuItem File_MenuItem;
        private ToolStripMenuItem Option_MenuItem;
        private ToolStripMenuItem Help_MenuItem;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.TopMenu = new System.Windows.Forms.MenuStrip();
            this.File_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Option_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Help_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TopMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopMenu
            // 
            this.TopMenu.Font = new System.Drawing.Font("Bahnschrift SemiLight", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopMenu.GripMargin = new System.Windows.Forms.Padding(0);
            this.TopMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_MenuItem,
            this.Option_MenuItem,
            this.Help_MenuItem});
            this.TopMenu.Location = new System.Drawing.Point(0, 0);
            this.TopMenu.Name = "TopMenu";
            this.TopMenu.Padding = new System.Windows.Forms.Padding(0);
            this.TopMenu.Size = new System.Drawing.Size(622, 30);
            this.TopMenu.TabIndex = 0;
            this.TopMenu.Text = "TopMenu";
            // 
            // File_MenuItem
            // 
            this.File_MenuItem.Name = "File_MenuItem";
            this.File_MenuItem.Size = new System.Drawing.Size(52, 30);
            this.File_MenuItem.Text = "File";
            // 
            // Option_MenuItem
            // 
            this.Option_MenuItem.Name = "Option_MenuItem";
            this.Option_MenuItem.Size = new System.Drawing.Size(72, 30);
            this.Option_MenuItem.Text = "Option";
            // 
            // Help_MenuItem
            // 
            this.Help_MenuItem.Name = "Help_MenuItem";
            this.Help_MenuItem.Size = new System.Drawing.Size(59, 30);
            this.Help_MenuItem.Text = "Help";
            // 
            // Window
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(622, 433);
            this.Controls.Add(this.TopMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.TopMenu;
            this.Name = "Window";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PSX4Any";
            this.TopMenu.ResumeLayout(false);
            this.TopMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void AddMenuItems()
        {
            CreateMenuItem("Open", this.File_MenuItem, this.TopMenu);
            CreateMenuItem("Exit", this.File_MenuItem, this.TopMenu);
            CreateMenuItem("Settings", this.Option_MenuItem, this.TopMenu);
            CreateMenuItem("AboutApp", this.Help_MenuItem, this.TopMenu);
        }

        private bool CreateMenuItem(string name, ToolStripMenuItem category, MenuStrip source)
        {
            if ((category == null || source == null) || (source != null && source.Items.Count == 0))
                return false;
            var menuItem = new ToolStripMenuItem
            {
                Name = name + "_MenuItem",
                Text = name
            };
            menuItem.Click += MenuItem_Click;

            ToolStripMenuItem temp = null;
            for (int i = 0; i < source.Items.Count; ++i)
            {
                ToolStripMenuItem parent = (ToolStripMenuItem)source.Items[i];
                if (parent != category)
                    continue;
                temp = parent;
            }
            temp.DropDownItems.Add(menuItem);

            return true;
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem))
                return;
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            switch (item.Text)
            {
                case "Open":
                    RunPSX();
                    break;
                case "Exit":
                    Application.Exit();
                    break;

                case "Settings":
                    new SettingsWindow().Show();
                    break;

                case "AboutApp":
                    new AboutAppWindow().Show();
                    break;
            }
        }

    }

}

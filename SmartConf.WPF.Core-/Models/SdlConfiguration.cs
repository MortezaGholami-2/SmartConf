using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartConf.WPF.Core.Models
{
    public class SdlConfiguration
    {
        private List<ConfigurationItem> _configurationItems;
        public SdlConfiguration(List<ConfigurationItem> configurationItems)
        {
            _configurationItems = configurationItems;
        }

        private bool fullScreen;
        public bool FullScreen
        {
            get
            {
                ConfigurationItem? item = _configurationItems.Find(x => x.Name == "fullscreen");

                if (item is not null && item.Value is not null)
                {
                    fullScreen = bool.Parse(item.Value);
                }
                return fullScreen;
            }

            set { fullScreen = value; }
        }

        private bool fullScreenDoubleBuffering;
        public bool FullScreenDoubleBuffering
        {
            get
            {
                ConfigurationItem? item = _configurationItems.Find(x => x.Name == "fulldouble");

                if (item is not null && item.Value is not null)
                {
                    fullScreenDoubleBuffering = bool.Parse(item.Value);
                }
                return fullScreenDoubleBuffering;
            }

            set { fullScreenDoubleBuffering = value; }
        }

        private FullResolutionEnum fullResolution;
        public FullResolutionEnum FullResolution
        {
            get
            {
                ConfigurationItem? item = _configurationItems.Find(x => x.Name == "fullresolution");

                if (item is not null && item.Value is not null)
                {
                    switch (item.Value)
                    {
                        case "original":
                            fullResolution = FullResolutionEnum.Original;
                            break;

                        case "desktop":
                            fullResolution = FullResolutionEnum.Desktop;
                            break;
                        //default:
                        //    break;
                    }
                    //fullResolution = Enum.GetName( bool.Parse(item.Value);
                }
                return fullResolution;
            }

            set { fullResolution = value; }
        }

        //public string? FullResolutionFixedSize { get; set; }
        //public string WindowResolution { get; set; } = "original";
        //public bool WindowPosition { get; set; }
        //public bool Display { get; set; }
        public VideoSystemOutputEnum VideoSystemOutPut { get; set; }
        //public bool VideoDriver { get; set; }
        //public bool Transparency { get; set; }
        //public bool Maximize { get; set; }
        //public bool Autolock { get; set; }
        //public bool AutolockFeedback { get; set; }
        //public bool MiddleUnlock { get; set; }
        //public bool ClipMouseButton { get; set; }
        //public bool ClipKeyModifier { get; set; }
        //public bool ClipPasteBios { get; set; }
        //public bool ClipPasteSpeed { get; set; }
        //public bool Sensitivity { get; set; }
        //public bool UseSystemCursor { get; set; }
        //public bool MouseEmulation { get; set; }
        //public bool MouseWheelKey { get; set; }
        //public bool WaitOnError { get; set; }
        //public bool Priority { get; set; }
        //public bool MapperFile { get; set; }
        //public bool MapperFileSdl1 { get; set; }
        //public bool MapperFileSdl2 { get; set; }
        //public bool ForceSquareCorner { get; set; }
        //public bool UseScanCodes { get; set; }
        //public bool OverScan { get; set; }

        private string titlebar;
        public string Titlebar
        {
            get
            {
                ConfigurationItem? item = _configurationItems.Find(x => x.Name == "titlebar");

                if (item is not null && item.Value is not null)
                {
                    fullScreen = bool.Parse(item.Value);
                }
                return titlebar;
            }

            set { titlebar = value; }
        }

        private bool showBasicInformation;
        public bool ShowBasicInformation
        {
            get
            {
                ConfigurationItem? item = _configurationItems.Find(x => x.Name == "showbasic");

                if (item is not null && item.Value is not null)
                {
                    fullScreen = bool.Parse(item.Value);
                }
                return showBasicInformation;
            }
            
            set { showBasicInformation = value; }
        }

        private bool showDetails;
        public bool ShowDetails
        {
            get
            {
                ConfigurationItem? item = _configurationItems.Find(x => x.Name == "showdetails");

                if (item is not null && item.Value is not null)
                {
                    fullScreen = bool.Parse(item.Value);
                }
                return showDetails;
            }

            set { showDetails = value; }
        }

        private bool showMenu;
        public bool ShowMenu
        {
            get
            {
                ConfigurationItem? item = _configurationItems.Find(x => x.Name == "showmenu");

                if (item is not null && item.Value is not null)
                {
                    fullScreen = bool.Parse(item.Value);
                }
                return showMenu;
            }

            set { showMenu = value; }
        }
    }

    public enum FullResolutionEnum
    {
        Original,
        Desktop,
        FixedSize
    }
    public enum VideoSystemOutputEnum
    {
        Default,
        Surface,
        Overlay,
        Ttf,
        Opengl,
        Openglnb,
        openglhq,
        openglpp,
        ddraw,
        direct3d
    }
}

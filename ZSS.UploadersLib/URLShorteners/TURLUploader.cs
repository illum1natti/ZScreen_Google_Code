﻿#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
    
    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Collections.Generic;
using UploadersLib.Helpers;

namespace UploadersLib.URLShorteners
{
    [Serializable]
    public sealed class TURLUploader : TextUploader
    {
        public static readonly string Hostname = UrlShortenerType.TURL.GetDescription();

        public override object Settings
        {
            get
            {
                return (object)HostSettings;
            }
            set
            {
                HostSettings = (TURLSettings)value;
            }
        }

        public TURLSettings HostSettings = new TURLSettings();

        public TURLUploader()
        {
            HostSettings.URL = "http://turl.ca/api.php";
        }

        public override string ToString()
        {
            return HostSettings.Name;
        }

        public override string UploadText(TextInfo text)
        {
            if (!string.IsNullOrEmpty(text.LocalString))
            {
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                arguments.Add("url", text.LocalString);
                arguments.Add("tag", HostSettings.Tag);

                return GetResponseString(HostSettings.URL, arguments);
            }

            return string.Empty;
        }

        [Serializable]
        public class TURLSettings : TextUploaderSettings
        {
            public override string Name { get; set; }
            public override string URL { get; set; }
            public string Tag { get; set; }

            public TURLSettings()
            {
                Name = Hostname;
            }
        }
    }
}
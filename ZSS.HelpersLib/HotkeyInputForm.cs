﻿#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2008-2011 ZScreen Developers

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

#endregion License Information (GPL v2)

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HelpersLib
{
    public partial class HotkeyInputForm : Form
    {
        public Keys SelectedKey
        {
            get
            {
                return GetKey();
            }
            set
            {
                SetKey(value);
            }
        }

        private List<KeyInfo> keys;

        public HotkeyInputForm()
        {
            InitializeComponent();

            AddKeys();
            ResetKey();
        }

        private void AddKeys()
        {
            // None, PrintScreen, A...Z, 0...9, F1...F12

            keys = new List<KeyInfo>();

            keys.Add(new KeyInfo(Keys.None));

            keys.Add(new KeyInfo(Keys.PrintScreen, "Print Screen"));

            for (Keys key = Keys.A; key <= Keys.Z; key++)
            {
                keys.Add(new KeyInfo(key));
            }

            for (Keys key = Keys.D0; key <= Keys.D9; key++)
            {
                keys.Add(new KeyInfo(key, (key - Keys.D0).ToString()));
            }

            for (Keys key = Keys.F1; key <= Keys.F12; key++)
            {
                keys.Add(new KeyInfo(key));
            }

            foreach (KeyInfo key in keys)
            {
                cbKeys.Items.Add(key);
            }
        }

        private void ResetKey()
        {
            cbControl.Checked = false;
            cbShift.Checked = false;
            cbAlt.Checked = false;
            cbKeys.SelectedIndex = 0;
        }

        private Keys GetKey()
        {
            Keys key = Keys.None;

            if (cbControl.Checked) key |= Keys.Control;
            if (cbShift.Checked) key |= Keys.Shift;
            if (cbAlt.Checked) key |= Keys.Alt;

            if (cbKeys.SelectedIndex >= 0)
            {
                key |= keys[cbKeys.SelectedIndex].Key;
            }

            return key;
        }

        private void SetKey(Keys key)
        {
            cbControl.Checked = (key & Keys.Control) == Keys.Control;
            cbShift.Checked = (key & Keys.Shift) == Keys.Shift;
            cbAlt.Checked = (key & Keys.Alt) == Keys.Alt;

            Keys vk = key & ~Keys.Modifiers;

            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i].Key == vk)
                {
                    cbKeys.SelectedIndex = i;
                    return;
                }
            }

            cbKeys.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetKey();
        }
    }
}
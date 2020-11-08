using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HDD_Freespace_Status_Design_1
{
    class Storage
    {
        static public void StorageDrive(string Drive, Label LabelText)
        {
            DriveInfo dDrive = new DriveInfo(Drive);

            if (dDrive.IsReady)
            {
                double freeSpacePerc = (dDrive.AvailableFreeSpace / (float)dDrive.TotalSize) * 100;
                LabelText.Text = String.Format("{0:0.00}%", freeSpacePerc);
            }
            else
            {
                LabelText.Text = "NA";
            }
        }
    }
}

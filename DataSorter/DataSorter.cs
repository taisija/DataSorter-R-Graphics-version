using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataSorter
{
    public partial class DataSorter : Form
    {
        public DataSorter()
        {
            InitializeComponent();
        }

        private void buttonLoadData_Click(object sender, EventArgs e)
        {
            DataLoader DL;
            DataWriter DW;
            ImageParameterization IP;
            int ImagesNum = (int)numericUpDownImNum.Value;
            int matricesNum = 16;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                DL = new DataLoader(folderBrowserDialog.SelectedPath, folderBrowserDialog.SelectedPath);
                DL.ReadSourceParametersForAllCombinetions(ImagesNum, 6);
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    int[,] m = new int[2, 16];
                    m[0, 0] = 0;
                    m[1, 0] = 5;
                    m[0, 1] = 1;
                    m[1, 1] = 5;
                    m[0, 2] = 5;
                    m[1, 2] = 5;
                    m[0, 3] = 4;
                    m[1, 3] = 4;
                    m[0, 4] = 1;
                    m[1, 4] = 1;
                    m[0, 5] = 2;
                    m[1, 5] = 2;
                    m[0, 6] = 1;
                    m[1, 6] = 2;
                    m[0, 7] = 1;
                    m[1, 7] = 3;
                    m[0, 8] = 0;
                    m[1, 8] = -1;
                    m[0, 9] = 0;
                    m[1, 9] = -2;
                    m[0, 10] = 0;
                    m[1, 10] = -3;
                    m[0, 11] = 0;
                    m[1, 11] = -4;
                    m[0, 12] = 1;
                    m[1, 12] = -1;
                    m[0, 13] = 2;
                    m[1, 13] = -2;
                    m[0, 14] = 1;
                    m[1, 14] = -2;
                    m[0, 15] = 1;
                    m[1, 15] = -3; 
                    IP = new ImageParameterization(ImagesNum, 0, 5 + 8 * matricesNum, true);
                    IP.LoadImages("");
                    IP.Calculate_mN(2);
                    IP.Calculate_mN(3);
                    IP.Calculate_mN(4);
                    IP.CalculateDivergence();
                    IP.ParamFromCOoccurenceMatrix(m, matricesNum);
                    //IP.AddResortDataAsPercentage(DL.LoadingData, DL.DataLen, DL.ObservationNum);
                    double[][] newData = new double[DL.DataLen.Length][];
                    string[][] newDataNames = new string[1][];
                    newDataNames[0] = new string[IP.ParamCounter];
                    for (int j = 0; j < (IP.ParamCounter); j++)
                    {
                        newDataNames[0][j] = IP.TestImageResultsNames[0][j];
                    }
                        DW = new DataWriter(saveFileDialog.FileName);
                        DW.WriteData(newData, newDataNames);
                        DW = new DataWriter(saveFileDialog.FileName);
                        DW.WriteData(IP.TestImageResults);
                        DW = new DataWriter(saveFileDialog.FileName+".txt");                        
                        DW.WriteData(IP.Correlation(), 5 + 8 * matricesNum);
                        DW = new DataWriter(saveFileDialog.FileName + "Thresh.txt");
                        DW.WriteData(IP.CorrelationT(0.9), 5 + 8 * matricesNum);
                }
            }
        }

        private void resortButton_Click(object sender, EventArgs e)
        {
            DataLoader DL;
            DataWriter DW;
            ImageParameterization IP;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                 DL = new DataLoader(folderBrowserDialog.SelectedPath, folderBrowserDialog.SelectedPath);
                 DL.ReadSourceParametersForAllCombinetions(50, 6);
                 if (saveFileDialog.ShowDialog() == DialogResult.OK)
                 {
                     DW = new DataWriter(saveFileDialog.FileName);
                     DW.WriteData(DL.LoadingData, DL.DataLen, DL.ObservationNum);
                 }
            }
        }
    }
}

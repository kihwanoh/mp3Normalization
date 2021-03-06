﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mp3Normalization
{
    public partial class Form1 : Form
    {

        private const double FIVELOG10TWO = 1.50514997831991;

        // Define static variables shared by class methods.
        private static StringBuilder Output = null;
        private static int numOutputLines = 0;
        private enum enmRcvState { idle = 0, found, NA, value };
        private enmRcvState rcvState = enmRcvState.idle;
        private string ModifydBGain = "";

        // member Table Query class
        private MemberQuery gMemberQuery = new MemberQuery();

        public Form1()
        {
            InitializeComponent();

            this.Size = Properties.Settings.Default.Size;
            //this.Location = Properties.Settings.Default.Location;
            this.textGain.Text = Properties.Settings.Default.TargetGain;
            this.chkConverFilename.CheckState = Properties.Settings.Default.FileConversion;
            this.chkUsingTempfile.CheckState = Properties.Settings.Default.UseTempFile;
            this.chkDisableMp3gain.CheckState = Properties.Settings.Default.DisableMp3gain;
            this.textTempDir.Text = Properties.Settings.Default.TempDirectory;

            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            //listView1.CheckBoxes = true; // 

            listView1.LabelEdit = true;
            listView1.Columns.Add("File name", 100);
            listView1.Columns.Add("Volume", 20);
            listView1.Columns.Add("Track Gain", 50);
            listView1.Columns.Add("File name Alias", 100);

            listView1.Columns[0].Width = -2;
            listView1.Columns[1].Width = -2;
            listView1.Columns[2].Width = -2;
            listView1.Columns[3].Width = -2;
            //listView1.Columns.Add("d", 50, HorizontalAlignment.Center);

            Output = new StringBuilder("");
            gMemberQuery.Connection();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Size = this.Size;
            //Properties.Settings.Default.Location = this.Location;
            Properties.Settings.Default.TargetGain = this.textGain.Text;
            Properties.Settings.Default.FileConversion = this.chkConverFilename.CheckState;
            Properties.Settings.Default.UseTempFile = this.chkUsingTempfile.CheckState;
            Properties.Settings.Default.DisableMp3gain = this.chkDisableMp3gain.CheckState;
            Properties.Settings.Default.TempDirectory = this.textTempDir.Text;
            Properties.Settings.Default.Save();

            gMemberQuery.Diconnect();
        }

        private void OutputHandler(object sendingProcess,
            DataReceivedEventArgs outLine)
        {
            char[] delimiterChars = { '\t', '\r' };

            // Collect the sort command output.
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                numOutputLines++;

                // Add the text to the collected output.
                //Output.Append(Environment.NewLine +
                Output.Append("[" + numOutputLines.ToString() + "] - " + outLine.Data);

#if true    //
                string[] strArray = outLine.Data.Split(delimiterChars);
                if (rcvState == enmRcvState.found)
                {
                    if (strArray[1] == "NA")
                        rcvState = enmRcvState.NA;
                    if (strArray[1] != "NA")
                    {
                        rcvState = enmRcvState.value;
                        ModifydBGain = strArray[2]; //ModifydBGain
                        Debug.WriteLine(strArray[2]);
                    }
                } else if (rcvState == enmRcvState.idle && strArray[0] == "File")
                        rcvState = enmRcvState.found;
#endif
            }
        }

        /// <summary>
        /// Get the stderr/stdout outputs of a process and return when the process is done.
        /// Both <b>must</b> be read or the process will block on windows.
        /// </summary>
        /// <param name="process">The process to get the ouput from</param>
        /// <param name="errorOutput">The array to store the stderr output. cannot be null.</param>
        /// <param name="stdOutput">The array to store the stdout output. cannot be null.</param>
        /// <param name="waitforReaders">if true, this will wait for the reader threads.</param>
        /// <returns>the process return code.</returns>
        private int GrabProcessOutput(Process process, List<String> errorOutput, List<String> stdOutput, bool waitforReaders)
        {
            if (errorOutput == null)
            {
                throw new ArgumentNullException("errorOutput");
            }
            if (stdOutput == null)
            {
                throw new ArgumentNullException("stdOutput");
            }
            // read the lines as they come. if null is returned, it's
            // because the process finished
            Thread t1 = new Thread(new ThreadStart(delegate {
                // create a buffer to read the stdoutput
                try
                {
                    using (StreamReader sr = process.StandardError)
                    {
                        while (!sr.EndOfStream)
                        {
                            String line = sr.ReadLine();
                            if (!String.IsNullOrEmpty(line))
                            {
                                Debug.Print(line);
                                errorOutput.Add(line);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // do nothing.
                }
            }));

            Thread t2 = new Thread(new ThreadStart(delegate {
                // create a buffer to read the std output
                try
                {
                    using (StreamReader sr = process.StandardOutput)
                    {
                        while (!sr.EndOfStream)
                        {
                            String line = sr.ReadLine();
                            if (!String.IsNullOrEmpty(line))
                            {
                                numOutputLines++;

                                stdOutput.Add(line);
                                Debug.Print(line);
#if true    //
                                char[] delimiterChars = { '\t', '\r' };
                                string[] strArray = line.Split(delimiterChars);
                                if (rcvState == enmRcvState.found)
                                {
                                    if (strArray[1] == "NA")
                                        rcvState = enmRcvState.NA;
                                    if (strArray[1] != "NA")
                                    {
                                        rcvState = enmRcvState.value;
                                        ModifydBGain = strArray[2]; //ModifydBGain
                                        Debug.WriteLine(strArray[2]);
                                    }
                                }
                                else if (rcvState == enmRcvState.idle && strArray[0] == "File")
                                    rcvState = enmRcvState.found;
#endif
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // do nothing.
                }
            }));

            t1.Start();
            t2.Start();

            // it looks like on windows process#waitFor() can return
            // before the thread have filled the arrays, so we wait for both threads and the
            // process itself.
            if (waitforReaders)
            {
                try
                {
                    t1.Join();
                }
                catch (ThreadInterruptedException)
                {
                }
                try
                {
                    t2.Join();
                }
                catch (ThreadInterruptedException)
                {
                }
            }

            // get the return code from the process
            process.WaitForExit();
            return process.ExitCode;
        }


        private void checkMp3(string fname, string args, bool async = true)
        {
            rcvState = enmRcvState.idle;
            numOutputLines = 0;
            if (chkUsingTempfile.Checked)
            {
                if (File.Exists("tmp.mp3")) File.Delete("tmp.mp3");
                File.Move(fname, "tmp.mp3");
                //File.Copy(fname, "tmp.mp3");
            }


#if true
            int status = -1;

            try
            {
                String command = "\\mp3gain.exe";
                Debug.Print(String.Format("Launching '{0}'", Application.StartupPath + command));
                ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + command);
                if (chkUsingTempfile.Checked)
                    psi.Arguments = " " + args + " " + "\"" + "tmp.mp3" + "\"" + "";
                else
                    psi.Arguments = " " + args + " " + "\"" + fname + "\"" + "";
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.UseShellExecute = false;
                psi.RedirectStandardError = true;
                psi.RedirectStandardOutput = true;

                using (Process proc = Process.Start(psi))
                {
                    List<String> errorOutput = new List<String>();
                    List<String> stdOutput = new List<String>();
                    status = GrabProcessOutput(proc, errorOutput, stdOutput, false /* waitForReaders */);
                }
            }
            catch (IOException ioe)
            {
                Debug.Print("Unable to run: {0}", ioe.Message);
            }
            catch (ThreadInterruptedException ie)
            {
                Debug.Print("Unable to run: {0}", ie.Message);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }
#else
            rcvState = enmRcvState.idle;

            Process pp = new Process();

            pp.StartInfo.Arguments = " " + args + " " + "\"" + fname + "\"" + "";
            if (chkUsingTempfile.Checked)
            {
                if (File.Exists("tmp.mp3")) File.Delete("tmp.mp3");
                File.Move(fname, "tmp.mp3");
                //File.Copy(fname, "tmp.mp3");

                pp.StartInfo.Arguments = " " + args + " " + "\"" + "tmp.mp3" + "\"" + "";
            }
            pp.StartInfo.FileName = Application.StartupPath + "\\mp3gain.exe";
            //pp.StartInfo.Arguments = "/r /d 20 /k /c " + "tmp.mp3" + "";
            //pp.StartInfo.Arguments = " " + args + " " + "\"" + fname + "\"" + "";
            //pp.StartInfo.Arguments = " " + args + " " + "\"" + "tmp.mp3" + "\"" + "";
            Debug.WriteLine(pp.StartInfo.FileName + pp.StartInfo.Arguments);

            pp.StartInfo.UseShellExecute = false;
            if (async)
                pp.StartInfo.RedirectStandardOutput = true;
            pp.StartInfo.CreateNoWindow = true;

            pp.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            pp.Start();
            if(async)
                pp.BeginOutputReadLine();
            pp.WaitForExit();
            pp.Close();
            Debug.WriteLine(Output.ToString());
            textView.Text = Output.ToString();
            Output.Clear();
#endif

            if (chkUsingTempfile.Checked)
            {
                File.Move("tmp.mp3", fname);
            }
            progressConvert.PerformStep();
        }

        private void singleFileProcessing(string fname, bool boolSetGain)
        {
            textFilePath.Text = fname;
            if (chkDisableMp3gain.Checked)
                return;

            // mp3gain option
            //
            // /g <i>  - apply gain i to mp3 without doing any analysis
            // /p - Preserve original file timestamp
            // /o - output is a database-friendly tab-delimited list
            // /s c - only check stored tag info (no other processing)
            // /s r - force re-calculation (do not read tag info)
            checkMp3(fname, " /o /s c ");
            if (rcvState == enmRcvState.NA)
            {
                checkMp3(fname, " /p /o /s r ", false);    // Standardoutput redirection disable
                checkMp3(fname, " /o /s c ");
            }

            if (boolSetGain)
            {
                double dblRadioMp3Gain =
                    (double.Parse(textGain.Text) - 89.0 + Math.Round(double.Parse(ModifydBGain), 1)) / FIVELOG10TWO;
                string intRadioGain = String.Format("{0:F0}", dblRadioMp3Gain);

                // change volume with gain
                checkMp3(fname, " /p /g " + intRadioGain, false);
                //TXTLog(fname + " " + intRadioGain);
            }
        }

        private void mp3Normalization(string[] FileNames, bool boolSetGain = true)
        {

            // set up progressbar parameter
            progressConvert.Style = ProgressBarStyle.Continuous;
            progressConvert.Minimum = 0;
            progressConvert.Step = 1;
            progressConvert.Value = 0;
            progressConvert.Maximum = FileNames.Length;

            int intSeed = 0;
            foreach (string fname in FileNames)
            {
                string fileName = Path.GetFileName(fname);
                string[] fileNameArray = fileName.Split('.');
                string filePath = Path.GetDirectoryName(fname);

                // set up location to store converted filename
                if (textTempDir.Text != null)
                    filePath = textTempDir.Text;
                Debug.WriteLine(fileName);

                DateTime dtNow = DateTime.Now;
                string strDate = dtNow.ToString("yyyy-MM-dd");
                string strPath = string.Format("{0}\\{1}-{2,4:D4}.{3}", filePath, strDate, intSeed++, fileNameArray.Last());

                if (chkConverFilename.Checked)
                {
                    // need filename conversion because mp3gainGUI can't handle unicode filename
                    while (File.Exists(strPath))
                    {
                        // increase file index in case of existing old one already
                        strPath = string.Format("{0}\\{1}-{2,4:D4}.{3}", filePath, strDate, intSeed++, fileNameArray.Last());
                    }
                    File.Move(fname, strPath);

                    singleFileProcessing(strPath, boolSetGain);
                }
                else
                {
                    singleFileProcessing(fname, boolSetGain);
                }

                // update listbox with converted information
                if (ModifydBGain == "") ModifydBGain = "0.0";

                string strRadioGain = string.Format("{0:F1}", 89.0 - Math.Round(double.Parse(ModifydBGain), 1));
                string strTrackGain = string.Format("{0:F1}", double.Parse(textGain.Text) - 89.0 + Math.Round(double.Parse(ModifydBGain), 1));
                if (chkDisableMp3gain.Checked)
                {
                    strRadioGain = "";
                    strTrackGain = "";
                }
                string[] aa = { fname, strRadioGain, strTrackGain, chkConverFilename.Checked ? strPath : "" };

                ListViewItem newitem = new ListViewItem(aa);
                listView1.Items.Add(newitem);
                listView1.Columns[0].Width = -1;
                listView1.Columns[3].Width = -1;
                listView1.Columns[1].TextAlign = HorizontalAlignment.Center;
                listView1.Columns[2].TextAlign = HorizontalAlignment.Center;
                TXTLog(string.Format("{0} {1} {2}", aa[0], aa[1], aa[2]));

                // update progressbar
                progressConvert.PerformStep();
            }

            // save listfile
            //if (MessageBox.Show("Complete the work! save the list?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string filePath = Path.GetDirectoryName(listView1.Items[0].Text);
                DateTime dtNow = DateTime.Now;
                string strDate = dtNow.ToString("yyyy-MM-dd");
                //string strPath = string.Format("{0}\\{1}{2}", AppDomain.CurrentDomain.BaseDirectory, strDate, ".lst");
                string strPath = string.Format("{0}\\{1}{2}", filePath, strDate, ".lst");

                if (File.Exists(strPath)) File.Delete(strPath);
                SaveListviewData(strPath);
            }
        }

        private void buttonAddFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "Mp3 Files (.mp3)|*.mp3|All Files (*.*)|*.*";
            //ofd.Filter = "Mp3 Files (.mp3;.m4a)|*.mp3;*.m4a|All Files (*.*)|*.*";
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                listView1.Items.Clear();
                mp3Normalization(ofd.FileNames, false);
            }
        }

        private void buttonLoadConvert_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "Mp3 Files (.mp3)|*.mp3|All Files (*.*)|*.*";
            //ofd.Filter = "Mp3 Files (.mp3;.m4a)|*.mp3;*.m4a|All Files (*.*)|*.*";
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                listView1.Items.Clear();
                mp3Normalization(ofd.FileNames, true);
            }
        }
        private void buttonTempDir_Click(object sender, EventArgs e)
        {
            textTempDir.Text = getFolder();
        }

        private void buttonAddFolders_Click(object sender, EventArgs e)
        {
            string[] FileNames = Directory.GetFiles(getFolder(), "*.mp3", SearchOption.AllDirectories);

            if (0 < FileNames.Length)
            {
                listView1.Items.Clear();
                mp3Normalization(FileNames, false);
            }
        }

        private void buttonSaveList_Click(object sender, EventArgs e)
        {
            string filePath = Path.GetDirectoryName(listView1.Items[0].Text);
            DateTime dtNow = DateTime.Now;
            string strDate = dtNow.ToString("yyyy-MM-dd");
            //string strPath = string.Format("{0}\\{1}{2}", AppDomain.CurrentDomain.BaseDirectory, strDate, ".lst");
            string strPath = string.Format("{0}\\{1}{2}", filePath, strDate, ".lst");

            if (File.Exists(strPath)) File.Delete(strPath);
            SaveListviewData(strPath);
        }

        private void buttonLoadList_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "List Files (.lst)|*.lst|All Files (*.*)|*.*";
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                listView1.Items.Clear();
                LoadListviewData(ofd.FileName);
            }
        }

        private string getFolder()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
                if (textTempDir.Text != null)
                    fbd.SelectedPath = textTempDir.Text;
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                    //string[] files = Directory.GetFiles(fbd.SelectedPath);
                }
            }

            return null;
        }

        private long CountLinesInFile(string f)
        {
            long count = 0;
            using (StreamReader r = new StreamReader(f))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    count++;
                }
            }
            return count;
        }

        private void SaveListviewData(string fileName)
        {
            // StreamWriter를 이용하여 문자작성기를 생성합니다.
            using (TextWriter tWriter = new StreamWriter(fileName))
            {
                // ListView의 Item을 하나씩 가져와서..
                foreach (ListViewItem item in listView1.Items)
                {
                    //item.Text.Replace(";", " ");    // replace special character in file name
                    // 원하는 형태의 문자열로 한줄씩 기록합니다.
                    tWriter.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}"
                        , item.Text, item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text));
                }
            }
        }

        private void LoadListviewData(string fileName)
        {
            // StreamReader를 이용하여 문자판독기를 생성합니다.
            using (TextReader tReader = new StreamReader(fileName))
            {
                // 파일의 내용을 모두 읽어와 줄바꿈을 기준으로 배열형태로 쪼갭니다.
                string[] stringLines
                    = tReader.ReadToEnd().Replace("\n", "").Split((char)Keys.Enter);

                // 최대,최소,간격을 임의로 조정
                progressConvert.Style = ProgressBarStyle.Continuous;
                progressConvert.Minimum = 0;
                progressConvert.Step = 1;
                progressConvert.Value = 0;
                progressConvert.Maximum = (int)CountLinesInFile(fileName);

                // 한줄씩 가져와서..
                foreach (string stringLine in stringLines)
                {
                    // 빈 문자열이 아니면..
                    if (stringLine != string.Empty)
                    {
                        // 구분자를 이용해서 배열형태로 쪼갭니다.
                        //string[] stringArray = stringLine.Split(';');
                        string[] stringArray = stringLine.Split('\t');

                        // 아이템을 구성합니다.
                        ListViewItem item = new ListViewItem(stringArray);
                        //item.SubItems.Add(stringArray[1]);
                        //item.SubItems.Add(stringArray[2]);

                        // ListView에 아이템을 추가합니다.
                        listView1.Items.Add(item);
                        listView1.Columns[0].Width = -1;
                        listView1.Columns[3].Width = -1;
                        listView1.Columns[1].TextAlign = HorizontalAlignment.Center;
                        listView1.Columns[2].TextAlign = HorizontalAlignment.Center;

                        if (File.Exists(stringArray[3]))
                        {
                            Stopwatch sw = new Stopwatch();
                            sw.Start();
                            File.Move(stringArray[3], stringArray[0]);
                            sw.Stop();
                            Debug.WriteLine("Load&Renamem: " + sw.ElapsedMilliseconds.ToString() + "ms " +
                                stringArray[3] + " --> " + stringArray[0]);
                        }
                    }

                    progressConvert.PerformStep();
                }
            }
        }

        public void TXTLog(String strMsg)
        {
            try
            {
                string m_strLogPrefix = AppDomain.CurrentDomain.BaseDirectory + @"LOG\";
                string m_strLogExt = @".LOG";
                DateTime dtNow = DateTime.Now;
                string strDate = dtNow.ToString("yyyy-MM-dd");
                string strPath = string.Format("{0}{1}{2}", m_strLogPrefix, strDate, m_strLogExt);
                string strDir = Path.GetDirectoryName(strPath);
                DirectoryInfo diDir = new DirectoryInfo(strDir);

                if (!diDir.Exists)
                {
                    diDir.Create();
                    diDir = new DirectoryInfo(strDir);
                }

                if (diDir.Exists)
                {
                    StreamWriter swStream = File.AppendText(strPath);
                    string strLog = String.Format("{0}: {1}", dtNow.ToString(dtNow.Hour + "시mm분ss초"), strMsg);
                    swStream.WriteLine(strLog);
                    swStream.Close();
                }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}

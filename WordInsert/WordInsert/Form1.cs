using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MSWord = Microsoft.Office.Interop.Word;
using System.Reflection;

namespace WordInsert
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog dialog;
        const string STRING_INSERT = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        static Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            dialog = new FolderBrowserDialog();
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            openFolder("初始", tbSource);
        }


        private void btnTarget_Click(object sender, EventArgs e)
        {
            openFolder("目标", tbTarget);
            while (tbTarget.Text.Trim().Equals(tbSource.Text.Trim()))
            {
                MessageBox.Show("文件夹不能相同");
                openFolder("目标", tbTarget);
            }
        }

        private void ComponentUseable(bool status)
        {
            btnSource.Enabled = status;
            btnTarget.Enabled = status;
            btnExe.Enabled = status;
            tbSource.Enabled = status;
            tbTarget.Enabled = status;
            rtbResult.Enabled = status;
        }

        private void btnExe_Click(object sender, EventArgs e)
        {
            string pathSource = tbSource.Text.Trim(), pathTarget = tbTarget.Text.Trim();
            //if (pathTarget.Equals(string.Empty) || pathSource.Equals(string.Empty) || pathTarget.Equals(pathSource))
            //{
            //    MessageBox.Show("请重新选择文件夹");
            //    return;
            //}
            pathSource = "C:\\Users\\Administrator\\Desktop\\原来的";
            pathTarget = "C:\\Users\\Administrator\\Desktop\\改过的";
            ComponentUseable(false);
            TransformDocs(pathSource, pathTarget);
            ComponentUseable(true);
        }

        /// <summary>
        /// 读取文件夹路径
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="tbBoard"></param>
        private void openFolder(string folderName, TextBox tbBoard)
        {
            dialog.Description = "请选择" + folderName + "文档路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                tbBoard.Text = foldPath;
            }
        }

        /// <summary>
        /// 转换文件夹中的Word文件
        /// </summary>
        /// <param name="pathSource">源文件目录</param>
        /// <param name="pathTarget">目标文件目录</param>
        private void TransformDocs(string pathSource, string pathTarget)
        {
            int total = 0, fail = 0;
            //路径
            DirectoryInfo sourceFolder = new DirectoryInfo(pathSource);
            FileInfo[] filesArray = sourceFolder.GetFiles();
            object fileTarget = null;
            //Word COM            
            object Nothing = Missing.Value;                       //COM调用时用于占位
            object format = MSWord.WdSaveFormat.wdFormatDocumentDefault; //Word文档的保存格式
            MSWord.Application wordApp = new MSWord.Application();              //声明一个wordAPP对象
            MSWord.Document worddoc = null;

            foreach (FileInfo docItem in filesArray)
            {
                if ((docItem.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden || docItem.Extension != ".doc" && docItem.Extension != ".docx")
                    continue;

                try
                {
                    total++;
                    lbResult.Text = "第 " +  total.ToString() + " 篇";
                    worddoc = wordApp.Documents.Open(docItem.FullName);
                    fileTarget = pathTarget + "\\" + docItem.Name;

                    //if (docItem.Extension == ".doc")
                    //{//word转换
                    //    fileTarget = pathTarget + "\\" + docItem.Name.Substring(0,docItem.Name.Length-4) + ".docx";
                    //    worddoc.SaveAs2(fileTarget);
                    //    wordApp.ActiveDocument.Close(false);
                    //    worddoc = wordApp.Documents.Open(fileTarget);
                    //}

                    //核心工作：修改文档
                    ChangeContents(worddoc);
                    worddoc.SaveAs2(fileTarget);
                }
                catch (Exception ex)
                {
                    fail++;
                    rtbResult.Text = docItem.Name + "\t" + ex.Message + ex.StackTrace +  "\r\n" + rtbResult.Text;
                }
                finally
                {
                    wordApp.ActiveDocument.Close(false);
                    worddoc = null;
                }
            }
            rtbResult.Text = "完成：\t成功：" + (total-fail).ToString () + "个；失败：" + fail.ToString () + "个；\r\n" + rtbResult.Text;
            wordApp.Quit();
        }

        /// <summary>
        /// 修改文档
        /// </summary>
        /// <param name="doc">文档内容</param>
        private void ChangeContents(MSWord.Document doc)
        {
            int senTotal = 0, idx = 0, idxTmp=0;
            MSWord.Range objRange = null;
            string strTmp = string.Empty;
            MSWord.Sentences content = null;

            //doc.SetCompatibilityMode((int)MSWord.WdCompatibilityMode.wdWord2010);
            content = doc.Sentences;
            senTotal = content.Count;
            pbInsert.Maximum = senTotal;
            for (int i = 1; i <= senTotal; i++)
            {
                pbInsert.Value = i;
                objRange = content[i].Words.First;
                idx = 1;
                if (objRange.Text == "\r" || objRange.Next().Text == "\r" || objRange.Next().Next().Text == "\r")
                    continue;//词语不足两个字则忽略
                while (objRange.Text.IndexOf("\r") < 0)
                {//插入字符
                    if (objRange.Text.IndexOf("。") >= 0 || objRange.Text.IndexOf("、") >= 0)
                    {//黑名单
                        if (content[i].Words.Count <= idx) break;
                        objRange = content[i].Words[++idx]; continue;
                    } 
                    idxTmp = content[i].Words.Count;
                    objRange.InsertAfter(GetRandomChar());
                    idx += (content[i].Words.Count - idxTmp);
                    //content[i].Words[++idx].Font.TextColor.ObjectThemeColor = MSWord.WdThemeColorIndex.wdThemeColorBackground1;
                    content[i].Words[idx].Font.Fill.Transparency = 1F;
                    content[i].Words[idx].Font.Spacing = -30;
                    objRange = content[i].Words[++idx];
                }
            }

            doc.Content.ParagraphFormat.AddSpaceBetweenFarEastAndAlpha = 0;
            doc.Content.ParagraphFormat.AddSpaceBetweenFarEastAndDigit = 0;
        }

        /// <summary>
        /// 生成随机字符
        /// </summary>
        /// <returns></returns>
        private string GetRandomChar()
        {
            int idx = random.Next(1, STRING_INSERT.Length);
            return STRING_INSERT[idx].ToString();
        }
    }
}
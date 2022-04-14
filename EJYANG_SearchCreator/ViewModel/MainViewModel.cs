using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace EJYANG_SearchCreator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region ----- Define Commands -----
        public RelayCommand GetSourceFolderCommand { get; set; }
        public RelayCommand GetImageFolderCommand { get; set; }
        public RelayCommand StartTransferCommand { get; set; }
        #endregion

        #region ----- Define Variables -----
        private string _sourceFile = "";
        private string _imgFile = "";

        public string ImgFile
        {
            get => _imgFile;
            set { Set(() => ImgFile, ref _imgFile, value); }
        }
        public string SourceFile
        {
            get => _sourceFile;
            set { Set(() => SourceFile, ref _sourceFile, value); }
        }
        public string Title { get; set; }
        public string Hint { get; set; }
        #endregion

        public MainViewModel()
        {
            InitCommands();
        }

        #region ----- Define Actions -----
        private void GetSourceFolderAction()
        {
            SourceFile = GetCSVFile();
        }
        private void GetImageFolderAction()
        {
            ImgFile = GetImgFile();
        }
        private void StartTransferAction()
        {
            IndexCreator creator = new IndexCreator(Title, Hint, SourceFile, ImgFile.Split('\\').Last());
            string content = creator.CreateFileContent();

            using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\index.html", false, Encoding.UTF8))
            {
                writer.WriteLine(content);
            }
        }
        #endregion

        #region ----- Define Functions -----
        private void InitCommands()
        {
            GetSourceFolderCommand = new RelayCommand(GetSourceFolderAction);
            GetImageFolderCommand = new RelayCommand(GetImageFolderAction);
            StartTransferCommand = new RelayCommand(StartTransferAction);
        }
        private string GetCSVFile()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Select file";
                dialog.InitialDirectory = ".\\";
                dialog.Filter = "TXT|*.txt";

                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                {
                    return dialog.FileName;
                }
            }

            return "";
        }
        private string GetImgFile()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Select file";
                dialog.InitialDirectory = ".\\";
                dialog.Filter = "Image File|*.jpg;*.jpeg;*.png";

                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                {
                    return dialog.FileName;
                }
            }

            return "";
        }
        #endregion
    }
}
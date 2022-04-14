using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EJYANG_SearchCreator
{
    public class IndexCreator
    {
        #region ----- Define Base -----
        private string indexBase1 = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""utf-8"">
    
    <title>";
        private string indexBase2 = @"</title>

    <!-- Bootstrap Core CSS -->
    <link href=""bootstrap/css/bootstrap.min.css"" rel=""stylesheet"">
    <link href=""search.css"" rel=""stylesheet"">
</head>

<body id=""grad1"">
    <!-- Main Section -->
    <section id=""about"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-lg-12 text-left"">
                    <h2>";
        private string indexBase3 = @"</h2>
                </div>
            </div>
            <p/>
            <div class=""row"">
                <div class=""col-lg-12 text-left"">
                    <input type=""text"" style=""font-size:30px; width:250px"" class=""search-text"" placeholder=""";
        private string indexBase4 = @"""/>
                </div>
            </div>
            <p/>
            <div class=""row"">
                <div class=""col-lg-12 text-left"">
                    <input type=""button"" value=""查詢"" style=""width:150px;height:50px;"" data-table=""result-table"" class=""light-table-filter""/>
                </div>
            </div>
            <p/>
            <div class=""row"">
                <div class=""col-lg-12 text-left"">
                    <h3 class=""no-result"" style=""display: none"">查無相關資料！</h3>
                </div>
            </div>
            <div class=""row"">
                <div class=""col-lg-12 text-left"">
                    <table class=""result-table"" style=""display: none"">";
        private string indexBase5 = @"</table>
                </div>
            </div>
            <p/>
            <div class=""row"">
                <div class=""col-lg-12 text-left"">
                    ";
        private string indexBase6 = @"
                </div>
            </div>
        </div>
    </section>

    <!-- JavaScript -->
    <script src=""javascript.js""></script>

    <!-- Bootstrap Core JavaScript -->
    <script src=""bootstrap/js/bootstrap.min.js""></script>

</body>

</html>";
        #endregion

        #region ----- Define Variables -----
        private string sourceFilePath;
        private string indexTitle;
        private string indexHint;
        private string imgName;
        #endregion

        public IndexCreator(string title, string hint, string filePath, string img)
        {
            indexTitle = title;
            indexHint = hint;
            sourceFilePath = filePath;
            imgName = img;
        }

        #region ----- Define Functions -----
        internal string CreateFileContent()
        {
            string finalContent = indexBase1 + indexTitle + indexBase2 + indexTitle + indexBase3 + indexHint + indexBase4;

            finalContent += GetTableHtml();

            finalContent += indexBase5;

            if (imgName != "")
            {
                finalContent += @"<img src=""" + imgName + @""" height=""500"" style=""max-height: 100%; max-width: 100%; object-fit: contain""/>";
            }

            return finalContent + indexBase6;
        }
        private string GetTableHtml()
        {
            string result = "";

            using (StreamReader reader = new StreamReader(sourceFilePath))
            {
                result += @"<thead><tr>";

                string line = reader.ReadLine();
                var headers = line.Split('\t');

                foreach (var header in headers)
                {
                    result += $"<th>{header}</th>";
                }

                result += @"</tr></thead><tbody>";

                while ((line = reader.ReadLine()) != null)
                {
                    var datas = line.Split('\t');

                    result += @"<tr>";

                    foreach (var data in datas)
                    {
                        result += $"<td>{data}</td>";
                    }

                    result += @"</tr>";
                }

                result += @"</tbody>";
            }

            return result;
        }
        #endregion
    }
}

using J3Price.BLL;
using J3Price.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace J3Price.Controllers
{
    public class PicturesController : ApiController
    {
        //Get : api/Pictures
        public HttpResponseMessage Get(string fileName)
        {
            HttpResponseMessage result = null;
            DirectoryInfo directoryInfo = new DirectoryInfo(J3Price_Helper.RootPath_Config + @"Files/Pictures");
            FileInfo foundFileInfo = directoryInfo.GetFiles().Where(x => x.Name == fileName).FirstOrDefault();
            if (foundFileInfo != null)
            {
                FileStream fs = new FileStream(foundFileInfo.FullName, FileMode.Open);
                result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StreamContent(fs);
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = foundFileInfo.Name;
            }
            else
            {
                result = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return result;
        }
        //POST : api/Pictures
        public async Task<IHttpActionResult> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new Exception("unsupported media type");
            }
            string root = J3Price_Helper.RootPath_Config;
            J3Price_Helper.CreateDirectoryIfNotExist(root + "/temp");
            var provider = new MultipartFormDataStreamProvider(root + "/temp");
            // Read the form data.
            await Request.Content.ReadAsMultipartAsync(provider);
            List<PictureResModel> fileNameList = new List<PictureResModel>();
            StringBuilder sb = new StringBuilder();
            long fileTotalSize = 0;
            int fileIndex = 1;
            // This illustrates how to get the file names.
            foreach (MultipartFileData file in provider.FileData)
            {
                //new folder
                string newRoot = root + @"Files/Pictures";
                J3Price_Helper.CreateDirectoryIfNotExist(newRoot);
                if (File.Exists(file.LocalFileName))
                {
                    //new fileName
                    string fileName = file.Headers.ContentDisposition.FileName.Substring(1, file.Headers.ContentDisposition.FileName.Length - 2);
                    string filetype = fileName.Substring(fileName.LastIndexOf(".")+1, fileName.Length - fileName.LastIndexOf(".")-1);
                    string newFileName = Guid.NewGuid() + "." + filetype;
                    string newFullFileName = newRoot + "/" + newFileName;
                    fileNameList.Add(new PictureResModel { imgUrl = $"Files/Pictures/{newFileName}" });
                    FileInfo fileInfo = new FileInfo(file.LocalFileName);
                    fileTotalSize += fileInfo.Length;
                    sb.Append($" #{fileIndex} Uploaded file: {newFileName} ({ fileInfo.Length} bytes)");
                    fileIndex++;
                    File.Move(file.LocalFileName, newFullFileName);
                    Trace.WriteLine("1 file copied , filePath=" + newFullFileName);
                }
            }
            
            return Json(fileNameList);
            //return Json(Return_Helper.Success_Msg_Data_DCount_HttpCode($"{fileNameList.Count} file(s) /{fileTotalSize} bytes uploaded successfully!   Details -> {sb.ToString()}", fileNameList, fileNameList.Count));
        }
    }
}

using Masuit.Tools;
using Masuit.Tools.AspNetCore.Mime;
using Masuit.Tools.AspNetCore.ResumeFileResults.Extensions;
using Masuit.Tools.Html;
using Masuit.Tools.Logging;
using Masuit.Tools.Media;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Hangfire;
using Masuit.Tools.Systems;
using Microsoft.AspNetCore.Http;
using QuickWeb.Controllers.Common;
using QuickWeb.Extensions.Common;
using QuickWeb.Extensions.UEditor;
using Quick.IService;
using System.Threading.Tasks;
using System.Collections.Generic;
using Quick.Models.Entity.Table;
using QuickWeb.Models.RequestModel;
using QuickWeb.Extensions;
using System.Linq;
using Quick.Common.Models;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 文件上传
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UploadController : AdminBaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public UploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// yoshop_upload_file对象业务方法
        /// </summary>
        public Iyoshop_upload_fileService UploadFileService { get; set; }

        /// <summary>
        /// yoshop_upload_group对象业务方法
        /// </summary>
        public Iyoshop_upload_groupService UploadGroupService { get; set; }

        #region Word上传转码

        /// <summary>
        /// 上传Word转码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadWord()
        {
            var files = Request.Form.Files;
            if (files.Count > 0 && files[0] != null)
            {
                var file = files[0];
                string fileName = file.FileName;
                if (fileName != null && !Regex.IsMatch(Path.GetExtension(fileName), "doc|docx"))
                {
                    return ResultData(null, false, "文件格式不支持，只能上传doc或者docx的文档!");
                }
                if (fileName != null)
                {
                    string upload = _hostingEnvironment.WebRootPath + "/upload";
                    if (!Directory.Exists(upload))
                    {
                        Directory.CreateDirectory(upload);
                    }
                    string resourceName = string.Empty.CreateShortToken(9);
                    string ext = Path.GetExtension(fileName);
                    string docPath = Path.Combine(upload, resourceName + ext);
                    using (FileStream fs = new FileStream(docPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        file.CopyTo(fs);
                    }
                    string htmlDir = docPath.Replace(".docx", "").Replace(".doc", "");
                    DocumentConvert.Doc2Html(docPath, htmlDir);
                    string htmlfile = Path.Combine(htmlDir, "index.html");
                    string html = System.IO.File.ReadAllText(htmlfile).ReplaceHtmlImgSource("/upload/" + resourceName).ClearHtml().HtmlSantinizerStandard();
                    ThreadPool.QueueUserWorkItem(state => System.IO.File.Delete(htmlfile));
                    if (html.Length < 10)
                    {
                        Directory.Delete(htmlDir, true);
                        System.IO.File.Delete(docPath);
                        return ResultData(null, false, "读取文件内容失败，请检查文件的完整性，建议另存后重新上传！");
                    }
                    if (html.Length > 1000000)
                    {
                        Directory.Delete(htmlDir, true);
                        System.IO.File.Delete(docPath);
                        return ResultData(null, false, "文档内容超长，服务器拒绝接收，请优化文档内容后再尝试重新上传！");
                    }
                    return ResultData(new
                    {
                        Title = Path.GetFileNameWithoutExtension(fileName),
                        Content = html,
                        ResourceName = resourceName + ext
                    });
                }
            }
            return ResultData(null, false, "请先选择您需要上传的文件!");
        }

        /// <summary>
        /// 解码Base64图片
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult DecodeDataUri(string data)
        {
            var dir = "/upload/images";
            var filename = string.Empty.CreateShortToken(9) + ".jpg";
            string path = Path.Combine(dir, filename);
            try
            {
                data.SaveDataUriAsImageFile().Save(_hostingEnvironment.WebRootPath + path, System.Drawing.Imaging.ImageFormat.Jpeg);
                var (url, success) = CommonHelper.UploadImage(path);
                BackgroundJob.Enqueue(() => System.IO.File.Delete(path));
                if (success)
                {
                    return ResultData(url);
                }
                return ResultData(null, false, "图片上传失败！");
            }
            catch (Exception e)
            {
                LogManager.Error(e);
                return ResultData(null, false, "转码失败！");
            }
        }

        #endregion

        #region 文件下载
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [Route("download")]
        [Route("download/{path}")]
        public ActionResult Download(string path)
        {
            if (string.IsNullOrEmpty(path)) return Content("null");
            var file = Path.Combine(_hostingEnvironment.WebRootPath + "/upload", path.Trim('.', '/', '\\'));
            if (System.IO.File.Exists(file))
            {
                return this.ResumePhysicalFile(file, Path.GetFileName(file));
            }
            return Content("null");
        }
        #endregion

        #region UEditor文件上传
        /// <summary>
        /// UEditor文件上传处理
        /// </summary>
        /// <returns></returns>
        [Route("fileuploader")]
        public ActionResult UeditorFileUploader()
        {
            //UserInfoOutputDto user = HttpContext.Session.GetByRedis<UserInfoOutputDto>(SessionKey.UserInfo) ?? new UserInfoOutputDto();
            bool IsAdmin = false;
            Handler action = new NotSupportedHandler(HttpContext);
            switch (Request.Query["action"])//通用
            {
                case "config":
                    action = new ConfigHandler(HttpContext);
                    break;
                case "uploadimage":
                    action = new UploadHandler(HttpContext, new UploadConfig()
                    {
                        AllowExtensions = UeditorConfig.GetStringList("imageAllowFiles"),
                        PathFormat = UeditorConfig.GetString("imagePathFormat"),
                        SizeLimit = UeditorConfig.GetInt("imageMaxSize"),
                        UploadFieldName = UeditorConfig.GetString("imageFieldName")
                    });
                    break;
                case "uploadscrawl":
                    action = new UploadHandler(HttpContext, new UploadConfig()
                    {
                        AllowExtensions = new[] { ".png" },
                        PathFormat = UeditorConfig.GetString("scrawlPathFormat"),
                        SizeLimit = UeditorConfig.GetInt("scrawlMaxSize"),
                        UploadFieldName = UeditorConfig.GetString("scrawlFieldName"),
                        Base64 = true,
                        Base64Filename = "scrawl.png"
                    });
                    break;
                case "catchimage":
                    action = new CrawlerHandler(HttpContext);
                    break;
            }

            if (IsAdmin)
            {
                switch (Request.Query["action"])//管理员用
                {
                    //case "uploadvideo":
                    //    action = new UploadHandler(context, new UploadConfig()
                    //    {
                    //        AllowExtensions = UeditorConfig.GetStringList("videoAllowFiles"),
                    //        PathFormat = UeditorConfig.GetString("videoPathFormat"),
                    //        SizeLimit = UeditorConfig.GetInt("videoMaxSize"),
                    //        UploadFieldName = UeditorConfig.GetString("videoFieldName")
                    //    });
                    //    break;
                    case "uploadfile":
                        action = new UploadHandler(HttpContext, new UploadConfig()
                        {
                            AllowExtensions = UeditorConfig.GetStringList("fileAllowFiles"),
                            PathFormat = UeditorConfig.GetString("filePathFormat"),
                            SizeLimit = UeditorConfig.GetInt("fileMaxSize"),
                            UploadFieldName = UeditorConfig.GetString("fileFieldName")
                        });
                        break;
                        //case "listimage":
                        //    action = new ListFileManager(context, UeditorConfig.GetString("imageManagerListPath"), UeditorConfig.GetStringList("imageManagerAllowFiles"));
                        //    break;
                        //case "listfile":
                        //    action = new ListFileManager(context, UeditorConfig.GetString("fileManagerListPath"), UeditorConfig.GetStringList("fileManagerAllowFiles"));
                        //    break;
                }
            }

            string result = action.Process();
            return Content(result, ContentType.Json);
        }
        #endregion

        #region Masuit通用上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        public ActionResult UploadFile(IFormFile file)
        {
            string path;
            string filename = $"{DateTime.Now:yyyyMMddHHmmss}"+ SnowFlake.GetInstance().GetUniqueShortId(9) + Path.GetExtension(file.FileName);
            switch (file.ContentType)
            {
                case var _ when file.ContentType.StartsWith("image"):
                    path = Path.Combine(_hostingEnvironment.WebRootPath, "upload", "images", $"{DateTime.Now:yyyy}/{DateTime.Now:MM}/{DateTime.Now:dd}", filename);
                    var dir = Path.GetDirectoryName(path);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        file.CopyTo(fs);
                    }
                    var (url, success) = CommonHelper.UploadImage(path);
                    if (success)
                    {
                        BackgroundJob.Enqueue(() => System.IO.File.Delete(path));
                        return YesResult(url);
                    }
                    break;
                case var _ when file.ContentType.StartsWith("audio") || file.ContentType.StartsWith("video"):
                    path = Path.Combine(_hostingEnvironment.WebRootPath, "upload", "medias", $"{DateTime.Now:yyyy}/{DateTime.Now:MM}/{DateTime.Now:dd}", filename);
                    break;
                case var _ when file.ContentType.StartsWith("text") || (ContentType.Doc + "," + ContentType.Xls + "," + ContentType.Ppt + "," + ContentType.Pdf).Contains(file.ContentType):
                    path = Path.Combine(_hostingEnvironment.WebRootPath, "upload", "docs", $"{DateTime.Now:yyyy}/{DateTime.Now:MM}/{DateTime.Now:dd}", filename);
                    break;
                default:
                    path = Path.Combine(_hostingEnvironment.WebRootPath, "upload", "files", $"{DateTime.Now:yyyy}/{DateTime.Now:MM}/{DateTime.Now:dd}", filename);
                    break;
            }
            try
            {
                var dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    file.CopyTo(fs);
                }
                return YesResult(path.Substring(_hostingEnvironment.WebRootPath.Length).Replace("\\", "/"));
            }
            catch (Exception e)
            {
                LogManager.Error(e);
                return No("文件上传失败！");
            }
        }

        #endregion

        #region 文件库文件管理

        /// <summary>
        /// 文件库列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="type"></param>
        /// <param name="group_id"></param>
        /// <returns></returns>
        [HttpGet, Route("/upload.library/fileList")]
        public async Task<IActionResult> LibraryFileList(FilePageRequest request, string type = "image", int group_id = -1)
        {
            var group_list = await GetUploadGroupList(type);
            var data = GetUploadFileList(group_id, type, request);
            request.data = data;
            request.last_page = (int)Math.Ceiling(request.total * 1.0f / request.per_page * 1.0f);
            return YesResult(new { group_list, file_list = request });
        }

        /// <summary>
        /// 通用图片文件上传 (多文件)
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("/upload/image")]
        public async Task<IActionResult> UploadImage(IFormFile file, FileUploadRequest request)
        {
            //var file = Request.Form.Files[0];
            yoshop_upload_file upload;
            try
            {
                var result = _ = await FileUpload(file);
                if (result.Code == 0) return No(result.Msg);
                upload = new yoshop_upload_file
                {
                    create_time = DateTimeExtensions.GetCurrentTimeStamp(),
                    file_type = "image",
                    storage = "local",
                    is_delete = 0,
                    group_id = request.group_id,
                    file_size = request.size,
                    file_url = result.Msg,
                    file_name = request.name,
                    extension = Path.GetExtension(request.name).TrimStart('.'),
                    wxapp_id = GetAdminSession().wxapp_id
                };

                UploadFileService.AddEntity(upload);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return YesResult("图片上传成功！", upload);
        }

        /// <summary>
        /// 文件库移动文件
        /// </summary>
        /// <param name="group_id"></param>
        /// <param name="fileIds"></param>
        /// <returns></returns>
        [HttpPost, Route("/upload.library/moveFiles")]
        public IActionResult LibraryMoveFiles(uint group_id, uint[] fileIds)
        {
            try
            {
                if (fileIds.Length > 0)
                {
                    UploadFileService.Update(x => new yoshop_upload_file() { group_id = group_id }, l => fileIds.Contains(l.file_id));
                }
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Yes("移动成功！");
        }

        /// <summary>
        /// 文件库删除文件
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("/upload.library/deleteFiles")]
        public IActionResult LibraryDeleteFiles(uint[] fileIds)
        {
            try
            {
                if (fileIds.Length > 0)
                {
                    UploadFileService.Update(x => new yoshop_upload_file() { is_delete = 1 }, l => fileIds.Contains(l.file_id));
                }
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Yes("删除成功！");
        }

        private IEnumerable<yoshop_upload_file> GetUploadFileList(int group_id, string type = "image", FilePageRequest request = null)
        {
            return group_id == -1
                ? UploadFileService.LoadPageEntities<uint>(request.current_page, request.per_page, ref request.total, l => l.file_type == type && l.is_delete == 0, l => l.file_id, true)
                : UploadFileService.LoadPageEntities<uint>(request.current_page, request.per_page, ref request.total, l => l.file_type == type && l.is_delete == 0 && l.group_id == group_id, l => l.file_id, true);
        }

        /// <summary>
        /// 通用文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task<ResultInfo> FileUpload(IFormFile file)
        {
            if(file == null)
                return FailResp("上传文件为空！");
            string filename = $"{DateTime.Now:yyyyMMddHHmmss}"+ SnowFlake.GetInstance().GetUniqueShortId(9) + Path.GetExtension(file.FileName);
            string path;
            switch (file.ContentType)
            {
                case var _ when file.ContentType.StartsWith("image"):
                    path = Path.Combine(_hostingEnvironment.WebRootPath, "upload", "images", $"{DateTime.Now:yyyy}/{DateTime.Now:MM}/{DateTime.Now:dd}", filename);
                    break;
                case var _ when file.ContentType.StartsWith("audio") || file.ContentType.StartsWith("video"):
                    path = Path.Combine(_hostingEnvironment.WebRootPath, "upload", "media", $"{DateTime.Now:yyyy}/{DateTime.Now:MM}/{DateTime.Now:dd}", filename);
                    break;
                case var _ when file.ContentType.StartsWith("text") || (ContentType.Doc + "," + ContentType.Xls + "," + ContentType.Ppt + "," + ContentType.Pdf).Contains(file.ContentType):
                    path = Path.Combine(_hostingEnvironment.WebRootPath, "upload", "docs", $"{DateTime.Now:yyyy}/{DateTime.Now:MM}/{DateTime.Now:dd}", filename);
                    break;
                default:
                    path = Path.Combine(_hostingEnvironment.WebRootPath, "upload", "files", $"{DateTime.Now:yyyy}/{DateTime.Now:MM}/{DateTime.Now:dd}", filename);
                    break;
            }
            try
            {
                var dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    await file.CopyToAsync(fs);
                }
                return SuccResp(path.Substring(_hostingEnvironment.WebRootPath.Length).Replace("\\", "/"));
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return FailResp("文件上传失败！");
            }
        }

        #endregion

        #region 文件库文件分组管理

        /// <summary>
        /// 添加文件组
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("/upload.library/addGroup")]
        public IActionResult LibraryAddGroup(UploadGroupRequest request)
        {
            var timestamp = DateTimeExtensions.GetCurrentTimeStamp();

            var model = new yoshop_upload_group();
            model.group_name = request.group_name;
            model.group_type = request.group_type;
            model.sort = 100;
            model.wxapp_id = GetAdminSession().wxapp_id;
            model.create_time = timestamp;
            model.update_time = timestamp;
            int group_id;
            try
            {
                group_id = UploadGroupService.AddEntityReturnIdentity(model);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return YesResult("添加成功！", new { group_id, request.group_name });
        }

        /// <summary>
        /// 编辑文件组
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("/upload.library/editGroup")]
        public IActionResult LibraryEditGroup(UploadGroupRequest request)
        {
            try
            {
                var model = UploadGroupService.GetById(request.group_id);
                if (model == null) return No("文件分组不存在或已被删除");
                model.group_name = request.group_name;
                model.update_time = DateTimeExtensions.GetCurrentTimeStamp();
                UploadGroupService.UpdateEntity(model);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Yes("编辑成功！");
        }

        /// <summary>
        /// 删除文件组
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("/upload.library/deleteGroup")]
        public IActionResult LibraryDeleteGroup(int group_id)
        {
            try
            {
                var _ = UploadGroupService.Delete(l => l.group_id == group_id);
                if (_)
                    UploadFileService.Update(x => new yoshop_upload_file { group_id = 0 }, l => l.group_id == group_id);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return Yes("删除成功！");
        }

        private async Task<List<yoshop_upload_group>> GetUploadGroupList(string group_type = "image")
        {
            return await UploadGroupService.LoadOrderedEntities<uint>(l => l.group_type == group_type, l => l.sort, true).ToListAsync();
        }

        #endregion
    }
}
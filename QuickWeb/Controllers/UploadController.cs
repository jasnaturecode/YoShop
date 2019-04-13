using Masuit.Tools;
using Masuit.Tools.AspNetCore.Mime;
using Masuit.Tools.Logging;
using Masuit.Tools.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quick.Common.Models;
using Quick.IService;
using Quick.Models.Entity.Table;
using QuickWeb.Controllers.Common;
using QuickWeb.Extensions;
using QuickWeb.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
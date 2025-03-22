using APP.API.Controllers.Helper;
using Microsoft.AspNetCore.Mvc;

namespace APP.API.Controllers
{
    [ApiController]
    [Route("api/upload")]
    public class UploadController : ApiBaseController
    {
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        public UploadController()
        {
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(10 * 1024 * 1024)] // Giới hạn 10MB
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return ResponseNoData(400, "File không hợp lệ.");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                return ResponseNoData(400, "Chỉ chấp nhận file .jpg, .jpeg, .png");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return ResponseOk(new { fileName = uniqueFileName, path = $"/Uploads/{uniqueFileName}" });
        }

        /// <summary>
        /// Lấy danh sách tất cả file trong thư mục Uploads.
        /// </summary>
        [HttpGet("all-files")]
        public IActionResult GetAllFiles()
        {
            var files = Directory.GetFiles(_uploadPath)
                                 .Select(Path.GetFileName)
                                 .ToList();

            if (files.Count == 0)
                return _respNotFound;

            return ResponseOk(new { files });
        }

        [HttpGet("{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var filePath = Path.Combine(_uploadPath, fileName);

            if (!System.IO.File.Exists(filePath))
                return _respNotFound;

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var contentType = GetContentType(fileName);

            return File(fileBytes, contentType);
        }

        /// <summary>
        /// Xóa ảnh theo tên file
        /// </summary>
        [HttpDelete("delete/{fileName}")]
        public IActionResult DeleteImage(string fileName)
        {
            var filePath = Path.Combine(_uploadPath, fileName);

            if (!System.IO.File.Exists(filePath))
                return _respNotFound;

            System.IO.File.Delete(filePath);
            return ResponseOk();
        }

        /// <summary>
        /// Xóa tất cả ảnh trong thư mục Uploads
        /// </summary>
        [HttpDelete("delete-all")]
        public IActionResult DeleteAllImages()
        {
            var files = Directory.GetFiles(_uploadPath);

            if (files.Length == 0)
                return _respNotFound;

            foreach (var file in files)
            {
                System.IO.File.Delete(file);
            }

            return ResponseOk();
        }

        /// <summary>
        /// Xác định loại nội dung (MIME type) dựa vào phần mở rộng file.
        /// </summary>
        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };
        }

    }
}

﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace TrickingLibrary.BackgroundServices.VideoEditing
{
    public class VideoManager
    {
        private readonly IWebHostEnvironment _env;
        public const string TempPrefix = "temp_";
        public const string ConvertedPrefix = "c";
        public const string ThumbnailPrefix = "t";

        public VideoManager(IWebHostEnvironment env)
        {
            _env = env;
        }

        private string WorkingDirectory => _env.WebRootPath;
        public string FFMPEGPath => Path.Combine(_env.ContentRootPath, "ffmpeg", "ffmpeg.exe");
        public bool Temporary(string fileName)
        {
            return fileName.StartsWith(TempPrefix);
        }

        public bool TemporaryFileExists(string fileName)
        {
            var path = TemporarySavePath(fileName);
            return File.Exists(path);
        }

        public void DeleteTemporaryFile(string fileName)
        {
            var path = TemporarySavePath(fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public string DevVideoPath(string fileName)
        {
            return !_env.IsDevelopment() ? null : Path.Combine(WorkingDirectory, fileName);
        }

        public string GenerateConvertedFileName() => $"{ConvertedPrefix}{DateTime.Now.Ticks}.mp4";
        public string GenerateThumbnailFileName() => $"{ThumbnailPrefix}{DateTime.Now.Ticks}.png";

        public async Task<string> SaveTemporaryVideo(IFormFile video)
        {
            var fileName = string.Concat(TempPrefix, DateTime.Now.Ticks, Path.GetExtension(video.FileName));
            var savePath = TemporarySavePath(fileName);
            await using (var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                await video.CopyToAsync(fileStream);
            }

            return fileName;
        }

        public string TemporarySavePath(string fileName)
        {
            return Path.Combine(WorkingDirectory, fileName);
        }
    }
}
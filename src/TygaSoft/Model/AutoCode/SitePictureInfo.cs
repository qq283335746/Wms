using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class SitePictureInfo
    {
        public SitePictureInfo() { }

        public SitePictureInfo(Guid id, Guid userId, string fileName, int fileSize, string fileExtension, string fileDirectory, string randomFolder, string funType, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.FileName = fileName;
            this.FileSize = fileSize;
            this.FileExtension = fileExtension;
            this.FileDirectory = fileDirectory;
            this.RandomFolder = randomFolder;
            this.FunType = funType;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string FileExtension { get; set; }
        public string FileDirectory { get; set; }
        public string RandomFolder { get; set; }
        public string FunType { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}

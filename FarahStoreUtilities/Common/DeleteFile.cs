using FarahStoreModel.CommonModel;

namespace FarahStoreUtilities.Common;

public static class DeleteFile
{

    public static ResultDto DeleteFileFromRoot(string path)
    {
        System.IO.File.Delete(path);
        return new ResultDto()
        {
            Status = true,
            Message = "doun"
        };
    }
}
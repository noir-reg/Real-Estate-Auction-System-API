﻿namespace BusinessObjects.Constants;

public static class FileTypes
{
    public const string PDF = "application/pdf";
    public const string JPEG = "image/jpeg";
    public const string PNG = "image/png";
    public const string DOC = "application/msword";
    public const string DOCX = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
    public const string XLS = "application/vnd.ms-excel";
    public const string XLSX = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
}

public class FileTypeChecker
{
    private HashSet<string> validFileTypes = new()
    {
        FileTypes.PDF,
        FileTypes.JPEG,
        FileTypes.PNG,
        FileTypes.DOC,
        FileTypes.DOCX,
        FileTypes.XLS,
        FileTypes.XLSX
    };

    public bool IsValidFileType(string fileType)
    {
        return validFileTypes.Contains(fileType);
    }
}

public class FileChecker
{
    public const long FILE_SIZE_LIMIT = 20_971_520L;
}
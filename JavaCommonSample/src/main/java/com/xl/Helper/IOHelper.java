package com.xl.Helper;

import org.apache.commons.io.FileUtils;
import org.apache.commons.io.IOUtils;
import org.apache.commons.io.LineIterator;

import java.io.*;
import java.net.URL;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

// 以下方法 的功能在FileUtil中有
public class IOHelper {

    public static void WriteStringToFile(String I_FilePath,String I_Content) throws IOException
    {
        try (InputStream is = IOUtils.toInputStream(I_Content, "utf-8");
             OutputStream os = new FileOutputStream(I_FilePath)) {
             IOUtils.copy(is, os);
        }
    }

    public static String ReadStringFromFile(String I_FilePath) throws IOException
    {
        String result = "";
        try(FileReader fileReader = new FileReader(I_FilePath))
        {
            result = IOUtils.toString(fileReader);
        }
        return result;
    }

    public static List<String> ReadLinesFromFile(String I_FilePath) throws IOException
    {
        List<String> result;
        try (FileInputStream fin = new FileInputStream(I_FilePath)) {
            result = IOUtils.readLines(fin, "utf-8");
        }
        return result;
    }

    public static void WriteLinesToFile(String I_FilePath,List<String> contentList )throws IOException
    {
        try(OutputStream os=new FileOutputStream(I_FilePath)) {
            IOUtils.writeLines(contentList,IOUtils.LINE_SEPARATOR,os);
        }
    }



    public static void CopyFile(String I_SourcePath,String I_TargetPath)  throws IOException
    {
        File src = new File(I_SourcePath);
        File dest = new File(I_TargetPath);
        FileUtils.copyFile(src, dest);
    }

    public static void DownLoadUrlToFile(String I_Url,String I_TargetPath)  throws Exception
    {
        URL url = new URL(I_Url);
        File file = new File(I_TargetPath);
        FileUtils.copyURLToFile(url, file);

    }


}

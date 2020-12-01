package com.xl.JavaCommonSample;


import com.xl.Helper.IOHelper;
import org.apache.commons.io.*;

import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;

public class FileIOUtilsSample {

    public static void main(String[] args) {
        TestFile();
        TestFilename();
    }

    public static void TestIO()
    {
        try {
            IOHelper.WriteStringToFile("iotest1.txt","abcdefdfd");


            String readContent = IOHelper.ReadStringFromFile("iotest1.txt");

            List<String> testList = new ArrayList<>();
            testList.add("aaaaaa");
            testList.add("bbbbbb");
            testList.add("cccccc");
            IOHelper.WriteLinesToFile("iotest2.txt",testList);

            List<String> readList = IOHelper.ReadLinesFromFile("iotest2.txt");

            //通过迭代器依次按行读取
            try(FileInputStream fin=new FileInputStream("iotest2.txt")){
                LineIterator lt=IOUtils.lineIterator(fin,"utf-8");
                while (lt.hasNext())
                {
                    System.out.println(lt.nextLine());
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
        }


    }

    public static void TestFile()
    {
        System.out.println(FileUtils.getTempDirectory());//获取临时目录 java.io.tmpdir,getUserDirectoryPath返回路径字符串
        System.out.println(FileUtils.getUserDirectory());//获取用户主目录 user.home,getUserDirectoryPath返回路径字符串

        //以可读的方式，返回文件的大小EB, PB, TB, GB, MB, KB or bytes
        System.out.println(FileUtils.byteCountToDisplaySize(10000000));
        System.out.println(FileUtils.byteCountToDisplaySize(1));

        try {
            FileUtils.writeStringToFile(new File("filetest1.txt"),"abcdefdfd","utf-8");
            String readContent = FileUtils.readFileToString(new File("filetest1.txt"),"utf-8") ;

            List<String> testList = new ArrayList<>();
            testList.add("aaaaaa");
            testList.add("bbbbbb");
            testList.add("cccccc");
            FileUtils.writeLines(new File("filetest2.txt"),testList);
            List<String> readList =FileUtils.readLines(new File("filetest2.txt"),"utf-8");

            LineIterator  lt = FileUtils.lineIterator(new File("filetest2.txt"));

            while (lt.hasNext())
            {
                System.out.println(lt.nextLine());
            }
        } catch (IOException e) {
            e.printStackTrace();
        }

    }

    public static void TestFilename()
    {
        String fileDirectory = "D:/aa/bb/cc";
        String fileName ="file.txt";
        String fileFullName = fileDirectory +"/"+ fileName;
        System.out.println("(1)显示linux路径:"+FilenameUtils.normalizeNoEndSeparator(fileFullName));
        System.out.println("(2)合并目录和文件名为文件全路径:"+FilenameUtils.concat(fileDirectory, fileName));
        System.out.println("(3)文件路径去除目录和后缀后的文件名:"+FilenameUtils.getBaseName(fileFullName));
        System.out.println("(4)获取文件的后缀:"+FilenameUtils.getExtension(fileFullName));
        System.out.println("(5)获取文件的完整目录:"+FilenameUtils.getFullPath(fileFullName));
        System.out.println("(6)获取文件的目录不包含结束符:"+FilenameUtils.getFullPathNoEndSeparator(fileFullName));
        System.out.println("(7)获取文件名称，包含后缀:"+FilenameUtils.getName(fileFullName));
        System.out.println("(8)去除前缀的路径:"+FilenameUtils.getPath(fileFullName));
        System.out.println("(9)去除前缀并结尾去除分隔符:"+FilenameUtils.getPathNoEndSeparator(fileFullName));
        System.out.println("(10)获取前缀:"+FilenameUtils.getPrefix(fileFullName));
        System.out.println("(11)获取前缀长度:"+FilenameUtils.getPrefixLength(fileFullName));
        System.out.println("(12)获取最后一个.的位置:"+FilenameUtils.indexOfExtension(fileFullName));
        System.out.println("(13)获取最后一个/的位置:"+FilenameUtils.indexOfLastSeparator(fileFullName));
        System.out.println("(14)获取当前系统格式化路径:"+FilenameUtils.normalize(fileFullName));
        System.out.println("(16)获取当前系统无结尾分隔符的路径:"+FilenameUtils.normalizeNoEndSeparator(fileDirectory));
        System.out.println("(17)获取linux系统无结尾分隔符的路径:"+FilenameUtils.normalizeNoEndSeparator(fileDirectory));
        System.out.println("(18)移除文件的扩展名:"+FilenameUtils.removeExtension(fileFullName));
        System.out.println("(19)转换分隔符为当前系统分隔符:"+FilenameUtils.separatorsToSystem(fileFullName));
        System.out.println("(20)转换分隔符为linux系统分隔符:"+FilenameUtils.separatorsToUnix(fileFullName));
        System.out.println("(21)转换分隔符为windows系统分隔符:"+FilenameUtils.separatorsToWindows(fileFullName));
        try {
            System.out.println("(22)判断目录下是否包含指定文件或目录:"+FilenameUtils.directoryContains(fileDirectory, fileName));
        } catch (IOException e) {
            e.printStackTrace();
        }
        String linuxFileName = FilenameUtils.normalize(fileFullName);
        System.out.println("(23)判断文件路径是否相同:"+FilenameUtils.equals(fileFullName, linuxFileName));
        System.out.println("(24)判断文件路径是否相同，格式化并大小写不敏感:"+FilenameUtils.equals(fileFullName,
                FilenameUtils.normalize(fileFullName),true,IOCase.INSENSITIVE));
        System.out.println("(25)判断文件路径是否相同，格式化并大小写敏感:"
                + FilenameUtils.equalsNormalized(fileFullName, linuxFileName));
        System.out.println("(26)判断文件路径是否相同，不格式化，大小写敏感根据系统规则：windows：敏感；linux：不敏感:"
                + FilenameUtils.equalsOnSystem(fileFullName, linuxFileName));
        List<String> extensions = new ArrayList<>();
        extensions.add("txt");
        extensions.add("java");
        System.out.println("(27)判断文件扩展名是否包含在指定集合中:"
                + FilenameUtils.isExtension(fileFullName, extensions));
        System.out.println("(28)判断文件扩展名是否等于指定扩展名:"
                + FilenameUtils.isExtension(fileFullName, "txt"));
        System.out.println("(29)判断文件扩展名是否包含在指定字符串数组中:"
                + FilenameUtils.isExtension(fileFullName, new String[]{"txt","java"}));
        System.out.println("(30)判断文件扩展名是否和指定规则匹配，大小写敏感:"
                + FilenameUtils.wildcardMatch(fileName, "*.???"));
        System.out.println("(31)判断文件扩展名是否和指定规则匹配，大小写不敏感:"
                + FilenameUtils.wildcardMatch(fileName, "*.???",IOCase.INSENSITIVE));
        System.out.println("(32)判断文件扩展名是否和指定规则匹配，根据系统判断敏感型：windows:不敏感；linux：敏感:"
                + FilenameUtils.wildcardMatchOnSystem(fileName, "*.???"));

        System.out.println("parentPath" + FilenameUtils.getFullPath( FilenameUtils.getFullPathNoEndSeparator(fileFullName)));

        String testPath = "d:\\aa\\bb\\..\\cc\\dd.txt";
        testPath = FilenameUtils.normalize(testPath);

    }

}

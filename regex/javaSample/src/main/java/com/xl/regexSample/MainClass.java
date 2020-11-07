package com.xl.regexSample;

import java.util.HashMap;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class MainClass {
    public static void main(String[] args) {
        System.out.println("aa");
        Sample_1_1();
        //     Sample_1_3();
        //   Sample_4_1();
      //  Sample_4_4();


       // Sample_4_5();
      //  Sample_4_8();

    }

    public static void Sample_1_3() {
        String testRegex = "^\\d+$";
        String testString1 = "323232";
        String testString2 = "33343ag333";
        boolean result = Pattern.matches(testRegex, testString1);
        System.out.println(result);
        result = Pattern.matches(testRegex, testString2);
        System.out.println(result);
    }

    public static void Sample_4_1() {
        String imgRegex = "<img\\s+src=\"(.*?)\"";

        String testString1 = TxtReader.readToString("E:\\workfolder\\regex\\javaSample\\test1.html");

        Pattern imgPattern = Pattern.compile(imgRegex);
        Matcher imgMather = imgPattern.matcher(testString1);
        while (imgMather.find()) {
            System.out.println(imgMather.group(1));
        }


        String emailRegex = "[-_0-9a-zA-Z]*@[0-9a-zA-Z]{2,10}\\.[a-z]{2,5}";
        Pattern emailPattern = Pattern.compile(emailRegex);
        Matcher emailMather = emailPattern.matcher(testString1);
        while (emailMather.find()) {
            System.out.println(emailMather.group());
        }

    }


    public static void Sample_4_2() {
        String emailString1 = "aa@bb.com";
        String emailString2 = "#aa@bb.com";
        String emailRegex = "[-_0-9a-zA-Z]*@[0-9a-zA-Z]{2,10}\\.[a-z]{2,5}";
        boolean emailResult1 = Pattern.matches(emailRegex, emailString1);
        boolean emailResult2 = Pattern.matches(emailRegex, emailString2);

        String qqString1 = "33433333";
        String qqString2 = "23433234322243432";
        String qqRegex = "^[1-9][0-9]{4,10}$";
        boolean qqResult1 = Pattern.matches(qqRegex, qqString1);
        boolean qqResult2 = Pattern.matches(qqRegex, qqString2);


        String userString1 = "a33433333";
        String userString2 = "#aa@bb.com";
        String userRegex = "^[a-zA-Z][0-9a-zA-Z]{4,20}$";
        boolean userResult1 = Pattern.matches(userRegex, userString1);
        boolean userResult2 = Pattern.matches(userRegex, userString2);

        //密码必须包含 小写字母 ，大写字母  数字  特殊字符 才能匹配
        String passString1 = "aaA3@34";
        String passString2 = "aa@3";
        String passString3 = "asdfaA163#goo";
        String passRegex = "(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@*$#]).{2,16}";
        boolean passResult1 = Pattern.matches(passRegex, passString1);
        boolean passResult2 = Pattern.matches(passRegex, passString2);
        boolean passResult3 = Pattern.matches(passRegex, passString3);
    }

    public static void Sample_4_3() {
        String num = "33334444443";
        String numRegex1 = "\\B(?=(\\d{3})+$)";
        String numRegex2 = "\\B(?<=^(\\d{3})+)";
        String result1 = num.replaceAll(numRegex1, ",");
        String result2 = num.replaceAll(numRegex2, ",");
    }

    public static void Sample_4_4() {
        String hump = "javaName";
        String humpRegex = "(?=[A-Z])";
        String humpResult = hump.replaceAll(humpRegex,"_").toLowerCase();

        String line = "java_name_call_me_hero";
        String lineRegex = "_(\\w)";
        Pattern linePattern = Pattern.compile(lineRegex);
        Matcher lineMatcher = linePattern.matcher(line);
        StringBuffer sb = new StringBuffer();
        while (lineMatcher.find())
        {
            lineMatcher.appendReplacement(sb,lineMatcher.group(1).toUpperCase());
        }
        lineMatcher.appendTail(sb);

    }


    public static void Sample_4_5() {
        String cn = "的1eeedfdfdfdf";
        String cnRegex = "[\\u4e00-\\u9fa5]+.*";
        boolean cnResult1 = Pattern.matches(cnRegex, cn);
    }

    public static void Sample_4_8() {
        HashMap<String,String > paras = new HashMap<>();
        paras.put("time","value1");
        paras.put("date","value2");
        paras.put("where","value3");

        String line = "javaName{time}cccdef{date}klkjj{where}msdfd";
        String lineRegex = "\\{(.*?)\\}";
        Pattern linePattern = Pattern.compile(lineRegex);
        Matcher lineMatcher = linePattern.matcher(line);
        StringBuffer sb = new StringBuffer();
        while (lineMatcher.find())
        {
            lineMatcher.appendReplacement(sb,paras.get(lineMatcher.group(1)));
        }
        lineMatcher.appendTail(sb);

        System.out.println(sb.toString());
    }

    public static void Sample_1_1() {
    UrlReader.download("https://www.expreview.com/76627.html","E:\\workfolder\\76627.html");
    }

}

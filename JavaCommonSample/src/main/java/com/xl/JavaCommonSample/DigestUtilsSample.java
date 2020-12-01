package com.xl.JavaCommonSample;

import org.apache.commons.codec.digest.DigestUtils;

public class DigestUtilsSample {

    public static void main(String[] args) {
        DigestTest();
    }
    public static void  DigestTest()
    {
        String testString = "dfdfdfdf哈哈哈defefe";
        String result1 = DigestUtils.md5Hex(testString);
        String result2 = DigestUtils.sha1Hex(testString);
        String result3 = DigestUtils.sha512Hex(testString);
    }
}

package com.xl.regexSample;

import java.io.*;
import java.net.URL;
import java.net.URLConnection;

public class UrlReader {

    public static boolean  download(String urlString, String localFilePath) {
        try {
            File localFile  = new File(localFilePath);
            URL url = new URL(urlString);
            URLConnection connection = url.openConnection();
            connection.setConnectTimeout(5*1000);
            try(InputStream inputStream = connection.getInputStream();
                OutputStream outputStream = new FileOutputStream(localFile))
            {
                byte[] byteArr = new byte[1024];
                int len;
                File dir = localFile.getParentFile();
                if (dir.exists() == false)
                {
                    dir.mkdirs();
                }

                while ((len = inputStream.read(byteArr)) != -1) {
                    outputStream.write(byteArr, 0, len);
                }
            }

        } catch (IOException e) {
            e.printStackTrace();
            return false;
        }

        return true;
    }
}

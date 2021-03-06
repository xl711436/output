package com.xl.folderWatcher;

public class MainClass {
    public static void main(String[] args) {
        System.out.println("main start");

        try {
            String pwdString = ExecLinuxCMD.exec("pwd").toString();
            String netsString = ExecLinuxCMD.exec("netstat -nat|grep -i \"80\"|wc -l").toString();

            System.out.println("==========获得值=============");
            System.out.println(pwdString);
            System.out.println(netsString);
        } catch (Exception e1) {
            e1.printStackTrace();
        }

        ZJPFileMonitor m = null;

        try {
            m = new ZJPFileMonitor(10 * 1000);
        } catch (Exception e) {
            e.printStackTrace();
        }

        m.monitor("/srv/watch/", new ZJPFileListener());
        try {
            m.start();
        } catch (Exception e) {
            e.printStackTrace();
        }
        System.out.println("exit");
    }
}

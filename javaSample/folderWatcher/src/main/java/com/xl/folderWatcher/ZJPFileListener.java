package com.xl.folderWatcher;



import org.apache.commons.io.monitor.FileAlterationListener;
import org.apache.commons.io.monitor.FileAlterationObserver;


import javax.annotation.PostConstruct;
import java.io.File;
import java.io.IOException;


public class ZJPFileListener  implements FileAlterationListener {
    private static ZJPFileListener zjpFileListener;
    private static int changeFlag =0;

    ZJPFileMonitor monitor = null;

    /**
     *通过@PostConstruct实现初始化bean之前进行的操作
     */
    public void init() {
        zjpFileListener = this;
        changeFlag = 0;
    }

    /**
     *
     * @param fileAlterationObserver
     */
    @Override
    public void onStart(FileAlterationObserver fileAlterationObserver) {
        System.out.println("onStart");
        changeFlag = 0;
    }

    /**
     * 监控目录中创建一个目录时触发
     * @param file
     */
    @Override
    public void onDirectoryCreate(File file) {
        System.out.println("onDirectoryCreate");
        changeFlag++;
    }

    /**
     * 监控目录中目录发生改变触发
     * @param file
     */
    @Override
    public void onDirectoryChange(File file) {
        System.out.println("onDirectoryChange");
        changeFlag++;
    }

    /**
     * 监控目录中目录发生删除触发
     * @param file
     */
    @Override
    public void onDirectoryDelete(File file) {
        System.out.println("onDirectoryDelete");
        changeFlag++;
    }

    /**
     * 监控目录中创建文件时触发
     * @param file
     */
    @Override
    public void onFileCreate(File file) {
        System.out.println("onFileCreate");
        changeFlag++;
    }

    /**
     * 监控目录中改变文件时触发
     * @param file
     */
    @Override
    public void onFileChange(File file) {
        System.out.println("onFileChange");
        changeFlag++;
    }

    /**
     * 监控目录中文件删除时触发
     * @param file
     */
    @Override
    public void onFileDelete(File file) {
        System.out.println("onFileDelete");
        changeFlag++;
    }

    /**
     *
     * @param fileAlterationObserver
     */
    @Override
    public void onStop(FileAlterationObserver fileAlterationObserver) {
        System.out.println("onStop" + changeFlag);
        if(changeFlag > 0)
        {
            System.out.println("changed" );
            try {
                Thread.sleep(20 * 1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }

}
package com.xl.JavaCommonSample;

import java.util.Random;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.CopyOnWriteArrayList;

public class ConcurrentSample {

    public static void main(String[] args) {
        try {
            //MapTest();
            ListTest();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    public static void MapTest() throws InterruptedException {
        ConcurrentHashMap<Integer, Integer> curMap = new ConcurrentHashMap<>();

        Thread addThread = new Thread(() -> {
            for (int i = 0; i < 100000; i++) {
                curMap.put(i, i);
            }
            System.out.println("addThread end");
        });


        Thread removeThread = new Thread(() -> {
            while (curMap.size() > 1000) {
                for (int i = new Random().nextInt(5000); i < 100000; i++) {
                    if (curMap.size() > 1000) {
                        curMap.remove(i);
                    }
                }
            }
            System.out.println("removeThread end");
        });

        addThread.start();
        Thread.sleep(100);
        removeThread.start();


        addThread.join();
        removeThread.join();


        System.out.println(curMap.keySet().stream().mapToInt((x) -> x).summaryStatistics().getSum());

    }

    public static void ListTest()throws InterruptedException {
        CopyOnWriteArrayList<Integer> curList = new CopyOnWriteArrayList<>();

        Thread addThread = new Thread(() -> {
            for (int i = 0; i < 100000; i++) {
                curList.add(i);
            }
            System.out.println("addThread end");
        });


        Thread removeThread = new Thread(() -> {
            while (curList.size() > 1000) {
                for (int i = new Random().nextInt(100); i < 1000; i++) {
                    if (curList.size() > 1000) {
                        curList.remove(i);
                    }
                }
            }
            System.out.println("removeThread end");
        });

        addThread.start();
        Thread.sleep(100);
        removeThread.start();


        addThread.join();
        removeThread.join();


        System.out.println(curList.stream().mapToInt((x) -> x).summaryStatistics().getSum());

    }


}

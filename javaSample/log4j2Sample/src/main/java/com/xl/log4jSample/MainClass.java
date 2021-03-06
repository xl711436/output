package com.xl.log4jSample;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

public class MainClass {
    private static Logger logger = LogManager.getLogger(MainClass.class.getName());
    public static void main(String[] args) {
        logger.trace("aa");

    }
}

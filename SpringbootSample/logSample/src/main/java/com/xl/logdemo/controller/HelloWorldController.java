package com.xl.logdemo.controller;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.apache.logging.log4j.Level;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

@RestController
@RequestMapping("/hello")
public class HelloWorldController {


    @Value("${server.port}")
    public String port;

    @Value("${dd.bb}")
    public String bb;

    private static Logger logger = LogManager.getLogger(HelloWorldController.class.getName());


    @RequestMapping("/say")
    public String say(){
        logger.trace("trace");
        logger.info("info");
        logger.error("error");

        return "Hello World" + port+ " " + bb;
    }
}